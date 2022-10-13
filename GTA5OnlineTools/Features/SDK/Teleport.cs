using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Features.SDK;

public static class Teleport
{
    public static float PlayerX
    {
        get => GTA5Mem.Read<float>(General.WorldPTR, Offsets.PlayerPositionX);
        set
        {
            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerPositionX, value);
            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerVisualX, value);
        }
    }

    public static float PlayerY
    {
        get => GTA5Mem.Read<float>(General.WorldPTR, Offsets.PlayerPositionY);
        set
        {
            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerPositionY, value);
            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerVisualY, value);
        }
    }

    public static float PlayerZ
    {
        get => GTA5Mem.Read<float>(General.WorldPTR, Offsets.PlayerPositionZ);
        set
        {
            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerPositionZ, value);
            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerVisualZ, value);
        }
    }

    public static float VehicleX
    {
        get => GTA5Mem.Read<float>(General.WorldPTR, Offsets.VehiclePositionX);
        set
        {
            GTA5Mem.Write(General.WorldPTR, Offsets.VehiclePositionX, value);
            GTA5Mem.Write(General.WorldPTR, Offsets.VehicleVisualX, value);
        }
    }

    public static float VehicleY
    {
        get => GTA5Mem.Read<float>(General.WorldPTR, Offsets.VehiclePositionY);
        set
        {
            GTA5Mem.Write(General.WorldPTR, Offsets.VehiclePositionY, value);
            GTA5Mem.Write(General.WorldPTR, Offsets.VehicleVisualY, value);
        }
    }

    public static float VehicleZ
    {
        get => GTA5Mem.Read<float>(General.WorldPTR, Offsets.VehiclePositionZ);
        set
        {
            GTA5Mem.Write(General.WorldPTR, Offsets.VehiclePositionZ, value);
            GTA5Mem.Write(General.WorldPTR, Offsets.VehicleVisualZ, value);
        }
    }

    /// <summary>
    /// 传送功能
    /// </summary>
    public static void SetTeleportV3Pos(Vector3 pos)
    {
        if (pos != Vector3.Zero)
        {
            if (GTA5Mem.Read<int>(General.WorldPTR, Offsets.InVehicle) == 1)
            {
                VehicleX = pos.X;
                VehicleY = pos.Y;
                VehicleZ = pos.Z;
            }
            else
            {
                PlayerX = pos.X;
                PlayerY = pos.Y;
                PlayerZ = pos.Z;
            }
        }
    }

    /// <summary>
    /// 导航点坐标
    /// </summary>
    public static Vector3 WaypointCoords()
    {
        Vector3 v3 = Vector3.Zero;
        int dwIcon, dwColor;

        for (int i = 2000; i > 1; i--)
        {
            dwIcon = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x40 });
            dwColor = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x48 });

            if (dwIcon == 8 && dwColor == 84)
            {
                v3.X = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x10 });
                v3.Y = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x14 });
                v3.Z = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x18 });

                v3.Z = v3.Z == 20.0f ? -225.0f : v3.Z + 1.0f;

                return v3;
            }
        }

        return v3;
    }

    /// <summary>
    /// 目标点坐标
    /// </summary>
    public static Vector3 ObjectiveCoords()
    {
        Vector3 v3 = Vector3.Zero;
        int dwIcon, dwColor;

        for (int i = 2000; i > 1; i--)
        {
            dwIcon = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x40 });
            dwColor = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x48 });

            //  黄点
            if (dwIcon == 1 &&
                (dwColor == 5 || dwColor == 60 || dwColor == 66))
            {

                v3.X = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x10 });
                v3.Y = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x14 });
                v3.Z = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;

                return v3;
            }
        }

        for (int i = 2000; i > 1; i--)
        {
            dwIcon = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x40 });
            dwColor = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x48 });

            if ((dwIcon == 1 || dwIcon == 225 || dwIcon == 427 || dwIcon == 478 || dwIcon == 501 || dwIcon == 523 || dwIcon == 556) &&
                (dwColor == 1 || dwColor == 2 || dwColor == 3 || dwColor == 54 || dwColor == 78))
            {
                v3.X = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x10 });
                v3.Y = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x14 });
                v3.Z = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;

                return v3;
            }
        }

        for (int i = 2000; i > 1; i--)
        {
            dwIcon = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x40 });
            dwColor = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x48 });

            if ((dwIcon == 432 || dwIcon == 443) &&
                (dwColor == 59))
            {
                v3.X = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x10 });
                v3.Y = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x14 });
                v3.Z = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;

                return v3;
            }
        }

        return v3;
    }

    /// <summary>
    /// 目标点坐标自定义
    /// </summary>
    public static Vector3 ObjectiveCoordsCustom(int target_Icon)
    {
        Vector3 v3 = Vector3.Zero;
        int dwIcon;

        for (int i = 2000; i > 1; i--)
        {
            dwIcon = GTA5Mem.Read<int>(General.BlipPTR + (i * 8), new int[] { 0x40 });

            if (dwIcon == target_Icon)
            {
                v3.X = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x10 });
                v3.Y = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x14 });
                v3.Z = GTA5Mem.Read<float>(General.BlipPTR + (i * 8), new int[] { 0x18 }) + 1.0f;

                return v3;
            }
        }

        return v3;
    }

    /// <summary>
    /// 坐标向前微调
    /// </summary>
    public static void MovingFoward()
    {
        float sin = GTA5Mem.Read<float>(General.WorldPTR, Offsets.PlayerSin);
        float cos = GTA5Mem.Read<float>(General.WorldPTR, Offsets.PlayerCos);

        if (GTA5Mem.Read<int>(General.WorldPTR, Offsets.InVehicle) == 1)
        {
            float x = GTA5Mem.Read<float>(General.WorldPTR, Offsets.VehiclePositionX);
            float y = GTA5Mem.Read<float>(General.WorldPTR, Offsets.VehiclePositionY);

            x += Settings.Forward * cos;
            y += Settings.Forward * sin;

            GTA5Mem.Write(General.WorldPTR, Offsets.VehiclePositionX, x);
            GTA5Mem.Write(General.WorldPTR, Offsets.VehiclePositionY, y);

            GTA5Mem.Write(General.WorldPTR, Offsets.VehicleVisualX, x);
            GTA5Mem.Write(General.WorldPTR, Offsets.VehicleVisualY, y);
        }
        else
        {
            float x = GTA5Mem.Read<float>(General.WorldPTR, Offsets.PlayerPositionX);
            float y = GTA5Mem.Read<float>(General.WorldPTR, Offsets.PlayerPositionY);

            x += Settings.Forward * cos;
            y += Settings.Forward * sin;

            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerPositionX, x);
            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerPositionY, y);

            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerVisualX, x);
            GTA5Mem.Write(General.WorldPTR, Offsets.PlayerVisualY, y);
        }
    }

    /// <summary>
    /// 传送到导航点
    /// </summary>
    public static void ToWaypoint()
    {
        SetTeleportV3Pos(WaypointCoords());
    }

    /// <summary>
    /// 传送到目标点
    /// </summary>
    public static void ToObjective()
    {
        SetTeleportV3Pos(ObjectiveCoords());
    }

    /// <summary>
    /// 传送到Blips
    /// </summary>
    public static void ToBlips(int blipID)
    {
        SetTeleportV3Pos(ObjectiveCoordsCustom(blipID));
    }
}
