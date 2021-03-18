// using System;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using Rocket_Elevators_Rest_API.Models;
// using System.Collections.Generic;
// using Rocket_Elevators_Rest_API.Data;

// namespace Rocket_Elevators_Rest_API.Models.Controllers
// {
//   [ApiController]
//   [Route("api/[controller]")]
//   public class LeadsController : Controller
//   {
//     private rocketelevators_developmentContext _context;

//     public LeadsController(rocketelevators_developmentContext context)
//     {
//       _context = context;
//     }

//     public IActionResult Index()
//     {
//       List<Leads> leadList = new List<Leads>();

//       DateTime minimumTime = DateTime.Now - TimeSpan.FromDays(30);

//       foreach(var lead in _context.Leads.ToArray())
//       {
//         if (lead.ContactRequestDate > minimumTime)
//         {
//           foreach(var customer in _context.Customers.ToArray())
//           {
//             if (lead.CompanyName == customer.CompanyName)
//             {
//               if (leadList.Contains(lead))
//               {
//                 continue;
//               }
//               else
//               {
//                 leadList.Add(lead);
//               }
//             }
//           }
//         }
//       }

//       return Ok(leadList);
//     }
//   }
// }