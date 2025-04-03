using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CSharp_Lab13.Models
{
    public class Booking
    {
        [PrimaryKey, AutoIncrement]
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public int TourId { get; set; }
        public int NumberOfPeople { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
