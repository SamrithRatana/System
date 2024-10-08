﻿using System.ComponentModel.DataAnnotations;

namespace ServiceMaintenance.Models
{
    public class Message
    {
        public int Id { get; set; } //object in database
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }
        public string UserID { get; set; }
        public string RecipientID { get; set; }  // Add this property
        public string FileUrl { get; set; } // Add this line
        public string AudioURL { get; set; } // Add this line
        public string VideoUrl { get; set; } // Add this property
        public virtual ApplicationUser Sender { get; set; }
        public Message()
        {
            When = DateTime.Now;
        }
    }
}
