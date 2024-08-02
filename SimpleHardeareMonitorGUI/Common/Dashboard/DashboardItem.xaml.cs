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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleHardwareMonitorGUI.Common.Dashboard
{
    /// <summary>
    /// GategoryItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DashboardItem : UserControl
    {
        public DashboardItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(DashboardItem), new PropertyMetadata("Header"));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(DashboardItem), new PropertyMetadata("0.0.00."));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty SymbolsUseProperty =
            DependencyProperty.Register("SymbolsUse", typeof(bool), typeof(DashboardItem), new PropertyMetadata(false));

        public bool SymbolsUse
        {
            get { return (bool)GetValue(SymbolsUseProperty); }
            set { SetValue(SymbolsUseProperty, value); }
        }

        public static readonly DependencyProperty SymbolsProperty =
            DependencyProperty.Register("Symbols", typeof(string), typeof(DashboardItem), new PropertyMetadata(""));

        public string Symbols
        {
            get { return (string)GetValue(SymbolsProperty); }
            set { SetValue(SymbolsProperty, value); }
        }

    }
}
