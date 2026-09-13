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

        private string _folderPath = @"E:\HoverSheet";

        public MainWindowViewModel()
        {
            AddMemoCommand = new RelayCommand(AddMemo);
            LoadMemos();
        }
        private void AddMemo()
        {
            var memo = MemoCollection.AddMemo("");

            string filePath = Path.Combine(_folderPath, $"{memo.Id}.txt");
            memo.Content = Path.GetFileName(filePath);
            File.Create(filePath).Dispose();
        }
        private void LoadMemos()
        {
            foreach (var filePath in Directory.GetFiles(_folderPath, "*.txt"))
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
            if(_selectedMemo == null)
            {
                _memoContent = "";
                OnPropertyChanged("MemoContent");
                return;
            }

            string filePath = Path.Combine(_folderPath, $"{_selectedMemo.Id}.txt");
            _memoContent = File.ReadAllText(filePath);
            OnPropertyChanged("MemoContent");
        }
        partial void OnMemoContentChanged(string value)
        {
            string filePath = Path.Combine(_folderPath, $"{_selectedMemo.Id}.txt");
            File.WriteAllText(filePath, value);
        }
    }
}
