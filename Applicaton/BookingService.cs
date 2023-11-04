using Data;

namespace Application;

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

    public void CancelBooking(CancelBookingDto cancelBookingDto)
    {
        var flight = Entities.Flights.Find(cancelBookingDto.FlightId);
        flight.CancelBooking(cancelBookingDto.PassengerEmail, cancelBookingDto.NumberOfSeats);
        Entities.SaveChanges();
    }

    public object GetRemainingNumberOfSeatsFor(Guid fliGuid)
    {
        return Entities.Flights.Find(fliGuid).RemainingNUmberOfSeats;
    }
}