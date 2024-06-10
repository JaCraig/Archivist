namespace Archivist.Tests.Utils
{
    internal static class CSVFileData
    {
        public static string ExampleCSV1 => "Header 1,Header 2,Header 3,Header 4,Header 5,Header 6\r\nThis,is,a,test,CSV,file\r\nTons,of,data,in here,is,super\r\nimportant";

        public static string ExamplePipe1 => "Header 1|Header 2|Header 3|Header 4|Header 5|Header 6\r\nThis|is|a|test|CSV|file\r\nTons|of|data|in here|is|super\r\nimportant";
        public static string ExampleTSV1 => "Header 1\tHeader 2\tHeader 3\tHeader 4\tHeader 5\tHeader 6\r\nThis\tis\ta\ttest\tCSV\tfile\r\nTons\tof\tdata\tin here\tis\tsuper\r\nimportant";
    }
}