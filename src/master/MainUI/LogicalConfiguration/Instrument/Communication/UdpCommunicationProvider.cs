using MainUI.LogicalConfiguration.Instrument.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ProtocolType = MainUI.LogicalConfiguration.Instrument.Models.ProtocolType;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// UDP 通讯提供者
    /// </summary>
    public class UdpCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private UdpClient _client;
        private UdpProtocolConfig _config;
        private IPEndPoint _remoteEndPoint;

        public ProtocolType ProtocolType => ProtocolType.Udp;
        public bool IsConnected => _client != null;
        public string ConnectionId => _config != null
            ? $"{_config.RemoteIpAddress}:{_config.RemotePort}"
            : "";

        Models.ProtocolType ICommunicationProvider.ProtocolType => throw new NotImplementedException();

       
        // 连接
        public Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not UdpProtocolConfig udpConfig)
            {
                logger?.LogError("无效的 UDP 配置");
                return Task.FromResult(false);
            }

            try
            {
                // UDP 无状态，重复调用时先释放旧的
                _client?.Close();
                _client?.Dispose();

                _config = udpConfig;
                _remoteEndPoint = new IPEndPoint(
                    IPAddress.Parse(udpConfig.RemoteIpAddress),
                    udpConfig.RemotePort);

                // 本地端口 0 表示系统自动分配
                _client = udpConfig.LocalPort > 0
                    ? new UdpClient(udpConfig.LocalPort)
                    : new UdpClient();

                // UDP Connect 只是记录默认远端地址，并不真正建立连接
                _client.Connect(_remoteEndPoint);

                logger?.LogInformation("UDP 就绪: {RemoteIP}:{RemotePort}",
                    udpConfig.RemoteIpAddress, udpConfig.RemotePort);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "UDP 初始化失败");
                return Task.FromResult(false);
            }
        }

        // ── 断开 

        public Task DisconnectAsync()
        {
            _client?.Close();
            _client?.Dispose();
            _client = null;
            logger?.LogInformation("UDP 已断开: {ConnectionId}", ConnectionId);
            return Task.CompletedTask;
        }

        // ── 发送并接收 

        public async Task<CommunicationResult> SendAndReceiveAsync(
            byte[] data,
            FrameConfig frameConfig,
            int timeout,
            bool waitForResponse,
            CancellationToken cancellationToken = default)
        {
            var result = new CommunicationResult
            {
                SentData = data,
                SentString = EncodingHelper.SmartDecode(data)
            };
            var sw = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                if (_client == null)
                    return CommunicationResult.Failed("UDP 未初始化");

                // 发送
                await _client.SendAsync(data, data.Length);
                logger?.LogDebug("UDP 发送({Bytes}B): {Hex}",
                    data.Length, BitConverter.ToString(data));

                if (!waitForResponse)
                {
                    result.Success = true;
                    result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                    return result;
                }

                // 接收（带超时）
                var receiveTask = _client.ReceiveAsync();
                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                var completedTask = await Task.WhenAny(
                    receiveTask,
                    Task.Delay(timeout, cts.Token));

                if (completedTask != receiveTask)
                    return CommunicationResult.Failed($"UDP 接收超时 ({timeout}ms)");

                var received = receiveTask.Result;
                result.RawResponse = received.Buffer;
                result.ResponseString = received.Buffer.Length > 0
                    ? EncodingHelper.SmartDecode(received.Buffer)
                    : string.Empty;
                result.Success = true;

                logger?.LogDebug("UDP 接收({Bytes}B): {Response}",
                    received.Buffer.Length, result.ResponseString);
            }
            catch (OperationCanceledException)
            {
                result.Success = false;
                result.ErrorMessage = $"UDP 接收超时 ({timeout}ms)";
                logger?.LogWarning("UDP 接收超时");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                logger?.LogError(ex, "UDP 发送/接收异常");
            }
            finally
            {
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
            }

            return result;
        }

        // ── 仅发送 

        public async Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            try
            {
                if (_client == null) return false;
                await _client.SendAsync(data, data.Length);
                return true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "UDP 发送失败");
                return false;
            }
        }

        // ── 仅接收 

        public async Task<byte[]> ReceiveAsync(
            FrameConfig frameConfig,
            int timeout,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (_client == null) return null;

                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                var result = await _client.ReceiveAsync(cts.Token);
                return result.Buffer;
            }
            catch (OperationCanceledException)
            {
                logger?.LogWarning("UDP 接收超时 ({Timeout}ms)", timeout);
                return null;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "UDP 接收异常");
                return null;
            }
        }

        // ── 释放 ─────────────────────────────────────────────────────────────

        public void Dispose()
        {
            _client?.Close();
            _client?.Dispose();
            _client = null;
        }
    }
}