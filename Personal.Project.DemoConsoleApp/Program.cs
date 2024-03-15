using Personal.Project.DemoConsoleApp;
using Personal.Project.DatabaseLibrary.Adapters;
using Personal.Project.FileSystemLibrary;

Console.WriteLine("Starting testing modules.");

var excelPaths = FileManager.FindAllExcelPaths("D:\\SRC\\CS\\Personal.Project\\Content\\Temp\\9dca7f2a-544b-4a2a-a8fa-8beeda9744f4");
var excelManager = new ExcelManager(excelPaths.ToList());

excelManager.ParseArchiveRecordsToDatabase();