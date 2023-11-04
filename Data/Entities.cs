using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class Entities : DbContext
{
    public DbSet<Flight> Flights => Set<Flight>();

    //public DbSet<Flight> Flights { get; set; }

    public Entities(DbContextOptions options) : base(options)
    {
        //Flights.Add(new Flight(3));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>().HasKey(x => x.Id);

        modelBuilder.Entity<Flight>().OwnsMany(x => x.BookingList);

        base.OnModelCreating(modelBuilder);
    }

}
