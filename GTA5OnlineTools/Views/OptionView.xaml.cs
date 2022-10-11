namespace GTA5OnlineTools.Views;

/// <summary>
/// OptionView.xaml 的交互逻辑
/// </summary>
public partial class OptionView : UserControl
{
    public OptionView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;
    }

    private void MainWindow_WindowClosingEvent()
    {

    }
}
