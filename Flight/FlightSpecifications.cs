using Domain;
using FluentAssertions;

namespace FlightTest;

public class FlightSpecifications
{
    [Fact(DisplayName = "부킹 좌석수 감소")]
    public void Booking_reduces_the_number_of_seats()
    {
        var flight = new Flight(seatCapacity: 3);

        flight.Book("krdn.net@gmail.com", 1);

        flight.RemainingNUmberOfSeats.Should().Be(2);
    }

    [Fact(DisplayName = "오버부킹 방지")]
    public void Avoids_overbooking()
    {
        // Given
        var flight = new Flight(seatCapacity: 3);

        // When
        var error = flight.Book("krdn.net@gmail.com.", 4);

        // Then
        error.Should().BeOfType<OverbookingError>();

    }
}