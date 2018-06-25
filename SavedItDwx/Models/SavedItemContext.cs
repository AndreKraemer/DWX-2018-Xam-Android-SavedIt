using System.IO;
using Microsoft.EntityFrameworkCore;

namespace SavedItDwx.Models
{
    public class SavedItemContext : DbContext
    {
        private readonly string _path;

        public SavedItemContext()
        {
            var docPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            _path = Path.Combine(docPath, "saveditemsdwx.db");
        }
        public DbSet<SavedItem> SavedItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Filename={_path}");
        }
    }
}