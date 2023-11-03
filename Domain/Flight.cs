

namespace Domain;

public class Flight
{
    List<Booking> _bookingList = new();

    // 외부에서 변경하지 못하도록 설정함.
    public IEnumerable<Booking> BookingList => _bookingList;

    public int RemainingNUmberOfSeats { get; set; }

    public Guid Id { get; set; }

    [Obsolete("For EF")]
    Flight() { }

    public Flight(int seatCapacity)
    {
        RemainingNUmberOfSeats = seatCapacity;
    }

    public object? Book(string passengerEmail, int numberOfSeats)
    {
        if (RemainingNUmberOfSeats < numberOfSeats)
            return new OverbookingError();

        RemainingNUmberOfSeats -= numberOfSeats;

        _bookingList.Add(new Booking(passengerEmail, numberOfSeats));

        return null;

    }

    public object? CancelBooking(string passengerEmail, int numberOfSeats)
    {
        if (_bookingList.All(x => x.Email != passengerEmail))
            return new BookingNotFoundError();

        RemainingNUmberOfSeats += numberOfSeats;

        // 예약을 취소하면 예약 목록에서 삭제한다.
        // Database 엔티티 업데이트 여부를 위한 테스트 필요
        _bookingList.RemoveAll(x => x.Email == passengerEmail);

        return null;
    }
}
