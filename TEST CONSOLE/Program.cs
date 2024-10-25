// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using CsvHelper;
using SimpleFileIO;
using SimpleFileIO.UserProperties;
using System.Globalization;
using TEST_CONSOLE;

Manager.CreateCsvLog("test1",
    new PathProperties {
        RootDirectory = new DirectoryInfo("./"),
        FileName = "TEST",
        Extension = "csvlog"
    });

var my = Manager.GetCsvLog("test1");

var data2 = new testdata2();
my?.Add(data2);
my?.Add(data2);
my?.Write();

List<testdata2> temp1;
using (var reader = new StreamReader("./TEST.csvlog"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    temp1 = csv.GetRecords<testdata2>().ToList();
}
int a = 0;