using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Windows;

/// <summary>
/// InjectorWindow.xaml 的交互逻辑
/// </summary>
public partial class InjectorWindow
{
    private InjectInfo InjectInfo { get; set; } = new();

    public ObservableCollection<ProcessList> ProcessLists { get; set; } = new();

    public InjectorWindow()
    {
        InitializeComponent();
    }

    private void Window_Injector_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        Task.Run(() =>
        {
            foreach (Process process in Process.GetProcesses())
            {
                this.Dispatcher.Invoke(() =>
                {
                    ProcessLists.Add(new ProcessList()
                    {
                        PID = process.Id,
                        PName = process.ProcessName,
                        MWindowTitle = process.MainWindowTitle,
                        MWindowHandle = process.MainWindowHandle,
                    });
                });
            }
        });
    }

    private void Window_Injector_Closing(object sender, CancelEventArgs e)
    {

    }

    /// <summary>
    /// 注入 按钮 点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Inject_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.PlayClickSound();

        if (string.IsNullOrEmpty(InjectInfo.DLLPath))
        {
            TextBlock_Status.Text = "请选择DLL文件路径";
            return;
        }

        if (InjectInfo.PID == 0)
        {
            TextBlock_Status.Text = "请选择目标进程";
            return;
        }

        foreach (ProcessModule module in Process.GetProcessById(InjectInfo.PID).Modules)
        {
            if (module.FileName == InjectInfo.DLLPath)
            {
                TextBlock_Status.Text = "该DLL已经被注入过了";
                return;
            }
        }

        try
        {
            BaseInjector.DLLInjector(InjectInfo.PID, InjectInfo.DLLPath);
            BaseInjector.SetForegroundWindow(InjectInfo.MWindowHandle);
            TextBlock_Status.Text = $"DLL注入到进程 {InjectInfo.PName} 成功";
        }
        catch (Exception ex)
        {
            TextBlock_Status.Text = ex.Message;
        }
    }

    /// <summary>
    /// 仅显示带窗口进程 单选框 点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckBox_OnlyShowWindowProcess_Click(object sender, RoutedEventArgs e)
    {
        ProcessLists.Clear();

        InjectInfo.PID = 0;
        InjectInfo.PName = string.Empty;
        InjectInfo.MWindowHandle = IntPtr.Zero;

        if (CheckBox_OnlyShowWindowProcess.IsChecked == true)
        {
            Task.Run(() =>
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (!string.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            ProcessLists.Add(new ProcessList()
                            {
                                PID = process.Id,
                                PName = process.ProcessName,
                                MWindowTitle = process.MainWindowTitle,
                                MWindowHandle = process.MainWindowHandle,
                            });
                        });
                    }
                }
            });
        }
        else
        {
            Task.Run(() =>
            {
                foreach (Process process in Process.GetProcesses())
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        ProcessLists.Add(new ProcessList()
                        {
                            PID = process.Id,
                            PName = process.ProcessName,
                            MWindowTitle = process.MainWindowTitle,
                            MWindowHandle = process.MainWindowHandle,
                        });
                    });
                }
            });
        }
    }

    private void TextBox_DLLPath_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Multiselect = false,
            RestoreDirectory = true,
            Filter = "DLL文件|*.dll"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            TextBox_DLLPath.Text = openFileDialog.FileName;
            InjectInfo.DLLPath = openFileDialog.FileName;
        }
        else
        {
            TextBox_DLLPath.Text = string.Empty;
            InjectInfo.DLLPath = string.Empty;
        }
    }

    private void DataGrid_Process_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataGrid_Process.SelectedItem is ProcessList temp)
        {
            InjectInfo.PID = temp.PID;
            InjectInfo.PName = temp.PName;
            InjectInfo.MWindowHandle = temp.MWindowHandle;
        }
    }
}
