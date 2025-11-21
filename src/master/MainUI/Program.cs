using MainUI.LogicalConfiguration;
using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods;
using MainUI.LogicalConfiguration.Services;
using MainUI.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.Configuration;

namespace MainUI
{
    static class Program
    {
        /// <summary>
        /// 全局服务提供者，用于整个应用程序
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>  
        /// 应用程序的主入口点
        /// </summary>  
        [STAThread]
        static void Main()
        {
            try
            {
                // 配置应用程序
                ConfigureApplication();

                // 初始化数据库
                InitializeDatabase();

                // 配置依赖注入
                ConfigureDependencyInjection();

                // 初始化试验台信息
                InitializeTestBench();

                // 检查单例运行
                EnsureSingleInstance();

                // 显示登录界面并启动主程序
                RunApplication();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"应用程序启动失败：{ex.Message}", ex);
                MessageBox.Show($"应用程序启动失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        #region 初始化方法

        /// <summary>
        /// 初始化试验台信息
        /// </summary>
        private static void InitializeTestBench()
        {
            try
            {
                TestBenchService.Initialize();
            }
            catch (Exception ex)
            {
                // 初始化失败直接退出,不再询问是否继续
                NlogHelper.Default.Error($"初始化试验台信息失败：{ex.Message}", ex);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        private static void InitializeDatabase()
        {
            VarHelper.fsql = new FreeSql.FreeSqlBuilder()
                //.UseMonitorCommand(cmd => Trace.WriteLine($"Sql：{cmd.CommandText}{cmd}"))
                .UseConnectionString(FreeSql.DataType.MySql,
                    ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString)
                .UseAutoSyncStructure(true) // 自动同步表结构
                .Build();

            if (!VarHelper.fsql.Ado.ExecuteConnectTest())
                throw new Exception("MySQL数据库连接失败");
        }

        /// <summary>
        /// 配置依赖注入容器
        /// </summary>
        private static void ConfigureDependencyInjection()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }


        private static Mutex applicationMutex;
        /// <summary>
        /// 确保只运行一个应用程序实例
        /// </summary>
        private static void EnsureSingleInstance()
        {
            string softname = Application.ProductName;
            VarHelper.SoftName = softname;

            applicationMutex = new Mutex(true, softname, out bool createdNew);

            if (!createdNew)
            {
                MessageBox.Show("只能运行一个程序！", "请确定",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 配置应用程序基本设置
        /// </summary>
        private static void ConfigureApplication()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
        }

        /// <summary>
        /// 运行应用程序主逻辑
        /// </summary>
        private static void RunApplication()
        {
            // 创建并配置登录窗体
            var login = CreateLoginForm();
            DialogResult loginResult = login.ShowDialog();
            if (loginResult == DialogResult.OK)
            {
                StartMainApplication();
            }
        }

        /// <summary>
        /// 创建登录窗体
        /// </summary>
        private static frmLogin CreateLoginForm()
        {
            var login = new frmLogin
            {
                lblSoftName = { Text = "制动阀类试验台E" },
                Icon = new Icon("ico.ico")
            };

            // 设置登录界面图片
            SetLoginImage(login);
            return login;
        }

        /// <summary>
        /// 设置登录界面图片
        /// </summary>
        private static void SetLoginImage(frmLogin login)
        {
            try
            {
                var files = Directory.GetFiles(Application.StartupPath, "ico.*");
                var imageFile = files.FirstOrDefault(x => !x.Contains("ico.ico"));

                if (imageFile != null)
                {
                    using var image = Image.FromFile(imageFile);
                    login.Logo.Image = new Bitmap(image); // 创建副本避免文件锁定
                }
            }
            catch (Exception ex)
            {
                // 图片加载失败不应该中断程序启动
                NlogHelper.Default.Warn($"加载登录界面图片失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 启动主应用程序
        /// </summary>
        private static void StartMainApplication()
        {
            try
            {
                // 连接OPC
                OPCHelper.Connect();

                // 获取主窗体
                var mainForm = ServiceProvider.GetRequiredService<frmMainMenu>();

                // 在应用启动时创建系统变量占位符
                InitializeSystemVariables();

                // 运行主程序
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("主应用程序启动失败：", ex);
                MessageBox.Show($"主应用程序启动失败：{ex.Message}", "系统提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void InitializeSystemVariables()
        {
            try
            {
                var globalVariableManager = ServiceProvider.GetService<GlobalVariableManager>();
                if (globalVariableManager != null)
                {
                    // 创建系统变量占位符
                    TestInfoVariableHelper.InitializeTestInfoVariables(globalVariableManager);

                    NlogHelper.Default.Info("✓ 系统变量占位符已创建");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化系统变量失败", ex);
            }
        }

        #endregion

        #region 服务配置

        /// <summary>
        /// 配置所有服务的依赖注入
        /// </summary>
        private static void ConfigureServices(IServiceCollection services)
        {
            // 工作流服务
            services.AddWorkflowServices(
             configureOptions: options =>
             {
                 options.EnableEventLogging = true;
                 options.EnablePerformanceMonitoring = false;
                 options.MaxVariableCacheSize = 1000;
                 options.MaxStepCacheSize = 500;
             },
             configureConfigOptions: options =>
             {
                 options.ConfigurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                      "Modules\\MyModules.ini");
                 options.EnableFileWatching = true;
                 options.CacheTimeoutMinutes = 30;
             });

            // 全局变量管理器
            services.AddSingleton<GlobalVariableManager>();

            // 工具模块方法类注册
            services.AddSingleton<SystemMethods>();
            services.AddSingleton<VariableMethods>();
            services.AddSingleton<PLCMethods>();
            services.AddSingleton<DetectionMethods>();
            services.AddSingleton<ReportMethods>();
            services.AddSingleton<WaitForStableMethods>();
            services.AddScoped<RealtimeMonitorPromptMethods>();
            services.AddTransient<ConditionMethods>();
            services.AddTransient<LoopMethods>();

            services.AddSingleton<ExpressionEngine>();
            services.AddSingleton<VariableAssignmentEngine>();

            services.AddTransient<frmMainMenu>();// 主窗体
            services.AddSingleton<IFormService, FormService>();// 窗体集合管理类
            services.AddTransient<DataGridViewManager>(); // UI管理器
            services.AddSingleton<WorkflowExecutionService>(); // 工作流执行服务

            // 日志服务
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();

                builder.ClearProviders(); // 清除默认提供者
                builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.AddNLog(); // 添加NLog
            });

            // 步骤执行管理器工厂
            services.AddTransient<Func<List<ChildModel>, StepExecutionManager>>(provider =>
                steps => ActivatorUtilities.CreateInstance<StepExecutionManager>(provider, steps));

        }
        #endregion
    }
}