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
        [ObservableProperty] 
        private bool isPanelOpen;

        public MemoCollection MemoCollection { get; } = new();
        public ICommand AddMemoCommand { get; }

        private Memo? _selectedMemo;
        public Memo? SelectedMemo
        {
            get => _selectedMemo;
            set
            {
                if(SetProperty(ref _selectedMemo, value))
                {
                    LoadMemoContent();
                }
            }
        }

        [ObservableProperty]
        private string _memoContent = "";

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
        private void LoadMemoContent()
        {
            string folderPath = @"E:\HoverSheet";
            if(_selectedMemo == null)
            {
                _memoContent = "";
                OnPropertyChanged("MemoContent");
                return;
            }

            string filePath = Path.Combine(folderPath, $"{_selectedMemo.Id}.txt");
            _memoContent = File.ReadAllText(filePath);
            OnPropertyChanged("MemoContent");
        }
    }
}
