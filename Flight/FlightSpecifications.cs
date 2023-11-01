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

        // Then
        flight.BookingList.Should().ContainEquivalentOf(new Booking("a@b.com", 4));
    }

    [Theory(DisplayName = "������ ����ϸ� �¼��� Ȯ��")]
    [InlineData(3, 1, 1, 3)]
    [InlineData(3, 2, 1, 2)]
    [InlineData(3, 3, 1, 1)]
    [InlineData(7, 4, 3, 6)]
    public void Canceling_bookings_frees_up_the_seats(
        int initialSeatCapacity,
        int numberOfSeatsToBook,
        int numberOfSeatsToCancel,
        int expectedRemainingNumberOfSeats)
    {
        // Given
        var flight = new Flight(initialSeatCapacity);
        flight.Book(passengerEmail: "a@b.com", numberOfSeats: numberOfSeatsToBook);

        // When
        flight.CancelBooking(passengerEmail: "a@b.com", numberOfSeats: numberOfSeatsToCancel);

        // Then
        flight.RemainingNUmberOfSeats.Should().Be(expectedRemainingNumberOfSeats);
    }

    [Fact(DisplayName = "���� ���� ���� �°� ���� ��� �ȵ�")]
    public void Doesnt_cancel_bookings_for_passengers_who_have_not_booked()
    {
        // Given
        var flight = new Flight(3);

        // When
        var error = flight.CancelBooking(passengerEmail: "a@b.com", numberOfSeats: 2);

        // Then
        error.Should().BeOfType<BookingNotFoundError>();
    }

    [Fact(DisplayName = "���� ��� ������ null ��ȯ")]
    public void Returns_null_when_cancelling_a_booking_successfully()
    {
        // Given
        var flight = new Flight(3);

        // When
        flight.Book(passengerEmail: "a@b.com", numberOfSeats: 1);

        var error = flight.CancelBooking(passengerEmail: "a@b.com", numberOfSeats: 1);

        // Then
        error.Should().BeNull();
    }




}