using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Place.Application.Dto
{
    public class FullUserInfoDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterDate { get; set; }

    }
}
