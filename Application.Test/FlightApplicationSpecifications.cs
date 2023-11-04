
using System.Collections;
using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.Test;

public class FlightApplicationSpecifications
{
    readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>()
        .UseInMemoryDatabase("Flights")
        .Options);

    readonly BookingService bookingService;

    public FlightApplicationSpecifications()
    {
            bookingService = new BookingService(entities: entities);
    }

    [Theory(DisplayName = "��ŷ ���� Ȯ��")]
    [InlineData("m@m.com", 2)]
    [InlineData("a@a.com", 2)]
    public void Books_flights(string passengerEmail, int numberOfSeats)
    {
        // Given
        var flight = new Flight(3);

        entities.Flights.Add(flight);

        // When
        bookingService.Book(new BookDto(
            flightId: flight.Id, passengerEmail, numberOfSeats));

        // Then
        bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
            new BookingRm(passengerEmail, numberOfSeats)
            );
    }

    [Theory(DisplayName = "��ŷ ���")]
    [InlineData(3)]
    [InlineData(10)]
    public void Cancels_booking(int initialCapacity)
    {
        // Given
        var flight = new Flight(initialCapacity);
        entities.Flights.Add(flight);

        bookingService.Book(new BookDto(
            flightId: flight.Id,
            passengerEmail: "m@m.com",
            numberOfSeats: 2)
        );

        // When
        bookingService.CancelBooking(
            new CancelBookingDto(
                flightId: flight.Id,
                passengerEmail: "m@m.com",
                numberOfSeats: 2)
            );

        // Then : ��� �� ���� �¼��� �����Ǿ�� �Ѵ�.
        bookingService.GetRemainingNumberOfSeatsFor(flight.Id)
            .Should().Be(initialCapacity);


    }
}
