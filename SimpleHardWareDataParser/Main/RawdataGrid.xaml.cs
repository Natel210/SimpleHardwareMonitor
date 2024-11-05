using SimpleHardWareDataParser.Rawdata;
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

namespace SimpleHardWareDataParser.Main
{
    /// <summary>
    /// RawdataGrid.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RawdataGrid : UserControl
    {
        public RawdataGrid()
        {
            InitializeComponent();
            PART_TabItem.ItemsSource = null;
            PART_TabItem.Items.Clear();
            foreach (var key in RawdataRecordManager._rawdataSplitInfo.Keys)
            {
                //var tabItem = new TabItem
                //{
                //    Header = key // 탭의 제목으로 key를 설정
                //};

                //// 페이지나 유저 컨트롤을 생성하여 탭의 Content로 추가합니다.
                //// 예시로 UserControlExample이라는 유저 컨트롤을 추가
                //var userControl = new RawdataGrid(); // UserControlExample은 원하는 컨트롤로 대체 가능
                //userControl.dataGrid.ItemsSource = RawdataRecordManager.Data[key].Values;
                //// 또는 특정 페이지를 추가할 수도 있습니다.
                //// var page = new PageExample(); // PageExample은 원하는 페이지로 대체 가능

                //var frame = new Frame();
                //frame.Content = userControl;
                //tabItem.Content = frame; // 또는 page로 설정 가능
                //PART_TabItem.Items.Add(tabItem);

                PART_TabItem.ItemsSource = RawdataRecordManager._rawdataSplitInfo.Keys;
                PART_TabItem.SelectionChanged += (object sender, SelectionChangedEventArgs e) => {
                    if (PART_TabItem.SelectedItem.ToString() is string selectItemName)
                    {
                        dataGrid.ItemsSource = RawdataRecordManager.Data[selectItemName].Values; ;
                    }
                };
            }
        }
    }
}
