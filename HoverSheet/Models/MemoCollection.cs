using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace HoverSheet.Models
{
    public class MemoCollection
    {
        public ObservableCollection<Memo> Memos { get; set; } = new ObservableCollection<Memo>();

        public Memo AddMemo(string content)
        {
            var memo = new Memo
            {
                Id = Guid.NewGuid(),
                Content = content
            };
            Memos.Add(memo);
            return memo;
        }
        public void RemoveMemo(Guid id)
        {
            var memo = Memos.FirstOrDefault(m => m.Id == id);
            if (memo != null)
            {
                Memos.Remove(memo);
            }
        }
        public Memo? GetMemo(Guid id)
        {
            return Memos.FirstOrDefault(m => m.Id == id);
        }
        public void UpdateMemo(Guid id, string newContent)
        {
            var memo = Memos.FirstOrDefault(m => m.Id == id);
            if (memo != null)
            {
                memo.Content = newContent;
            }
        }   
    }
}
