using System;
using System.ComponentModel.DataAnnotations;

namespace superVise.Models.Requests
{
    public class NewLocationRequest
    {
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }
    }
}