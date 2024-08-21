using SimpleHardwareMonitorGUI.rawdata;
using System.Windows;


namespace SimpleHardwareMonitorGUI.Setting
{
    /// <summary>
    /// SettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();

            //var enumValues = Enum.GetValues(typeof(ERawDataInterval)).Cast<ERawDataInterval>().Select(e => new
            //{
            //    Value = e,
            //    Group = GetEnumDescription(e)
            //});

            //var collectionView = new CollectionViewSource { Source = enumValues };
            //collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            //ComboBoxLoggingInterval.ItemsSource = collectionView.View;



        }

        public static readonly DependencyProperty SettingWindowLoggingIntervalProperty =
    DependencyProperty.Register("SettingWindowLogging", typeof(ERawDataInterval), typeof(SettingWindow), new PropertyMetadata(ERawDataInterval.m1));

        public ERawDataInterval SettingWindowLogging
        {
            get { return (ERawDataInterval)GetValue(SettingWindowLoggingIntervalProperty); }
            set { SetValue(SettingWindowLoggingIntervalProperty, value); }
        }
    }
}
