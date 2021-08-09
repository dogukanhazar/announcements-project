using System;
using System.ComponentModel.DataAnnotations;

namespace AnnouncementsApi.Models
{
    public class AnnouncementModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(5000)]
        public string Description { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedDate
        {
            get { return CreatedDateBuffer ?? DateTime.Now; }
            set { this.CreatedDateBuffer = value; }
        }
        public DateTime? ExpiredDate
        {
            get { return ExpiredDateBuffer ?? null; }
            set { this.ExpiredDateBuffer = value; }
        }

        private DateTime? CreatedDateBuffer = null;
        private DateTime? ExpiredDateBuffer = null;
    }
}