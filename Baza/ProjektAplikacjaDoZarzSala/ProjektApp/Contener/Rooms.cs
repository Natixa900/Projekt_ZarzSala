using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektApp.Contener
{
    public class Rooms
    {
        public int IdRoom { get; set; }
        public string roomName { get; set; }
        public int chairNumber { get; set; }
        public string equipment { get; set; }
        public bool avilable { get; set; }
        public string price { get; set; }

        public Rooms(int id, string name, int chairnum, string eq, bool av, string price)
        {
            this.IdRoom = id;
            this.roomName = name;
            this.chairNumber = chairnum;
            this.equipment = eq;
            this.avilable = av;
            this.price = price;


        }
    }
}
