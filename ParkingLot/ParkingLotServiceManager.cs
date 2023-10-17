namespace ParkingLot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ParkingLotServiceManager : ParkingBoy
    {
        // NOTE: ParkingBoys here is exactly the same way as parkedCar of ParkingLot and it should be public so the { get; set; } is necessary
        public List<ParkingBoy> ManagementList { get; set; } = new List<ParkingBoy>();

        public void AddParkingBoy(ParkingBoy parkingBoy)
        {
            if (parkingBoy == null)
            {
                throw new ArgumentNullException(nameof(parkingBoy));
            }

            if (ManagementList.Contains(parkingBoy))
            {
                throw new InvalidOperationException("Parking Boy is already in the management list.");
            }

            ManagementList.Add(parkingBoy);
        }

        public ParkingTicket SpecifyParkingBoyToParkCar(Car car)
        {
            Random rnd = new Random();
            return ManagementList[rnd.Next(ManagementList.Count)].ParkCar(car);
        }

        public new Car FetchCar(ParkingTicket ticket) => WhichParkingLotCarParkedIn(ticket.LicensePlate)?.FetchCar(ticket)
            ?? ManagementList.FirstOrDefault(parkingBoy => parkingBoy.WhichParkingLotCarParkedIn(ticket.LicensePlate) != null)?.FetchCar(ticket)
            ?? throw new InvalidOperationException("Unrecognized parking ticket.");
    }
}