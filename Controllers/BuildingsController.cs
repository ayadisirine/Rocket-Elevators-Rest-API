using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_Rest_API.Models;
using System.Collections.Generic;
using Rocket_Elevators_Rest_API.Data;

namespace Rocket_Elevators_Rest_API.Models.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class BuildingsController : Controller
  {
    private readonly rocketelevators_developmentContext _context;

    public BuildingsController(rocketelevators_developmentContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      List<Buildings> buildingList = new List<Buildings>();

      // Checks for elevators that are offline
      foreach(var elevator in _context.Elevators.ToArray())
      {
        if (elevator.Status == "Intervention")
        {
          foreach(var column in _context.Columns.ToArray())
          {
            if (elevator.Id == column.Id)
            {
              foreach (var battery in _context.Batteries.ToArray())
              {
                if (battery.Id == column.BatteryId)
                {
                  foreach (var building in _context.Buildings.ToArray())
                  {
                    if (building.Id == battery.BuildingId)
                    {
                      if (buildingList.Contains(building))
                      {
                       continue; 
                      }
                      else
                      {
                        buildingList.Add(building);
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }

      // Checks for columns that are offline
      foreach(var column in _context.Columns.ToArray())
      {
        if (column.Status == "Intervention")
        {
          foreach (var battery in _context.Batteries.ToArray())
          {
            if (battery.Id == column.BatteryId)
            {
              foreach (var building in _context.Buildings.ToArray())
              {
                if (building.Id == battery.BuildingId)
                {
                  if (buildingList.Contains(building))
                  {
                    continue;
                  }
                  else
                  {
                    buildingList.Add(building);
                  }
                }
              }
            }
          }
        }
      }

      // Checks for batteries that are offline
      foreach(var battery in _context.Batteries.ToArray())
      {
        if (battery.Status == "Offline")
        {
          foreach (var building in _context.Buildings.ToArray())
          {
            if (building.Id == battery.BuildingId)
            {
              if (buildingList.Contains(building))
              {
                continue;
              }
              else
              {
                buildingList.Add(building);
              }
            }
          }
        }
      }

      foreach (var building in buildingList)
      {
        Console.WriteLine(building.Id);
      }

      return Ok(buildingList);
    }
  }
}
