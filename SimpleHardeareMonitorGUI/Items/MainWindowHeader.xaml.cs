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
using static System.Net.Mime.MediaTypeNames;

namespace SimpleHardwareMonitorGUI.Items
{
    /// <summary>
    /// MainWindowHeader.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindowHeader : UserControl
    {
        Timer updateStatusTimer;

        public MainWindowHeader()
        {
            InitializeComponent();

            updateStatusTimer = new Timer(UpdateStatus, this, 0, 250);

        }

        private readonly SolidColorBrush _monitoring_OK_Brush = new SolidColorBrush(new Color() { R = 0, G = 128, B = 0, A = 60 });
        private readonly SolidColorBrush _monitoring_Wait_Brush = new SolidColorBrush(new Color() { R = 180, G = 83, B = 0, A = 60 });


        bool test = true;

        private void UpdateStatus(object? state)
        {
            try
            {
                // UI 스레드에서 작업 실행
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    // UI 업데이트 코드
                    if (test)
                    {
                        string temp = $"LOG ON";
                        if (temp != logStatusTextBlock.Text)
                            logStatusTextBlock.Text = temp;
                        if (logStatusBorder.Background != _monitoring_OK_Brush)
                            logStatusBorder.Background = _monitoring_OK_Brush;
                    }
                    else
                    {
                        string temp = $"LOG OFF";
                        if (temp != logStatusTextBlock.Text)
                            logStatusTextBlock.Text = temp;
                        if (logStatusBorder.Background != _monitoring_Wait_Brush)
                            logStatusBorder.Background = _monitoring_Wait_Brush;
                    }

                    test = !test;
                }));
            }
            catch (TaskCanceledException ex)
            {
                // 작업이 취소된 경우의 처리
                //Console.WriteLine("The task was canceled: " + ex.Message);
            }
            catch (Exception ex)
            {
                // 기타 예외 처리
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
