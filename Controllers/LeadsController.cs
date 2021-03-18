using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_Rest_API.Models;
using System.Collections.Generic;
using Rocket_Elevators_Rest_API.Data;

namespace Rocket_Elevators_Rest_API.Models.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly rocketelevators_developmentContext _context;

        public LeadController(rocketelevators_developmentContext context)
        {
            _context = context;

        }

        // To get full list of leads                                   
        // GET: api/lead/all           
        [HttpGet("all")]
        public IEnumerable<Leads> GetLeads()
        {
            IQueryable<Leads> Leads =
            from leaad in _context.Leads
            select leaad;
            return Leads.ToList();

        }

        
         [HttpGet("notcustomers")]
         public IEnumerable<Leads> GetLead()
         {
           DateTime today = DateTime.Now;
          DateTime answer = today.AddDays(-30);
            IQueryable<Leads> notCustomers =
            from leaad in _context.Leads
            where leaad.ContactRequestDate  >= answer
            select leaad;
            return notCustomers.ToList();
         }               
    }
}