using AnnouncementsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementsApi.Contexts
{
    public class AnnouncementContext : DbContext
    {
        public AnnouncementContext(DbContextOptions<AnnouncementContext> options) : base(options) { }

        public DbSet<AnnouncementModel> Announcements { get; set; }
    }
}