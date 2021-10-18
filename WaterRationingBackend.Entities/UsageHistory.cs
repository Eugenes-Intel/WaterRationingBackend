using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterRationingBackend.Entities
{
    public class UsageHistory : IData
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Suburb.Id))]
        public int SuburbId { get; set; }

        public DateTime Day { get; set; }

        public float Usage { get; set; }
    }
}