using CommunityToolkit.Mvvm.ComponentModel;
using HoverSheet.Models;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.IO;

namespace HoverSheet.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private bool isPanelOpen;
        public MemoCollection MemoCollection { get; } = new();
        public ICommand AddMemoCommand { get; }

        public MainWindowViewModel()
        {
            AddMemoCommand = new RelayCommand(AddMemo);
        }
        private void AddMemo()
        {
            var memo = MemoCollection.AddMemo("New Memo");

            string folderPath = @"E:\HoverSheet";
            string filePath = Path.Combine(folderPath, $"{memo.Id}.txt");

            File.Create(filePath).Dispose();
        }
    }
}
