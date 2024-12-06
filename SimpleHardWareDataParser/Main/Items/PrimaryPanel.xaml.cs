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

namespace SimpleHardWareDataParser.Main.Items
{
    /// <summary>
    /// PrimaryPanel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PrimaryPanel : UserControl
    {
        private static readonly FrameworkPropertyMetadataOptions _frameworkPropertyMetadataOptions = FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure;

        public static readonly DependencyProperty RootDirectoryProperty
            = DependencyProperty.Register(
                nameof(RootDirectory),
                typeof(string),
                typeof(PrimaryPanel),
                new FrameworkPropertyMetadata(null, _frameworkPropertyMetadataOptions));
        public string RootDirectory
        {
            get => (string)GetValue(RootDirectoryProperty);
            set => SetValue(RootDirectoryProperty, value);
        }



        public PrimaryPanel()
        {
            InitializeComponent();
        }

        

    }
}
