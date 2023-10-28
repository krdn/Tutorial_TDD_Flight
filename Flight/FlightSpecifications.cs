using Domain;
using FluentAssertions;

namespace FlightTest;

public class FlightSpecifications
{
    [Theory(DisplayName = "��ŷ �¼��� ����")]
    [InlineData(3, 1, 2)]
    [InlineData(3, 2, 1)]
    [InlineData(3, 3, 0)]
    [InlineData(10, 6, 4)]
    public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats, int remainingNumberOfSeats)
    {
        var flight = new Flight(seatCapacity: seatCapacity);

        flight.Book("krdn.net@gmail.com", numberOfSeats);

        flight.RemainingNUmberOfSeats.Should().Be(remainingNumberOfSeats);
    }

    [Fact(DisplayName = "������ŷ ����")]
    public void Avoids_overbooking()
    {
        // Given
        var flight = new Flight(seatCapacity: 3);

        // When
        var error = flight.Book("krdn.net@gmail.com.", 4);

        // Then
        error.Should().BeOfType<OverbookingError>();

    }

    [Fact(DisplayName = "���� ����")]
    public void Books_flights_successfully()
    {
        // Given
        var flight = new Flight(seatCapacity: 3);

        // When
        var error = flight.Book("krdn.net@gmail.com.", 1);

        // Then
        error.Should().BeNull();
    }

    [Fact(DisplayName = "��ŷ ���")]
    public void Remembers_bookings()
    {
        // Given
        var flight = new Flight(seatCapacity: 150);

        // When
        flight.Book(passengerEmail: "a@b.com", numberOfSeats: 4);

        flight.BookingList.Should().ContainEquivalentOf(new Booking("a@b.com", 4));
        
    }

}