using System;
using System.Runtime.Serialization;

namespace WpfApp.Models
{
    [DataContract]
    public class TodoItem
    {
        [DataMember]
        public Guid Id { get; set; } = Guid.NewGuid();

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public bool IsCompleted { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
