using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using HoverSheet.ViewModels;

namespace HoverSheet.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainGrid.AddHandler(
                PointerPressedEvent,
                OnPointerPressed,
                Avalonia.Interactivity.RoutingStrategies.Tunnel);
        }
        private void BarEntered(object? sender, PointerEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.IsPanelOpen = true;
            }
        }

        private void PanelExited(object? sender, PointerEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.IsPanelOpen = false;
            }
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            var properties = e.GetCurrentPoint(MainGrid).Properties;

            if (properties.IsMiddleButtonPressed)
            {
                System.Diagnostics.Debug.WriteLine("中クリック");
                OverlayScreen.IsVisible = !OverlayScreen.IsVisible;
                e.Handled = true;
            }
        }
        private void MainGrid_PointerWheelChanged(object? sender, PointerWheelEventArgs e)
        {
            if (!OverlayScreen.IsVisible)
                return;

            System.Diagnostics.Debug.WriteLine($"ホイール回転量: {e.Delta.Y}");

            if(DataContext is MainWindowViewModel viewModel)
            {
                if (e.Delta.Y > 0)
                {
                    System.Diagnostics.Debug.WriteLine("ホイール上回転");
                    viewModel.SelectPreviousMemo();
                }
                else if (e.Delta.Y < 0)
                {
                    System.Diagnostics.Debug.WriteLine("ホイール下回転");
                    viewModel.SelectNextMemo();
                }
            }

            e.Handled = true;
        }
    }
}