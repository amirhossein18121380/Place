using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Place.Domain.ViewModels
{
    public class ReserveViewModel
    {
        public long PlaceId { get; set; }
        public DateTime ReserveTime { get; set; }
        public decimal Cost { get; set; }
    }
}