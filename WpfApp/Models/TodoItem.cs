using System;
using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp.Models
{
    [DataContract]
    public class TodoItem : ObservableObject
    {
        private bool _isCompleted;
        [DataMember]
        public Guid Id { get; set; } = Guid.NewGuid();

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }

        [DataMember]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataMember]
        public string ContactAssigned { get; set; }
    }
}
