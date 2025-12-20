using AntdUI;
using MainUI.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainUI;
public partial class frmMainMenu : Form
{
    private UcHMI _hmi;
    private OpcStatusGrp _opcStatus;
    private frmHardWare _hardWare;

    [System.Runtime.InteropServices.LibraryImport("user32.dll")]
    [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
    public static partial bool ReleaseCapture();
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    public const int WM_SYSCOMMAND = 0x0112;
    public const int SC_MOVE = 0xF010;
    public const int HTCAPTION = 0x0002;
    private readonly TimeTrackingService _timeTrackingService;

    public frmMainMenu()
    {
        InitializeComponent();
        InitializeBasicSettings();
        _timeTrackingService = new TimeTrackingService();
    }

    #region 初始化
    /// <summary>
    /// 初始化基本设置
    /// </summary>
    private void InitializeBasicSettings()
    {
        try
        {
            timerHeartbeat.Start();
            Text = VarHelper.SoftName;
            lblTitle.Text = VarHelper.SoftName;
            CheckForIllegalCrossThreadCalls = false;
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"初始化基本设置失败：{ex.Message}");
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        try
        {
            // 1. 初始化组件
            InitializeComponents();

            // 初始化权限
            InitializePermissions();

            // 初始化HMI
            InitializeHMI();
        }
        catch (Exception ex)
        {
            MessageHelper.MessageOK($"窗体初始化失败：{ex.Message}");
        }
    }

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void InitializeComponents()
    {
        if (!DesignMode) // 设计模式下不加载
        {
            try
            {
                var _workflowService = Program.ServiceProvider?.GetService<WorkflowExecutionService>();
                var _logger = Program.ServiceProvider?.GetService<ILogger<UcHMI>>();
                if (_workflowService == null)
                {
                    // 如果获取不到服务，记录警告但不影响其他功能
                    Debug.WriteLine("警告：无法获取工作流服务，工作流执行功能将不可用");
                }
                _hmi = new UcHMI(_workflowService, _logger) { Dock = DockStyle.Fill };
                InitializeEvents(); // 确保在变量初始化完成后再订阅事件
                _hmi.Init();
                _hardWare = new frmHardWare();
                _opcStatus = OPCHelper.opcStatus;
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK(this, $"获取工作流服务失败: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// 初始化事件
    /// </summary>
    private void InitializeEvents()
    {
        try
        {
            // HMI事件
            _hmi.EmergencyStatusChanged += OnEmergencyStatusChanged;
            _hmi.TestStateChanged += OnTestStateChanged;

            // 测试状态事件
            BaseTest.TestStateChanged += OnBaseTestStateChanged;
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"初始化事件失败：{ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 初始化权限
    /// </summary>
    private void InitializePermissions()
    {
        try
        {
            var currentUser = NewUsers.NewUserInfo;
            PermissionHelper.Initialize(currentUser.ID, currentUser.Role_ID);
            PermissionHelper.ApplyPermissionToControl(this, currentUser.ID);
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"初始化权限失败：{ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 初始化HMI
    /// </summary>
    private void InitializeHMI()
    {
        try
        {
            _hardWare.InitZeroGain();
            PanelHmi.Controls.Add(_hmi);
            timerPLC.Enabled = true;
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"初始化HMI失败：{ex.Message}");
            throw;
        }
    }
    #endregion

    #region 权限验证
    private bool CheckPermission(string permissionCode)
    {
        if (!PermissionHelper.HasPermission(NewUsers.NewUserInfo.ID, permissionCode))
        {
            MessageHelper.MessageOK(this, "您没有执行此操作的权限！", AntdUI.TType.Error);
            return false;
        }
        return true;
    }
    #endregion

    #region 事件处理
    private void OnTestStateChanged(bool isTesting, bool Exit = false) => UpdateControlsState(!isTesting, Exit);

    private void OnBaseTestStateChanged(bool isTesting) => UpdateControlsState(!isTesting);

    private void OnEmergencyStatusChanged(bool enabled)
    {
        // 更新控件状态
        UpdateControlsState(enabled);

        // 更新急停状态图标
        picRunStatus.Image = enabled ? Resources.noemergent : Resources.emergent;
        PanelHmi.Enabled = enabled;
    }

    private void UpdateControlsState(bool enabled, bool isTesting = false)
    {
        // 批量更新按钮状态
        var buttons = new[]
        {
            btnNLog, btnReports, btnHardwareTest, btnMainData,
            btnChangePwd, btnMeteringRemind, btnErrStatistics,
            btnDeviceDetection,
        };

        foreach (var button in buttons)
        {
            button.Enabled = enabled;
        }
        btnExit.Enabled = !isTesting;
    }
    #endregion

    #region 窗体拖动
    private void lblTitle_MouseDown(object sender, MouseEventArgs e)
    {
        ReleaseCapture();
        SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
    }
    #endregion

    #region 功能按钮事件处理

    /// <summary>
    /// 参数设置按钮点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void BtnMainData_Click(object sender, EventArgs e)
    {
        // 记录需要刷新的内容类型
        bool needRefreshWorkflow = false;     // 是否需要刷新工作流
        bool needRefreshReport = false;       // 是否需要刷新报表
        bool needRefreshParams = false;       // 是否需要刷新参数

        // 创建数据变更监听器
        DataChangedEventManager.DataChangedWithType += (changeType) =>
        {
            switch (changeType)
            {
                case DataChangeType.TestStep:       // 工作流配置修改
                    needRefreshWorkflow = true;
                    NlogHelper.Default.Debug("检测到工作流配置变更");
                    break;

                case DataChangeType.TestProcess:    // 项点管理修改
                    needRefreshParams = true;
                    needRefreshWorkflow = true;
                    NlogHelper.Default.Debug("检测到项点管理变更");
                    break;

                case DataChangeType.Model:          // 型号管理修改
                case DataChangeType.ModelType:      // 类型管理修改
                    needRefreshParams = true;
                    needRefreshWorkflow = true;
                    NlogHelper.Default.Debug($"检测到型号/类型变更: {changeType}");
                    break;

                case DataChangeType.ReportTemplate: // 报表模板修改
                    needRefreshReport = true;
                    NlogHelper.Default.Debug("检测到报表模板变更");
                    break;

                case DataChangeType.All:            // 全部刷新
                    needRefreshParams = true;
                    needRefreshWorkflow = true;
                    needRefreshReport = true;
                    NlogHelper.Default.Debug("检测到全部数据变更");
                    break;
            }
        };

        try
        {
            using var main = new frmSettingMain();
            ConfigureMainDataNodes(main);
            VarHelper.ShowDialogWithOverlay(this, main);

            // 根据不同的变更类型执行对应的刷新
            if (needRefreshParams || needRefreshWorkflow || needRefreshReport)
            {
                if (needRefreshParams)
                {
                    _hmi.RefreshParaConfig();
                    _hmi.RefreshTestItems();
                }

                // 刷新工作流
                if (needRefreshWorkflow)
                {
                    await _hmi.RefreshWorkflowConfigAsync();
                }

                // 刷新报表
                if (needRefreshReport)
                {
                    _hmi.RefreshReport();
                }

                NlogHelper.Default.Info($"数据刷新完成 - 参数:{needRefreshParams} 工作流:{needRefreshWorkflow} 报表:{needRefreshReport}");
            }
            else
            {
                NlogHelper.Default.Debug("未检测到数据变更，跳过刷新操作");
            }
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"主数据配置错误: {ex.Message}", ex);
            MessageHelper.MessageOK(this, $"操作失败: {ex.Message}", TType.Error);
        }
        finally
        {
            DataChangedEventManager.DataChangedWithType -= (changeType) =>
        {
            switch (changeType)
            {
                case DataChangeType.TestStep:       // 工作流配置修改
                    needRefreshWorkflow = true;
                    NlogHelper.Default.Debug("检测到工作流配置变更");
                    break;

                case DataChangeType.TestProcess:    // 项点管理修改
                    needRefreshParams = true;
                    needRefreshWorkflow = true;
                    NlogHelper.Default.Debug("检测到项点管理变更");
                    break;

                case DataChangeType.Model:          // 型号管理修改
                case DataChangeType.ModelType:      // 类型管理修改
                    needRefreshParams = true;
                    needRefreshWorkflow = true;
                    NlogHelper.Default.Debug($"检测到型号/类型变更: {changeType}");
                    break;

                case DataChangeType.ReportTemplate: // 报表模板修改（新增）
                    needRefreshReport = true;
                    NlogHelper.Default.Debug("检测到报表模板变更");
                    break;

                case DataChangeType.All:            // 全部刷新
                    needRefreshParams = true;
                    needRefreshWorkflow = true;
                    needRefreshReport = true;
                    NlogHelper.Default.Debug("检测到全部数据变更");
                    break;
            }
        };
        }
    }

    /// <summary>
    /// 配置主数据节点设置
    /// </summary>
    /// <param name="main">主设置窗体实例</param>
    private void ConfigureMainDataNodes(frmSettingMain main)
    {
        ArgumentNullException.ThrowIfNull(main);
        try
        {
            // 获取当前登录用户的ID
            var userId = NewUsers.NewUserInfo.ID;

            // 判断是否为超级管理员
            if (IsAdminUser(userId))
            {
                // 超级管理员:显示所有菜单
                ConfigureAdminNodes(main);
                return;
            }

            // 普通用户:过滤受限节点后再根据权限显示
            ConfigureRegularUserNodes(main, userId);
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"配置主数据节点失败: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 节点配置列表
    /// </summary>
    /// <returns></returns>
    private static (string Name, UserControl Control, int Index)[]
      GetMainDataNodes() =>
      [
         ("用户管理", new ucUserManager(), 0),
         ("角色管理", new ucRole(), 6),
         ("权限管理", new ucPermission(), 7),
         ("权限分配", new ucPermissionAllocation(), 8),
         ("类型管理", new ucKindManage(), 1),
         ("型号管理", new ucModelManage(), 9),
         ("项点管理", new ucItemManagerial(), 4),
         ("项点配置", new ucItemConfiguration(), 5),
         ("试验参数", new UcTestParams(), 2)
      ];

    /// <summary>
    /// 检查是否为管理员用户
    /// </summary>
    private static bool IsAdminUser(int userId) => userId == 1;

    /// <summary>
    /// 配置管理员节点
    /// </summary>
    private static void ConfigureAdminNodes(frmSettingMain main)
    {
        // 遍历所有节点
        foreach (var node in GetMainDataNodes())
        {
            // 全部添加到菜单树中,不做任何过滤
            main.AddNodes(node.Name, node.Control, node.Index);
        }
    }

    /// <summary>
    /// 配置普通用户的菜单节点
    /// 步骤:
    /// 1. 先过滤掉受限节点(权限管理、权限分配)
    /// 2. 再根据用户的角色权限进行二次过滤
    /// </summary>
    private void ConfigureRegularUserNodes(frmSettingMain main, int userId)
    {
        // 获取用户的权限配置
        var permissionConfigs = GetPermissionConfigurations();

        // 获取所有节点并过滤掉受限节点
        var nodes = GetMainDataNodes()
            .Where(node => !IsRestrictedNode(node.Name)); // ← 关键过滤

        // 遍历过滤后的节点
        foreach (var node in nodes)
        {
            // 检查用户是否有该节点的权限
            AddNodeWithPermissionCheck(main, node, permissionConfigs);
        }
    }
    /// <summary>
    /// 检查是否为受限节点
    /// </summary>
    private static bool IsRestrictedNode(string nodeName) =>
        nodeName is "权限管理" or "权限分配";

    /// <summary>
    /// 从数据库获取用户的权限配置
    /// 返回: 字典 <控件名称, 权限代码>
    /// </summary>
    private static Dictionary<string, string> GetPermissionConfigurations()
    {
        try
        {
            var permissionConfigBLL = new PermissionBLL();

            // 从数据库查询所有权限
            return permissionConfigBLL.GetPermissions()
                .Where(p => !string.IsNullOrEmpty(p.ControlName)) // 过滤空控件名
                .ToDictionary(
                    p => p.ControlName,    // 键: 控件名称 (如 "ucUserManager")
                    p => p.PermissionCode, // 值: 权限代码 (如 "USER_MANAGE")
                    StringComparer.OrdinalIgnoreCase // 忽略大小写
                );
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"获取权限配置失败: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 添加节点前检查用户是否有该节点的访问权限
    /// </summary>
    private void AddNodeWithPermissionCheck(
        frmSettingMain main,
        (string Name, UserControl Control, int Index) node,
        Dictionary<string, string> permissionConfigs)
    {
        try
        {
            // 尝试获取该控件对应的权限代码
            if (!TryGetPermissionCode(node.Control.Name, permissionConfigs, out var code))
            {
                return; // 没有权限代码,不添加
            }

            // 检查用户是否有该权限
            if (PermissionHelper.HasPermission(NewUsers.NewUserInfo.ID, code))
            {
                // 有权限,添加到菜单树
                main.AddNodes(node.Name, node.Control, node.Index);
            }
            // 没有权限,不添加(静默跳过)
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error($"添加节点[{node.Name}]失败：{ex.Message}");
        }
    }

    /// <summary>
    /// 尝试从权限配置中获取权限代码
    /// </summary>
    private bool TryGetPermissionCode(
        string controlName,
        Dictionary<string, string> permissionConfigs,
        out string code)
    {
        if (!permissionConfigs.TryGetValue(controlName, out code))
        {
            NlogHelper.Default.Warn($"控件[{controlName}]未找到对应的权限配置");
            return false;
        }
        return true;
    }

    private void ShowDialog<T>(object sender = null) where T : Form, new()
    {
        var btn = sender as UIButton;
        if (btn.Tag != null && !CheckPermission(btn.Tag.ToString()))
            return;

        using var form = new T();
        VarHelper.ShowDialogWithOverlay(this, form);
    }

    private void btnHardwareTest_Click(object sender, EventArgs e) =>
        ShowDialog<frmHardWare>(sender);

    private void btnReports_Click(object sender, EventArgs e) =>
        ShowDialog<FrmDataManager>(sender);

    private void btnChangePwd_Click(object sender, EventArgs e) =>
        ShowDialog<frmRemindEdit>(sender);

    private void btnNLog_Click(object sender, EventArgs e) =>
        ShowDialog<frmNLogs>(sender);

    private void btnMeteringRemind_Click(object sender, EventArgs e) =>
        ShowDialog<frmMeteringRemind>(sender);

    private void btnDeviceDetection_Click(object sender, EventArgs e) =>
        ShowDialog<frmDeviceInspect>(sender);

    private void btnErrStatistics_Click(object sender, EventArgs e) =>
        ShowDialog<frmErrStatistics>(sender);

    #endregion

    #region 系统操作
    private void btnLogout_Click(object sender, EventArgs e) =>
        Application.Restart();

    private void btnExit_Click(object sender, EventArgs e)
    {
        if (MessageHelper.MessageYes(this, "确定要退出试验吗？") != DialogResult.OK)
            return;

        try
        {
            OPCHelper.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        finally
        {
            Application.Exit();
        }
    }

    #endregion

    #region 定时器事件

    private void timerPLC_Tick(object sender, EventArgs e)
    {
        try
        {
            UpdateUI();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateUI()
    {
        // 更新时间显示
        lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // 更新用户信息
        var userInfo = NewUsers.NewUserInfo;
        tslblUser.Text = $"当前登录用户： {userInfo.Username}  当前权限：{userInfo.RoleName} ";

        // 更新试验台信息
        var testBench = TestBenchService.CurrentTestBench;
        tslblWorkStation.Text = $"工位名称：{testBench?.BenchName ?? "未知"}  IP：{testBench?.BenchIP}";

        // 更新PLC状态
        tslblPLC.Text = _opcStatus.NoError ? " PLC连接正常 " : " PLC连接失败 ";
        tslblPLC.BackColor = _opcStatus.NoError ? Color.FromArgb(110, 190, 40) : Color.Salmon;

        if (_opcStatus.Simulated)
        {
            tslblPLC.Text += " 仿真模式 ";
        }
    }

    private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
    {

    }
    #endregion
}
