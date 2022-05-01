using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketTrackingAP.Model
{
    public class Ticket
    {
        public string TicketId { get; set; }
        public string Status { get; set; }
        public string TicketType { get; set; }
        public string Applicant { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Solver { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Severity { get; set; }
        public string Priority { get; set; }
        public string RDRemark { get; set; }
    }
}
