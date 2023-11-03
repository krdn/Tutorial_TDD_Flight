
using System.Collections;
using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.Test;

public class FlightApplicationSpecifications
{
    [Theory(DisplayName = "부킹 여부 확인")]
    [InlineData("m@m.com", 2)]
    [InlineData("a@a.com", 2)]
    public void Books_flights(string passengerEmail, int numberOfSeats)
    {
        // Given
        var options = new DbContextOptionsBuilder<Entities>()
            .UseInMemoryDatabase("Flights")
            .Options;

        var entities = new Entities(options);

        var flight = new Flight(3);

        entities.Flights.Add(flight);

        var bookingService = new BookingService(entities: entities);

        // When
        bookingService.Book(new BookDto(
            flightId: flight.Id, passengerEmail, numberOfSeats));

        // Then
        bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
            new BookingRm(passengerEmail, numberOfSeats)
            );
    }
}

public class BookingService
{
    public Entities Entities { get; set; }

    public BookingService(Entities entities)
    {
        Entities = entities;
    }

    public void Book(BookDto bookDto)
    {
        var flight = Entities.Flights.Find(bookDto.FlightId);
        flight.Book(bookDto.PassengerEmail, bookDto.NumberOfSeats);
        Entities.SaveChanges();
    }

    public IEnumerable<BookingRm> FindBookings(Guid flightId)
    {
        return Entities.Flights
            .Find(flightId)
            .BookingList
            .Select(x => new BookingRm(
                x.Email, 
                x.NumberOfSeats)
            );

    }
}


public class BookDto
{
    public Guid FlightId { get; set; }
    public string PassengerEmail { get; set; }
    public int NumberOfSeats { get; set; }

    public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
    {
        FlightId = flightId;
        PassengerEmail = passengerEmail;
        NumberOfSeats = numberOfSeats;
    }
}

public class BookingRm
{
    public string PassengerEmail { get; set; }
    public int NumberOfSeats { get; set; }

    public BookingRm(string passengerEmail, int numberOfSeats)
    {
        PassengerEmail = passengerEmail;
        NumberOfSeats = numberOfSeats;
    }
}
