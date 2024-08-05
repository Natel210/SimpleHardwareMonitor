
using SimpleHardwareMonitorGUI.Common;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace SimpleHardwareMonitorGUI.Items
{
    /// <summary>
    /// MainWindowHeader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindowHeader : UserControl
    {
        private bool isDragging = false;
        private Point startPoint;

        public MainWindowHeader()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += MainWindowHeader_MouseLeftButtonDown;
            this.MouseMove += MainWindowHeader_MouseMove;
            this.MouseLeftButtonUp += MainWindowHeader_MouseLeftButtonUp;
        }

        public static readonly DependencyProperty _logging =
            DependencyProperty.Register("Logging", typeof(bool), typeof(MainWindowHeader), new PropertyMetadata(false));
        public bool Logging {
            get { return (bool)GetValue(_logging); }
            set { SetValue(_logging, value); }
        }
        private bool _titleLocked = false;
        private bool TitleLocked
        {
            get => _titleLocked;
            set { _titleLocked = value; SetLockedItems(value); }
        }
        private void SetLockedItems(bool boolean)
        {
            loggingButton.IsEnabled = !boolean;
            settingButton.IsEnabled = !boolean;
            minimizationButton.IsEnabled = !boolean;
            exitButton.IsEnabled = !boolean;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MainWindowHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(this);
            this.CaptureMouse();
        }
        private void MainWindowHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (TitleLocked is true)
                return;
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(this);
                var parentWindow = Window.GetWindow(this);

                if (parentWindow != null)
                {
                    double offsetX = currentPoint.X - startPoint.X;
                    double offsetY = currentPoint.Y - startPoint.Y;

                    parentWindow.Left += offsetX;
                    parentWindow.Top += offsetY;
                }
            }
        }
        private void MainWindowHeader_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            this.ReleaseMouseCapture();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Lock_Checked(object sender, RoutedEventArgs e)
        {
            TitleLocked = true;
        }

        private void Lock_Unchecked(object sender, RoutedEventArgs e)
        {
            TitleLocked = false;
        }

        private void Logging_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Logging_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
