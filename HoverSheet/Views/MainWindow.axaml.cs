using Avalonia.Controls;
using Avalonia.Input;

namespace HoverSheet.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

    }
}