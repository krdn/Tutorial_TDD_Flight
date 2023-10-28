
namespace Domain;

public class Flight
{
    private List<Booking> _bookingList = new();

    // 외부에서 변경하지 못하도록 설정함.
    public IEnumerable<Booking> BookingList => _bookingList;

    //public List<Booking> BookingList { get; set; } = new List<Booking>();

    public int RemainingNUmberOfSeats { get; set; }

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
}
