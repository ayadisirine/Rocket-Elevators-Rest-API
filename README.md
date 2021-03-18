"# Rocket-Elevators-Rest-API" 
In this project we Created Rest Apis for Rocket Elevators.

We Scaffolded our The entities which are the Models folder That we had in Mysql Server.and every  model is a set of classes that represent the data that the app manages. for example in Batteries Class we have:

        public long Id { get; set; }
        public long? BuildingId { get; set; }
        public string Status { get; set; }
        public DateTime? DateCommissioning { get; set; }
        public DateTime? DateLastInspection { get; set; }
        public string CertificateOfOperations { get; set; }
        public string Information { get; set; }
        public string Notes { get; set; }
        public string BatteryType { get; set; }

We have a The database context(rocketelevators_developmentContext) is the main class that coordinates Entity Framework functionality for a data model.and for our Batteries Class, its relation is:
modelBuilder.Entity<Batteries>(entity =>
            {
                entity.ToTable("batteries");

                entity.HasIndex(e => e.BuildingId)
                    .HasName("index_batteries_on_building_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BatteryType)
                    .HasColumnName("battery_type")
                    .HasMaxLength(255);

                entity.Property(e => e.BuildingId)
                    .HasColumnName("building_id")
                    .HasColumnType("bigint(20)");
We use difference Controllers to expose Async API Endpoints for CRUD operations. For example in Batteries controller, 
[HttpGet("{id}")]
        public async Task<ActionResult<Batteries>> GetBattery(long id)
        {
            var battery = await _context.Batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery;
        }
And we can see the results our our request in Postman.
