using System;

namespace Producer.Models
{
    public class CustomMessage
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }

        public CustomMessage(int id, string description)
        {
            Id = id;
            Description = description;
            CreatedOn = DateTime.Now;
        }
    }
}