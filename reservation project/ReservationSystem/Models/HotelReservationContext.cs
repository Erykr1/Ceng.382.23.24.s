using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.Data
{
    public class HotelReservationContext : DbContext
    {
        public HotelReservationContext(DbContextOptions<HotelReservationContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }  
        public DbSet<Reservation> Reservations { get; set; }  
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Room entity configuration
            modelBuilder.Entity<Room>(entity =>  
            {
                entity.ToTable("rooms");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Capacity).HasColumnName("capacity");
                entity.Property(e => e.View).HasColumnName("view");

                entity.HasMany(e => e.Reservations)
                    .WithOne(r => r.Room)
                    .HasForeignKey(r => r.RoomId);
            });

            // Reservation entity configuration
            modelBuilder.Entity<Reservation>(entity =>  
            {
                entity.ToTable("reservations");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.RoomId).HasColumnName("roomid");
                entity.Property(e => e.UserName).HasColumnName("username");
                entity.Property(e => e.StartDate).HasColumnName("startdate");
                entity.Property(e => e.EndDate).HasColumnName("enddate");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior
            });

            // User entity configuration
            modelBuilder.Entity<User>(entity =>  
            {
                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserName).HasColumnName("username");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Gender).HasColumnName("gender");
                entity.Property(e => e.Age).HasColumnName("age");
            });
        }
    }
}
