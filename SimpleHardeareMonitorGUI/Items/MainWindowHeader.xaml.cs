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
        Timer updateStatusTimer;
        private bool isDragging = false;
        private Point startPoint;

        public MainWindowHeader()
        {
            InitializeComponent();

            updateStatusTimer = new Timer(UpdateStatus, this, 0, 250);
            this.MouseLeftButtonDown += MainWindowHeader_MouseLeftButtonDown;
            this.MouseMove += MainWindowHeader_MouseMove;
            this.MouseLeftButtonUp += MainWindowHeader_MouseLeftButtonUp;
        }

        private readonly SolidColorBrush _logOnBrush = new SolidColorBrush(new Color() { R = 0, G = 128, B = 0, A = 60 });
        private readonly SolidColorBrush _logOffBrush = new SolidColorBrush(new Color() { R = 180, G = 83, B = 0, A = 60 });


        bool test = true;

        private void UpdateStatus(object? state)
        {
            try
            {
                // UI 스레드에서 작업 실행
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    // UI 업데이트 코드
                    //if (test)
                    //{
                    //    string temp = $"DataSave";
                    //    if (temp != logStatusTextBlock.Text)
                    //        logStatusTextBlock.Text = temp;
                    //    if (logStatusBorder.Background != _logOnBrush)
                    //        logStatusBorder.Background = _logOnBrush;
                    //}
                    //else
                    //{
                    //    string temp = $"DataDisSave";
                    //    if (temp != logStatusTextBlock.Text)
                    //        logStatusTextBlock.Text = temp;
                    //    if (logStatusBorder.Background != _logOffBrush)
                    //        logStatusBorder.Background = _logOffBrush;
                    //}

                    test = !test;
                }));
            }
            catch (TaskCanceledException /*ex*/)
            {
                // 작업이 취소된 경우의 처리
                //Console.WriteLine("The task was canceled: " + ex.Message);
            }
            catch (Exception /*ex*/)
            {
                // 기타 예외 처리
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static readonly DependencyProperty _saving =
            DependencyProperty.Register("Saving", typeof(bool), typeof(MainWindowHeader), new PropertyMetadata(false));
        public bool Saving
        {
            get { return (bool)GetValue(_saving); }
            set { SetValue(_saving, value); }
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
            if (lockButton.IsChecked is true)
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
            SettingData.Instance.WindowTitleLocked = true;
        }

        private void Lock_Unchecked(object sender, RoutedEventArgs e)
        {
            SettingData.Instance.WindowTitleLocked = false;
        }
    }
}
