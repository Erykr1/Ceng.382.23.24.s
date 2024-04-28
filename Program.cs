using System;
using System.Linq;

namespace ReservationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var roomRepository = new RoomRepository("Data.json"); //roomhandler, iroomrepository'ye bağlı.İşlemler roomrepository ile sağlanıyor. DI örneği.
            var reservationRepository = new ReservationRepository("reservation.json"); //DI
            var logger = new FileLogger("log.json"); //DI
            var reservationHandler = new ReservationHandler(reservationRepository, roomRepository, logger); //DI

            var room = roomRepository.GetRooms().FirstOrDefault(r => r.RoomId == "016");
            var reservationAddData = new Reservation(DateTime.Now, DateTime.Today, "baris portakal",room);
            reservationHandler.AddReservation(reservationAddData);

            var reservations = reservationRepository.GetAllReservations();
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Reservation: {reservation.ReserverName} on {reservation.Date} at {reservation.Time} in {reservation.Room.RoomName}");
            }

            // yorum satırını açınca roomid ile reservation.jsondan siler.
            //var reservationDelete = reservationRepository.GetAllReservations().FirstOrDefault(r => r.Room.RoomId == "001");
            //reservationHandler.DeleteReservation(reservationDelete);

        }
    }
    }
