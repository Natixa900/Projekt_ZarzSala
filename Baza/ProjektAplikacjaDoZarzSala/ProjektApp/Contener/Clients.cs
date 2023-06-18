using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektApp.Contener
{
    public class Clients
    {
        public int ClientId { get; set; }
        public string CFName { get; set; }
        public string CLName { get; set; }
        public string CPhone { get; set; }
        public string CMail { get; set; }

        public Clients(int CID, string FirstName, string LastName, string Phone, string Mail)
        {
            this.ClientId = CID;
            this.CFName = FirstName;
            this.CLName = LastName;
            this.CPhone = Phone;
            this.CMail = Mail;

        }
    }
}
