namespace GTA5OnlineTools.Common.Utils;

public static class FileUtil
{
    /// <summary>
    /// 默认路径
    /// </summary>
    public const string Default_Path = @"C:\ProgramData\GTA5OnlineTools\";

    public const string Kiddion_Path = Default_Path + @"Kiddion\";
    public const string KiddionScripts_Path = Kiddion_Path + @"scripts\";

    public const string Cache_Path = Default_Path + @"Cache\";
    public const string GTAHaxStat_Path = Cache_Path + @"stat.txt";

    public const string Config_Path = Default_Path + @"Config\";
    public static string CustomTPList_Path = Config_Path + "CustomTPList.json";
    public static string BlockWords_Path = Config_Path + "BlockWords.txt";

    public const string Inject_Path = Default_Path + @"Inject\";

    public const string Log_Path = Default_Path + @"Log\";

    public const string Resource_Path = "GTA5OnlineTools.Features.Files.";
    public const string Resource_Kiddion_Path = "GTA5OnlineTools.Features.Files.Kiddion.";
    public const string Resource_Inject_Path = "GTA5OnlineTools.Features.Files.Inject.";

    /// <summary>
    /// 获取当前运行文件完整路径
    /// </summary>
    public static string Current_Path = Process.GetCurrentProcess().MainModule.FileName;

    /// <summary>
    /// 获取当前文件目录，不加文件名及后缀
    /// </summary>
    public static string CurrentDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    /// 我的文档完整路径
    /// </summary>
    public static string MyDocuments_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    /// <summary>
    /// 文件重命名
    /// </summary>
    public static void FileReName(string OldPath, string NewPath)
    {
        var ReName = new FileInfo(OldPath);
        ReName.MoveTo(NewPath);
    }

    /// <summary>
    /// 给文件名，得出当前目录完整路径，AppName带文件名后缀
    /// </summary>
    public static string GetCurrFullPath(string AppName)
    {
        return Path.Combine(CurrentDirectory_Path, AppName);
    }

    /// <summary>
    /// 从资源文件中抽取资源文件
    /// </summary>
    /// <param name="resFileName">资源文件名称（资源文件名称必须包含目录，目录间用“.”隔开,最外层是项目默认命名空间）</param>
    /// <param name="outputFile">输出文件</param>
    public static void ExtractResFile(string resFileName, string outputFile)
    {
        BufferedStream inStream = null;
        FileStream outStream = null;
        try
        {
            var asm = Assembly.GetExecutingAssembly();
            inStream = new BufferedStream(asm.GetManifestResourceStream(resFileName));
            outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

            var buffer = new byte[1024];
            int length;

            while ((length = inStream.Read(buffer, 0, buffer.Length)) > 0)
                outStream.Write(buffer, 0, length);

            outStream.Flush();
        }
        finally
        {
            outStream?.Close();
            inStream?.Close();
        }
    }

    /// <summary>
    /// 判断文件是否被占用
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static bool IsOccupied(string filePath)
    {
        FileStream stream = null;
        try
        {
            stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            return false;
        }
        catch
        {
            return true;
        }
        finally
        {
            stream?.Close();
        }
    }

    /// <summary>
    /// 保存错误Log日志文件到本地
    /// </summary>
    /// <param name="logContent">保存内容</param>
    public static void SaveErrorLog(string logContent)
    {
        var path = Log_Path + "ErrorLog";
        Directory.CreateDirectory(path);
        path += $@"\#ErrorLog# {DateTime.Now:yyyyMMdd_HH-mm-ss_ffff}.log";
        File.WriteAllText(path, logContent);
    }
}
