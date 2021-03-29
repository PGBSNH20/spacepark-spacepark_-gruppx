using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models
{
    public class Parking
    {
        public int Id { get; set; }
        public int Fee { get; set; }
        public decimal MaxLength { get; set; }
        public bool Occupied { get; set; }
        [MaxLength(50)] public string ParkedBy { get; set; }
        [MaxLength(50)] public string ShipName { get; set; }
    }
}
