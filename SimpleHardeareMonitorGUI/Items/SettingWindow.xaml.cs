using SimpleHardwareMonitorGUI.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static SimpleHardwareMonitorGUI.MainWindowViewmodel;

namespace SimpleHardwareMonitorGUI.Items
{
    /// <summary>
    /// SettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();

            //var enumValues = Enum.GetValues(typeof(ELoggingInterval)).Cast<ELoggingInterval>().Select(e => new
            //{
            //    Value = e,
            //    Group = GetEnumDescription(e)
            //});

            //var collectionView = new CollectionViewSource { Source = enumValues };
            //collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            //ComboBoxLoggingInterval.ItemsSource = collectionView.View;
        }

        public static readonly DependencyProperty SettingWindowLoggingIntervalProperty =
    DependencyProperty.Register("SettingWindowLogging", typeof(ELoggingInterval), typeof(SettingWindow), new PropertyMetadata(ELoggingInterval.m1));

        public ELoggingInterval SettingWindowLogging
        {
            get { return (ELoggingInterval)GetValue(SettingWindowLoggingIntervalProperty); }
            set { SetValue(SettingWindowLoggingIntervalProperty, value); }
        }
    }
}
