using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleHardwareMonitorGUI.common.dashboard
{
    /// <summary>
    /// GategoryItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DashboardItem : UserControl
    {
        private static readonly FrameworkPropertyMetadataOptions _frameworkPropertyMetadataOptions = FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure;

        public DashboardItem()
        {
            InitializeComponent();
        }
        //public static readonly DependencyProperty CategoryContentProperty
        //    = DependencyProperty.Register(
        //        nameof(CategoryContent),
        //        typeof(object),
        //        typeof(DashboardItem),
        //        new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        //public object? CategoryContent
        //{
        //    get { return GetValue(CategoryContentProperty); }
        //    set { SetValue(CategoryContentProperty, value); }
        //}

        public static readonly DependencyProperty HeaderTextProperty
            = DependencyProperty.Register(
                nameof(HeaderText),
                typeof(string),
                typeof(DashboardItem),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly DependencyProperty HeaderBackgroundProperty
            = DependencyProperty.Register(
                nameof(HeaderBackground),
                typeof(SolidColorBrush),
                typeof(DashboardItem),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public SolidColorBrush? HeaderBackground
        {
            get { return (SolidColorBrush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderHeightProperty
            = DependencyProperty.Register(
                nameof(HeaderHeight),
                typeof(double),
                typeof(DashboardItem),
                new FrameworkPropertyMetadata(30.0, _frameworkPropertyMetadataOptions));
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(DashboardItem), new PropertyMetadata("0"));
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
