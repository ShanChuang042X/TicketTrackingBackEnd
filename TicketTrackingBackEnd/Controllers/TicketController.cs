using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketTrackingAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [HttpGet]
        [Route("Test")]
        [Produces("application/json", Type = typeof(int))]
        public async Task<IActionResult> Test()
        {
            return Ok(7777);
        }
        [HttpPost("Add")]
        [Produces("application/json", Type = typeof(int))]
        public async Task<IActionResult> Add(Model.Ticket ticket)
        {
            ticket.TicketId = Repository.Tickets.GetNewID();
            var response = Repository.Tickets.Add(ticket);

            if(response == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost("Update")]
        [Produces("application/json", Type = typeof(int))]
        public async Task<IActionResult> Update(Model.Ticket ticket)
        {
            
            var response = Repository.Tickets.Update(ticket);

            if (response == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("Delete")]
        [Produces("application/json", Type = typeof(int))]
        public async Task<IActionResult> Delete(Model.Ticket ticket)
        {

            var response = Repository.Tickets.Delete(ticket.TicketId);

            if (response == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("GetAll")]
        [Produces("application/json", Type = typeof(List<Model.Ticket>))]
        public async Task<IActionResult> GetAll()
        {

            var response = Repository.Tickets.GetAll();
            return Ok(response);
        }



        [HttpGet("{ticketID}/Get")]
        [Produces("application/json", Type = typeof(Model.Ticket))]
        public async Task<IActionResult> Get(string ticketID)
        {

            var response = Repository.Tickets.Get(ticketID);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
            
        }
        
    }
}
