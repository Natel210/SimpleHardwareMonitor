using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleHardwareMonitorGUI.Common.Header
{
    public partial class WindowHeader : UserControl
    {
        public WindowHeader()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += MainWindowHeader_MouseLeftButtonDown;
            this.MouseMove += MainWindowHeader_MouseMove;
            this.MouseLeftButtonUp += MainWindowHeader_MouseLeftButtonUp;
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(WindowHeader), new PropertyMetadata(""));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty AddOnItemProperty =
            DependencyProperty.Register("AddOnItem", typeof(object), typeof(WindowHeader), new PropertyMetadata(null));
        public object AddOnItem
        {
            get { return GetValue(AddOnItemProperty); }
            set { SetValue(AddOnItemProperty, value); }
        }

        public static readonly DependencyProperty WindowUnlockedProperty =
            DependencyProperty.Register("WindowUnlocked", typeof(bool), typeof(WindowHeader), new PropertyMetadata(true));
        public bool WindowUnlocked
        {
            get { return (bool)GetValue(WindowUnlockedProperty); }
            set { SetValue(WindowUnlockedProperty, value); }
        }

        public static readonly DependencyProperty ShowMinimizeProperty =
            DependencyProperty.Register("ShowMinimize", typeof(Visibility), typeof(WindowHeader), new PropertyMetadata(Visibility.Visible));
        public Visibility ShowMinimize
        {
            get { return (Visibility)GetValue(ShowMinimizeProperty); }
            set { SetValue(ShowMinimizeProperty, value); }
        }

        public static readonly DependencyProperty ShowToggleMaximizeRestoreProperty =
            DependencyProperty.Register("ShowToggleMaximizeRestore", typeof(Visibility), typeof(WindowHeader), new PropertyMetadata(Visibility.Visible));
        public Visibility ShowToggleMaximizeRestore
        {
            get { return (Visibility)GetValue(ShowToggleMaximizeRestoreProperty); }
            set { SetValue(ShowToggleMaximizeRestoreProperty, value); }
        }

        public static readonly DependencyProperty ShowCloseProperty =
            DependencyProperty.Register("ShowClose", typeof(Visibility), typeof(WindowHeader), new PropertyMetadata(Visibility.Visible));
        public Visibility ShowClose
        {
            get { return (Visibility)GetValue(ShowCloseProperty); }
            set { SetValue(ShowCloseProperty, value); }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.WindowState = WindowState.Minimized;
        }

        private void ToggleMaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                if (parentWindow.WindowState == WindowState.Maximized)
                {
                    parentWindow.WindowState = WindowState.Normal;
                }
                else
                {
                    parentWindow.WindowState = WindowState.Maximized;
                }
                UpdateMaximizeRestoreButton();
            }
        }

        private void UpdateMaximizeRestoreButton()
        {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                var mask = ToggleMaxRestoreButton.Template.FindName("toggleMaxRestoreMask", ToggleMaxRestoreButton) as Border;
                var overlayMask = ToggleMaxRestoreButton.Template.FindName("toggleMaxRestoreOverlayMask", ToggleMaxRestoreButton) as Border;

                if (mask != null && overlayMask != null)
                {
                    if (parentWindow.WindowState == WindowState.Maximized)
                    {
                        mask.OpacityMask = (Brush)FindResource("restoreImage");
                        overlayMask.OpacityMask = (Brush)FindResource("restoreImage");
                    }
                    else
                    {
                        mask.OpacityMask = (Brush)FindResource("maximizeImage");
                        overlayMask.OpacityMask = (Brush)FindResource("maximizeImage");
                    }
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Close();
        }

        private bool isDragging = false;
        private Point startPoint;

        private void MainWindowHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(this);
            this.CaptureMouse();
        }

        private void MainWindowHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (WindowUnlocked is false)
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
            // 설정 버튼 클릭 시 처리할 내용
        }
    }
}
