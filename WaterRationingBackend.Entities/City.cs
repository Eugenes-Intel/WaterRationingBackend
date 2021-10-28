using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WaterRationingBackend.Entities
{
    public class City : IData
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        [Index(IsUnique =true)]
        public string Name { get; set; }

        public virtual ICollection<Suburb> Suburbs { get; set; }
    }
}
