namespace AppointmentManager.Repositories.EFGetStarted.DataBase
{
  using AppointmentManager.Domain.DBModel;
  using Microsoft.EntityFrameworkCore;
  using System;
  using System.Collections.Generic;

  public class AppointmentManagerContext : DbContext
  {
    public DbSet<Test> Test { get; set; }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<User> Users { get; set; }


  //  public DbSet<Blog> Blogs { get; set; }
   // public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public AppointmentManagerContext()
    {
      var folder = Environment.SpecialFolder.LocalApplicationData;
      var path = Environment.GetFolderPath(folder);
      DbPath = Path.Join(path, "blogging.db");
    }

    //   The following configures EF to create a Sqlite database file in the
    //   special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure the Appointment entity
      modelBuilder.Entity<Appointment>(entity =>
      {
        entity.HasKey(a => a.AppointmentId);

        entity.Property(a => a.AppointmentDate)
            .IsRequired();

        entity.Property(a => a.Description)
            .HasMaxLength(500);

        // Configure the relationship with User
        entity.HasOne(a => a.User)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull); // Set to null if user is deleted
      });

      // Configure the User entity
      modelBuilder.Entity<User>(entity =>
      {
        entity.HasKey(u => u.UserId);

        entity.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);
      });
    }

  }


}
//public class Blog
//{
//  public int BlogId { get; set; }
//  public string Url { get; set; }

//  public List<Post> Posts { get; } = new();
//}

//public class Post
//{
//  public int PostId { get; set; }
//  public string Title { get; set; }
//  public string Content { get; set; }

//  public int BlogId { get; set; }
//  public Blog Blog { get; set; }
//}


public class Test
{
  public int ID { get; set; }

}


//public class Appointment
//{
//  public int AppointmentId { get; set; }
//  public bool IsAvailable { get; set; }
//  public DateTime AppointmentDate { get; set; }
//  public string Description { get; set; }
//  public int? UserId { get; set; } // מזהה המשתמש שמקושר לתור
//  public User User { get; set; } // הקשר למשתמש
//}


