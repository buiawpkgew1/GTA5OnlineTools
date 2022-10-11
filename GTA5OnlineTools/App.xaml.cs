using GTA5OnlineTools.Common.Utils;

namespace GTA5OnlineTools;

/// <summary>
/// App.xaml 的交互逻辑
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// 主程序互斥体
    /// </summary>
    public static Mutex AppMainMutex;

    protected override void OnStartup(StartupEventArgs e)
    {
        AppMainMutex = new Mutex(true, ResourceAssembly.GetName().Name, out var createdNew);

        if (createdNew)
        {
            RegisterEvents();

            base.OnStartup(e);
        }
        else
        {
            MessageBox.Show("请不要重复打开，程序已经运行\n如果一直提示，请到\"任务管理器-详细信息（win7为进程）\"里结束本程序",
                "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            Current.Shutdown();
        }
    }

    /// <summary>
    /// 注册异常捕获事件
    /// </summary>
    private void RegisterEvents()
    {
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
    }

    /// <summary>
    /// UI线程未捕获异常处理事件（UI主线程）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        var msg = GetExceptionMsg(e.Exception, e.ToString());
        FileUtil.SaveErrorLog(msg);
    }

    /// <summary>
    /// 非UI线程未捕获异常处理事件（例如自己创建的一个子线程）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var msg = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
        FileUtil.SaveErrorLog(msg);
    }

    /// <summary>
    /// Task线程内未捕获异常处理事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        var msg = GetExceptionMsg(e.Exception, e.ToString());
        FileUtil.SaveErrorLog(msg);
    }

    /// <summary>
    /// 生成自定义异常消息
    /// </summary>
    /// <param name="ex">异常对象</param>
    /// <param name="backStr">备用异常消息：当ex为null时有效</param>
    /// <returns>异常字符串文本</returns>
    private string GetExceptionMsg(Exception ex, string backStr)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("【出现时间】：" + DateTime.Now.ToString());

        if (ex != null)
        {
            stringBuilder.AppendLine("【异常类型】：" + ex.GetType().Name);
            stringBuilder.AppendLine("【异常信息】：" + ex.Message);
            stringBuilder.AppendLine("【堆栈调用】：\n" + ex.StackTrace);
        }
        else
        {
            stringBuilder.AppendLine("【未处理异常】：" + backStr);
        }

        return stringBuilder.ToString();
    }
}
