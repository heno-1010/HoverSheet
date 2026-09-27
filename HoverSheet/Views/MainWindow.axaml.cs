using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

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
    }
}