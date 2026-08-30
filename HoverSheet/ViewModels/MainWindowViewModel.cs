using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HoverSheet.Models;
using System;
using System.IO;
using System.Windows.Input;

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
            LoadMemos();
        }
        private void AddMemo()
        {
            var memo = MemoCollection.AddMemo("New Memo");

            string folderPath = @"E:\HoverSheet";
            string filePath = Path.Combine(folderPath, $"{memo.Id}.txt");

            File.Create(filePath).Dispose();
        }
        private void LoadMemos()
        {
            string folderPath = @"E:\HoverSheet";

            foreach (var filePath in Directory.GetFiles(folderPath, "*.txt"))
            {
                var memo = new Memo
                {
                    Id = Guid.Parse(Path.GetFileNameWithoutExtension(filePath)),
                    Content = Path.GetFileName(filePath)
                };

                MemoCollection.Memos.Add(memo);
            }
        }
    }
}
