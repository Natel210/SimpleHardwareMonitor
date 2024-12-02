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
    }

    public partial class PathPanel
    {

    }
}
