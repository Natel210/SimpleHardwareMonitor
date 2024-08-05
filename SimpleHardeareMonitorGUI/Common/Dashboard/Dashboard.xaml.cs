using System.Windows;
using System.Windows.Controls;

namespace SimpleHardwareMonitorGUI.Common.Dashboard
{
    /// <summary>
    /// Dashboard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ContentNameProperty =
            DependencyProperty.Register("ContentName", typeof(object), typeof(Dashboard), new PropertyMetadata(null));
        public object ContentName
        {
            get { return GetValue(ContentNameProperty); }
            set { SetValue(ContentNameProperty, value); }
        }
        public static readonly DependencyProperty Header1Property =
            DependencyProperty.Register("Header1", typeof(string), typeof(Dashboard), new PropertyMetadata("N/A"));
        public string Header1
        {
            get { return (string)GetValue(Header1Property); }
            set { SetValue(Header1Property, value); }
        }
        public static readonly DependencyProperty Value1Property =
            DependencyProperty.Register("Value1", typeof(string), typeof(Dashboard), new PropertyMetadata("000.0"));
        public string Value1
        {
            get { return (string)GetValue(Value1Property); }
            set { SetValue(Value1Property, value); }
        }
        public static readonly DependencyProperty SymbolsUse1Property =
            DependencyProperty.Register("SymbolsUse1", typeof(bool), typeof(Dashboard), new PropertyMetadata(false));
        public bool SymbolsUse1
        {
            get { return (bool)GetValue(SymbolsUse1Property); }
            set { SetValue(SymbolsUse1Property, value); }
        }
        public static readonly DependencyProperty Symbols1Property =
            DependencyProperty.Register("Symbols1", typeof(string), typeof(Dashboard), new PropertyMetadata("?"));
        public string Symbols1
        {
            get { return (string)GetValue(Symbols1Property); }
            set { SetValue(Symbols1Property, value); }
        }
        public static readonly DependencyProperty Header2Property =
            DependencyProperty.Register("Header2", typeof(string), typeof(Dashboard), new PropertyMetadata("N/A"));
        public string Header2
        {
            get { return (string)GetValue(Header2Property); }
            set { SetValue(Header2Property, value); }
        }
        public static readonly DependencyProperty Value2Property =
            DependencyProperty.Register("Value2", typeof(string), typeof(Dashboard), new PropertyMetadata("000.0"));
        public string Value2
        {
            get { return (string)GetValue(Value2Property); }
            set { SetValue(Value2Property, value); }
        }
        public static readonly DependencyProperty SymbolsUse2Property =
            DependencyProperty.Register("SymbolsUse2", typeof(bool), typeof(Dashboard), new PropertyMetadata(false));
        public bool SymbolsUse2
        {
            get { return (bool)GetValue(SymbolsUse2Property); }
            set { SetValue(SymbolsUse2Property, value); }
        }
        public static readonly DependencyProperty Symbols2Property =
            DependencyProperty.Register("Symbols2", typeof(string), typeof(Dashboard), new PropertyMetadata("?"));
        public string Symbols2
        {
            get { return (string)GetValue(Symbols2Property); }
            set { SetValue(Symbols2Property, value); }
        }
        public static readonly DependencyProperty Header3Property =
            DependencyProperty.Register("Header3", typeof(string), typeof(Dashboard), new PropertyMetadata("N/A"));
        public string Header3
        {
            get { return (string)GetValue(Header3Property); }
            set { SetValue(Header3Property, value); }
        }
        public static readonly DependencyProperty Value3Property =
            DependencyProperty.Register("Value3", typeof(string), typeof(Dashboard), new PropertyMetadata("000.0"));
        public string Value3
        {
            get { return (string)GetValue(Value3Property); }
            set { SetValue(Value3Property, value); }
        }
        public static readonly DependencyProperty SymbolsUse3Property =
            DependencyProperty.Register("SymbolsUse3", typeof(bool), typeof(Dashboard), new PropertyMetadata(false));
        public bool SymbolsUse3
        {
            get { return (bool)GetValue(SymbolsUse3Property); }
            set { SetValue(SymbolsUse3Property, value); }
        }
        public static readonly DependencyProperty Symbols3Property =
            DependencyProperty.Register("Symbols3", typeof(string), typeof(Dashboard), new PropertyMetadata("?"));
        public string Symbols3
        {
            get { return (string)GetValue(Symbols3Property); }
            set { SetValue(Symbols3Property, value); }
        }
    }
}
