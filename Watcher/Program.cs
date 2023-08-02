public class Program
{
    private static FileSystemWatcher watcher;                                       //объявление FileSystemWatcher
    private static StreamWriter logWriter;                                          //объявление StreamWriter
    private static void OnFileChanged(object sender, FileSystemEventArgs e)         //изменение файла
    {
        string logMessage = $"{DateTime.Now} - File {e.ChangeType}: {e.Name};";     //сообщение
        logWriter.WriteLine(logMessage);                                            //запись сообщения в лог
        Console.WriteLine(logMessage);                                              //temp вывод сообщения в консоль
    }
    private static void OnFileDeleted(object sender, FileSystemEventArgs e)         //удаление файла
    {
        string logMessage = $"{DateTime.Now} - File {e.ChangeType}: {e.Name};";     //сообщение
        logWriter.WriteLine(logMessage);                                            //запись сообщения в лог
        Console.WriteLine(logMessage);                                              //temp вывод сообщения в консоль
    }
    private static void OnFileCreated(object sender, FileSystemEventArgs e)         //создание файла
    {
        string logMessage = $"{DateTime.Now} - File {e.ChangeType}: {e.Name};";     //сообщение
        logWriter.WriteLine(logMessage);                                            //запись сообщения в лог
        Console.WriteLine(logMessage);                                              //temp вывод сообщения в консоль
    }
    private static void OnFileRenamed(object sender, FileSystemEventArgs e)         //переименование файла
    {
        string logMessage = $"{DateTime.Now} - File {e.ChangeType}: {e.Name};";     //сообщение
        logWriter.WriteLine(logMessage);                                            //запись сообщения в лог
        Console.WriteLine(logMessage);                                              //temp вывод сообщения в консоль
    }
    private static void OnApplicationExit(object sender, EventArgs e)               //закрытие приложения
    {
        Console.WriteLine("Closing app...");
        if (watcher != null)                                                        
        {
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();                                                      //закрыть наблюдатель
        }
        if (logWriter != null)
        {
            logWriter.Close();                                                      //закрыть StreamWriter
            logWriter.Dispose();
        }
    }
    public static void Main()
    {
        DirectoryInfo logDirectory = new DirectoryInfo(@"C:\log");                          //директория с логом
        if (!logDirectory.Exists) { Directory.CreateDirectory(logDirectory.FullName); }     
        FileInfo logFileInfo = new FileInfo(logDirectory.FullName + @"\log.txt");           //файл лога

        logWriter = new StreamWriter(logFileInfo.FullName, true);                           //если лог не существует - создать

        watcher = new FileSystemWatcher(@"C:\Users\gaukk\Downloads");                       //наблюдаемая директория

        watcher.EnableRaisingEvents = true;                                                 //включение
        watcher.IncludeSubdirectories = true;                                               //настройка
        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        watcher.Changed += OnFileChanged;                                                   //изменение файла
        watcher.Deleted += OnFileDeleted;                                                   //удаление файла
        watcher.Created += OnFileCreated;                                                   //создание файла
        watcher.Renamed += OnFileRenamed;                                                   //переименование файла

        AppDomain.CurrentDomain.ProcessExit += OnApplicationExit;                           //закрытие приложения

        Console.WriteLine("Нажмите Enter для выхода.");
        Console.ReadLine();
    }
}
