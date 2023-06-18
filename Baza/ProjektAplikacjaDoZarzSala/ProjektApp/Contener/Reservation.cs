using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektApp.Contener
{
   public class Reservation
    {
        public int ReservationID { get; set; }
        public string Name { get; set; }
        public int ClientID { get; set; }
        public bool equipment { get; set; }
        public int EmployeeID { get; set; }
        public DateTime dateTime { get; set; }

        public Reservation() { }
        public Reservation(int ReservationID, string Name, int ClientId, bool EQ, int EmploID,DateTime dateTime)
        {
            this.ReservationID = ReservationID;
            this.Name = Name;
            this.ClientID = ClientId;
            this.equipment = EQ;
            this.EmployeeID = EmploID;
            this.dateTime = dateTime;

        }
    }
}
