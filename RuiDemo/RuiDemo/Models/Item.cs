using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace RuiDemo.Models
{
    public class Item : ReactiveObject
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        private bool _completed;
        public bool Completed
        {
            get => _completed;
            set => this.RaiseAndSetIfChanged(ref _completed, value);
        }
    }
}