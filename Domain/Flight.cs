
namespace Domain;

public class Flight
{
    public int RemainingNUmberOfSeats { get; set; }

    public Flight(int seatCapacity)
    {
        RemainingNUmberOfSeats = seatCapacity;
    }

    public object? Book(string passengerEmail, int numberOfSeats)
    {
        if (RemainingNUmberOfSeats < numberOfSeats)
        {
            return new OverbookingError();
        }

        RemainingNUmberOfSeats -= numberOfSeats;
        return null;
    }
}
