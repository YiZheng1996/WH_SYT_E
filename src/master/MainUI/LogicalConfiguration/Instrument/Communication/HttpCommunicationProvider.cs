using MainUI.LogicalConfiguration.Instrument.Models;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MainUI.LogicalConfiguration.Instrument.Communication
{
    /// <summary>
    /// HTTP通讯提供者
    /// </summary>
    public class HttpCommunicationProvider(ILogger logger = null) : ICommunicationProvider
    {
        private HttpClient _httpClient;
        private HttpProtocolConfig _config;

        public ProtocolType ProtocolType => ProtocolType.Http;
        public bool IsConnected => _httpClient != null;
        public string ConnectionId => _config?.BaseUrl ?? "";

        public Task<bool> ConnectAsync(ProtocolConfigBase config, CancellationToken cancellationToken = default)
        {
            if (config is not HttpProtocolConfig httpConfig)
            {
                logger?.LogError("无效的HTTP配置");
                return Task.FromResult(false);
            }

            try
            {
                _config = httpConfig;
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri(httpConfig.BaseUrl),
                    Timeout = TimeSpan.FromMilliseconds(httpConfig.ConnectionTimeout)
                };

                // 设置认证
                if (httpConfig.AuthType == "Basic" && !string.IsNullOrEmpty(httpConfig.Username))
                {
                    var authBytes = Encoding.ASCII.GetBytes($"{httpConfig.Username}:{httpConfig.Password}");
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
                }
                else if (httpConfig.AuthType == "Bearer" && !string.IsNullOrEmpty(httpConfig.BearerToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", httpConfig.BearerToken);
                }

                // 设置默认请求头
                foreach (var header in httpConfig.DefaultHeaders)
                {
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }

                logger?.LogInformation("HTTP客户端初始化成功: {BaseUrl}", httpConfig.BaseUrl);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "HTTP客户端初始化失败");
                return Task.FromResult(false);
            }
        }

        public Task DisconnectAsync()
        {
            _httpClient?.Dispose();
            _httpClient = null;
            return Task.CompletedTask;
        }

        public async Task<CommunicationResult> SendAndReceiveAsync(
            byte[] data,
            FrameConfig frameConfig,
            int timeout,
            bool waitForResponse,
            CancellationToken cancellationToken = default)
        {
            var result = new CommunicationResult();
            var sw = Stopwatch.StartNew();

            try
            {
                if (_httpClient == null)
                {
                    return CommunicationResult.Failed("HTTP客户端未初始化");
                }

                var requestString = Encoding.UTF8.GetString(data);
                result.SentData = data;
                result.SentString = requestString;

                // 解析请求: "METHOD|URL|BODY" 格式
                var parts = requestString.Split('|');
                var method = parts.Length > 0 ? parts[0].ToUpper() : "GET";
                var url = parts.Length > 1 ? parts[1] : "/";
                var body = parts.Length > 2 ? parts[2] : "";

                using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);

                HttpResponseMessage response;
                switch (method)
                {
                    case "GET":
                        response = await _httpClient.GetAsync(url, cts.Token);
                        break;
                    case "POST":
                        var postContent = new StringContent(body, Encoding.UTF8, _config.ContentType);
                        response = await _httpClient.PostAsync(url, postContent, cts.Token);
                        break;
                    case "PUT":
                        var putContent = new StringContent(body, Encoding.UTF8, _config.ContentType);
                        response = await _httpClient.PutAsync(url, putContent, cts.Token);
                        break;
                    case "DELETE":
                        response = await _httpClient.DeleteAsync(url, cts.Token);
                        break;
                    default:
                        return CommunicationResult.Failed($"不支持的HTTP方法: {method}");
                }

                result.ResponseString = await response.Content.ReadAsStringAsync(cts.Token);
                result.RawResponse = Encoding.UTF8.GetBytes(result.ResponseString);
                result.Success = response.IsSuccessStatusCode;
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;

                if (!result.Success)
                {
                    result.ErrorMessage = $"HTTP错误: {(int)response.StatusCode} {response.ReasonPhrase}";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"HTTP请求异常: {ex.Message}";
                result.ElapsedMilliseconds = sw.ElapsedMilliseconds;
                logger?.LogError(ex, "HTTP请求异常");
                return result;
            }
        }

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            // HTTP不支持仅发送
            return Task.FromResult(false);
        }

        public Task<byte[]> ReceiveAsync(FrameConfig frameConfig, int timeout, CancellationToken cancellationToken = default)
        {
            // HTTP不支持单独接收
            return Task.FromResult(Array.Empty<byte>());
        }

        public void Dispose()
        {
            DisconnectAsync().Wait();
        }
    }
}
