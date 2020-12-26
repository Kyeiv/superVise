using System;
using System.ComponentModel.DataAnnotations;

namespace superVise.Entities
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }
        public virtual User User { get; set; }
    }
}