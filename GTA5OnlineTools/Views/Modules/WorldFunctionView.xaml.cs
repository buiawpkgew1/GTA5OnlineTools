using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Views.Modules;

/// <summary>
/// WorldFunctionView.xaml 的交互逻辑
/// </summary>
public partial class WorldFunctionView : UserControl
{
    private List<PVInfo> pVInfos = new();

    public WorldFunctionView()
    {
        InitializeComponent();
        this.DataContext = this;
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        // Ped列表
        foreach (var item in PedData.PedDataClass)
        {
            ListBox_PedModel.Items.Add(item.DisplayName);
        }
        ListBox_PedModel.SelectedIndex = 0;
    }

    private void MainWindow_WindowClosingEvent()
    {

    }

    private void Button_LocalWeather_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();
        var index = MiscData.LocalWeathers.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            World.Set_Local_Weather(MiscData.LocalWeathers[index].ID);
        }
    }

    private void Button_KillNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.KillNPC(false);
    }
    private void Button_KillAllHostilityNPC_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.KillNPC(true);
    }

    private void Button_KillAllPolice_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.KillPolice();
    }

    private void Button_DestroyAllVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.DestroyAllVehicles();
    }

    private void Button_DestroyAllNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.DestroyNPCVehicles(false);
    }

    private void Button_DestroyAllHostilityNPCVehicles_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.DestroyNPCVehicles(true);
    }

    private void Button_TPAllNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.TeleportNPCToMe(false);
    }

    private void Button_TPHostilityNPCToMe_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        World.TeleportNPCToMe(true);
    }

    private void Button_ModelChange_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var index = ListBox_PedModel.SelectedIndex;
        if (index != -1)
            Online.ModelChange(PedData.PedDataClass[index].Hash);
    }

    private void Button_RefushPersonalVehicleList_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        ListBox_PersonalVehicle.Items.Clear();
        pVInfos.Clear();

        Task.Run(() =>
        {
            int max_slots = Hacks.ReadGA<int>(1585857);
            for (int i = 0; i < max_slots; i++)
            {
                long hash = Hacks.ReadGA<long>(1585857 + 1 + (i * 142) + 66);
                if (hash == 0)
                    continue;

                string plate = Hacks.ReadGAString(1585857 + 1 + (i * 142) + 1);

                pVInfos.Add(new PVInfo()
                {
                    Index = i,
                    Name = Vehicle.FindVehicleDisplayName(hash, true),
                    hash = hash,
                    plate = plate
                });
            }

            foreach (var item in pVInfos)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ListBox_PersonalVehicle.Items.Add($"[{item.plate}]\t{item.Name}");
                });
            }
        });
    }

    private void Button_SpawnPersonalVehicle_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index = ListBox_PersonalVehicle.SelectedIndex;
        if (index != -1)
        {
            Task.Run(() =>
            {
                Vehicle.SpawnPersonalVehicle(pVInfos[index].Index);
            });
        }
    }
}

public struct PVInfo
{
    public int Index;
    public string Name;
    public long hash;
    public string plate;
}
