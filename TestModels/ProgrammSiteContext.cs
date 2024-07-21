
using Microsoft.EntityFrameworkCore;

namespace test1.TestModels;

public class ProgrammSiteContext : DbContext
{

    public ProgrammSiteContext(DbContextOptions<ProgrammSiteContext> options)
        : base(options)
    {
        
    }

    public DbSet<Chapter> Chapters { get; set; }

    public DbSet<Lang> Langs { get; set; }

    public DbSet<Section> Sections { get; set; }

    public DbSet<Tutorial> Tutorials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=localhost;user=vadimkirpikov;password=texus-find12345VadQWE#;database=programm_site;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Lang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Preview).HasColumnName("preview");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e=>e.Url).HasComment("url");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LangId).HasColumnName("lang_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Preview).HasColumnName("preview");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e=>e.Url).HasComment("url");
        });

        modelBuilder.Entity<Tutorial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Url).HasColumnName("url");
        });
        
    }
}
