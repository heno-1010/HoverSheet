using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoverSheet.Models
{
    internal class MemoCollection
    {
        public List<Memo> Memos { get; set; } = new List<Memo>();

        public void AddMemo(string content)
        {
            var memo = new Memo
            {
                Id = Guid.NewGuid(),
                Content = content
            };
            Memos.Add(memo);
        }
        public void RemoveMemo(Guid id)
        {
            var memo = Memos.Find(m => m.Id == id);
            if (memo != null)
            {
                Memos.Remove(memo);
            }
        }
        public Memo? GetMemo(Guid id)
        {
            return Memos.Find(m => m.Id == id);
        }
        public void UpdateMemo(Guid id, string newContent)
        {
            var memo = Memos.Find(m => m.Id == id);
            if (memo != null)
            {
                memo.Content = newContent;
            }
        }   
    }
}
