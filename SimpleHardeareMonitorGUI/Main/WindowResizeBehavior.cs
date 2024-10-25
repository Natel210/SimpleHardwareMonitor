using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace SimpleHardwareMonitorGUI.Main
{
    public class WindowResizeBehavior : Behavior<Window>
    {
        private const int ResizeBorderThickness = 10;  // 크기 조절 영역 두께
        private bool isResizing = false;
        private Point startPoint;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var window = sender as Window;
            if (window == null || isResizing)
                return;

            // 마우스 위치에 따라 커서 변경
            Point position = e.GetPosition(window);
            if (position.X <= ResizeBorderThickness && position.Y <= ResizeBorderThickness)
                window.Cursor = Cursors.SizeNWSE;
            else if (position.X >= window.ActualWidth - ResizeBorderThickness && position.Y <= ResizeBorderThickness)
                window.Cursor = Cursors.SizeNESW;
            else if (position.X <= ResizeBorderThickness && position.Y >= window.ActualHeight - ResizeBorderThickness)
                window.Cursor = Cursors.SizeNESW;
            else if (position.X >= window.ActualWidth - ResizeBorderThickness && position.Y >= window.ActualHeight - ResizeBorderThickness)
                window.Cursor = Cursors.SizeNWSE;
            else if (position.X <= ResizeBorderThickness)
                window.Cursor = Cursors.SizeWE;
            else if (position.X >= window.ActualWidth - ResizeBorderThickness)
                window.Cursor = Cursors.SizeWE;
            else if (position.Y <= ResizeBorderThickness)
                window.Cursor = Cursors.SizeNS;
            else if (position.Y >= window.ActualHeight - ResizeBorderThickness)
                window.Cursor = Cursors.SizeNS;
            else
                window.Cursor = Cursors.Arrow;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = sender as Window;
            if (window == null)
                return;

            // 마우스 커서가 크기 조절 모드일 때, 창 크기 조절 시작
            if (window.Cursor != Cursors.Arrow)
            {
                isResizing = true;
                startPoint = e.GetPosition(window);
                window.CaptureMouse();
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var window = sender as Window;
            if (window == null)
                return;

            isResizing = false;
            window.ReleaseMouseCapture();
        }

        // 창 크기 변경 로직을 여기 추가
    }
}
