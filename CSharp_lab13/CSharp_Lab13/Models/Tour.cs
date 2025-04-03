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
    public class Tour
    {
        [PrimaryKey, AutoIncrement]
        public int TourId { get; set; }
        public string Destination { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
