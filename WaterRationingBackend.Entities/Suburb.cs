using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterRationingBackend.Entities
{
    public class Suburb : IData
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get ; set ; }

        [ForeignKey(nameof(City.Id))]
        public int CityId { get; set; }

        public Density Density { get; set; }

        public float Population { get; set; }

        public float Allocation { get; set; }

        public float DailyAverageUsage { get; set; }
        
        public IEnumerable<UsageHistory> UsageHistory { get; set; }
    }
}
