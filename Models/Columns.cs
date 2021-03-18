using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Rest_API.Models
{
    public partial class Columns
    {
        public Columns()
        {
            Elevators = new HashSet<Elevators>();
        }

        public long Id { get; set; }
        public long? battery_id { get; set; }
        public int? NumberOfFloorsServed { get; set; }
        public string Status { get; set; }
        public string Information { get; set; }
        public string Notes { get; set; }
        public string ColumnType { get; set; }

        public virtual Batteries Battery { get; set; }
        public virtual ICollection<Elevators> Elevators { get; set; }
    }
}
