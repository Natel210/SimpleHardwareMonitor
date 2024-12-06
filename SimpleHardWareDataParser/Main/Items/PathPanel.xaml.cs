using SimpleHardWareDataParser.Rawdata;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SimpleHardWareDataParser.Main.Items
{
    /// <summary>
    /// PathPanel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PathPanel : UserControl
    {
        private static readonly FrameworkPropertyMetadataOptions _frameworkPropertyMetadataOptions = FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure;

        public static readonly DependencyProperty RootDirectoryProperty
            = DependencyProperty.Register(
                nameof(RootDirectory),
                typeof(string),
                typeof(PathPanel),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string RootDirectory
        {
            get { return (string)GetValue(RootDirectoryProperty); }
            set { SetValue(RootDirectoryProperty, value); }
        }

        public static readonly DependencyProperty ActiveGroupProperty
            = DependencyProperty.Register(
                nameof(ActiveGroup),
                typeof(bool),
                typeof(PathPanel),
                new FrameworkPropertyMetadata(false, _frameworkPropertyMetadataOptions));
        public bool ActiveGroup
        {
            get { return (bool)GetValue(ActiveGroupProperty); }
            set { SetValue(ActiveGroupProperty, value); }
        }

        public PathPanel()
        {
            InitializeComponent();
        }

        private void DirectorySearchingButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ReloadingButtonClick(object sender, RoutedEventArgs e)
        {
            RawdataRecordManager.Load(new DirectoryInfo(RootDirectory));
            MakeSplitFormat();
            RawdataRecordManager.CalculateData();


        }
    }

    public partial class PathPanel
    {
        private void MakeSplitFormat()
        {
            var splitTemplate = RawdataRecordManager.SplitTemplate;
            splitTemplate.Clear();
            splitTemplate.Add(
                "ALL",
                new()
                {
                    SplitName = "ALL",
                    SplitStart = new DateTime(2024, 10, 28, 10, 00, 00),
                    SplitEnd = new DateTime(2024, 12, 30, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 28, 18, 00, 00),
                    //SplitEnd = new DateTime(2024, 12, 02, 09, 00, 00)
                });

            splitTemplate.Add(
                "1",
                new()
                {
                    SplitName = "1",
                    SplitStart = new DateTime(2024, 12, 04, 10, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 29, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 28, 18, 00, 00),
                    //SplitEnd = new DateTime(2024, 11, 29, 09, 00, 00)
                });
            splitTemplate.Add(
                "2",
                new()
                {
                    SplitName = "2",
                    SplitStart = new DateTime(2024, 11, 29, 01, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 29, 10, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 29, 09, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 29, 18, 00, 00)
                });
            splitTemplate.Add(
                "3",
                new()
                {
                    SplitName = "3",
                    SplitStart = new DateTime(2024, 11, 29, 10, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 30, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 29, 18, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 30, 09, 00, 00)
                });
            splitTemplate.Add(
                "4",
                new()
                {
                    SplitName = "4",
                    SplitStart = new DateTime(2024, 11, 30, 01, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 30, 10, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 30, 09, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 30, 18, 00, 00)
                });
            splitTemplate.Add(
                "5",
                new()
                {
                    SplitName = "5",
                    SplitStart = new DateTime(2024, 11, 30, 10, 00, 01),
                    SplitEnd = new DateTime(2024, 12, 01, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 30, 18, 00, 01),
                    //SplitEnd = new DateTime(2024, 12, 01, 09, 00, 00)
                });
            splitTemplate.Add(
                "6",
                new()
                {
                    SplitName = "6",
                    SplitStart = new DateTime(2024, 12, 01, 01, 00, 01),
                    SplitEnd = new DateTime(2024, 12, 01, 10, 00, 00)

                    //SplitStart = new DateTime(2024, 12, 01, 09, 00, 01),
                    //SplitEnd = new DateTime(2024, 12, 01, 18, 00, 00)
                });
            splitTemplate.Add(
                "7",
                new()
                {
                    SplitName = "7",
                    SplitStart = new DateTime(2024, 12, 01, 10, 00, 01),
                    SplitEnd = new DateTime(2024, 12, 02, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 12, 01, 18, 00, 01),
                    //SplitEnd = new DateTime(2024, 12, 02, 09, 00, 00)
                });

            RawdataRecordManager.SplitTemplate = splitTemplate;
        }
    }
}
