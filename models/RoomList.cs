namespace ReservationSystem
{
    public class RoomList
{
    public List<Room> Rooms { get; set; }

    public RoomList()
    {
        Rooms = new List<Room>();  // Non-nullable özellik her zaman non-null bir liste ile başlatılır
    }
}
}