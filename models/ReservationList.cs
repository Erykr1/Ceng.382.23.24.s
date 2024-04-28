namespace ReservationSystem
{
    public class ReservationList
{
    public List<Reservation> Reservations { get; set; }

    public ReservationList()
    {
        Reservations = new List<Reservation>();
    }
}
}