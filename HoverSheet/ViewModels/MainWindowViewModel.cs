using CommunityToolkit.Mvvm.ComponentModel;

namespace HoverSheet.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private bool isPanelOpen;
    }
}
