using System.Windows;
using System.Windows.Controls;

namespace SimpleHardwareMonitorGUI.Common.Dashboard
{
    /// <summary>
    /// DashboardBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DashboardBar : UserControl
    {
        public DashboardBar()
        {
            InitializeComponent();
        }

        private static readonly FrameworkPropertyMetadataOptions _frameworkPropertyMetadataOptions = FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure;

        public static readonly DependencyProperty CategoryContentProperty
            = DependencyProperty.Register(
                nameof(CategoryContent),
                typeof(object),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public object CategoryContent
        {
            get { return GetValue(CategoryContentProperty); }
            set { SetValue(CategoryContentProperty, value); }
        }

        public static readonly DependencyProperty Item1_HeaderProperty
            = DependencyProperty.Register(
                nameof(Item1_Header),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? Item1_Header
        {
            get { return (string)GetValue(Item1_HeaderProperty); }
            set { SetValue(Item1_HeaderProperty, value); }
        }

        public static readonly DependencyProperty Item1_ValueProperty
            = DependencyProperty.Register(
                nameof(Item1_Value),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata("", _frameworkPropertyMetadataOptions));
        public string? Item1_Value
        {
            get { return (string)GetValue(Item1_ValueProperty); }
            set { SetValue(Item1_ValueProperty, value); }
        }

        public static readonly DependencyProperty Item1_SymbolsUseProperty
            = DependencyProperty.Register(
                nameof(Item1_SymbolsUse),
                typeof(bool),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(false, _frameworkPropertyMetadataOptions));
        public bool Item1_SymbolsUse
        {
            get { return (bool)GetValue(Item1_SymbolsUseProperty); }
            set { SetValue(Item1_SymbolsUseProperty, value); }
        }

        public static readonly DependencyProperty Item1_SymbolsProperty
            = DependencyProperty.Register(
                nameof(Item1_Symbols),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? Item1_Symbols
        {
            get { return (string)GetValue(Item1_SymbolsProperty); }
            set { SetValue(Item1_SymbolsProperty, value); }
        }

        public static readonly DependencyProperty Item2_HeaderProperty
            = DependencyProperty.Register(
                nameof(Item2_Header),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? Item2_Header
        {
            get { return (string)GetValue(Item2_HeaderProperty); }
            set { SetValue(Item2_HeaderProperty, value); }
        }

        public static readonly DependencyProperty Item2_ValueProperty
            = DependencyProperty.Register(
                nameof(Item2_Value),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? Item2_Value
        {
            get { return (string)GetValue(Item2_ValueProperty); }
            set { SetValue(Item2_ValueProperty, value); }
        }

        public static readonly DependencyProperty Item2_SymbolsUseProperty
            = DependencyProperty.Register(
                nameof(Item2_SymbolsUse),
                typeof(bool),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(false, _frameworkPropertyMetadataOptions));
        public bool Item2_SymbolsUse
        {
            get { return (bool)GetValue(Item2_SymbolsUseProperty); }
            set { SetValue(Item2_SymbolsUseProperty, value); }
        }

        public static readonly DependencyProperty Item2_SymbolsProperty
            = DependencyProperty.Register(
                nameof(Item2_Symbols),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? Item2_Symbols
        {
            get { return (string)GetValue(Item2_SymbolsProperty); }
            set { SetValue(Item2_SymbolsProperty, value); }
        }

        public static readonly DependencyProperty Item3_HeaderProperty =
            DependencyProperty.Register(
                nameof(Item3_Header),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? Item3_Header
        {
            get { return (string)GetValue(Item3_HeaderProperty); }
            set { SetValue(Item3_HeaderProperty, value); }
        }

        public static readonly DependencyProperty Item3_ValueProperty
            = DependencyProperty.Register(
                nameof(Item3_Value),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? Item3_Value
        {
            get { return (string)GetValue(Item3_ValueProperty); }
            set { SetValue(Item3_ValueProperty, value); }
        }

        public static readonly DependencyProperty Item3_SymbolsUseProperty
            = DependencyProperty.Register(
                nameof(Item3_SymbolsUse),
                typeof(bool),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(false, _frameworkPropertyMetadataOptions));
        public bool Item3_SymbolsUse
        {
            get { return (bool)GetValue(Item3_SymbolsUseProperty); }
            set { SetValue(Item3_SymbolsUseProperty, value); }
        }

        public static readonly DependencyProperty Item3_SymbolsProperty
            = DependencyProperty.Register(
                nameof(Item3_Symbols),
                typeof(string),
                typeof(DashboardBar),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string? Item3_Symbols
        {
            get { return (string)GetValue(Item3_SymbolsProperty); }
            set { SetValue(Item3_SymbolsProperty, value); }
        }
    }
}
