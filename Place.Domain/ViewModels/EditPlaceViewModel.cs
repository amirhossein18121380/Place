using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Place.Domain.ViewModels
{
    public class EditPlaceViewModel
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public short PlaceType { get; set; }
        public string? Location { get; set; }
    }
}
