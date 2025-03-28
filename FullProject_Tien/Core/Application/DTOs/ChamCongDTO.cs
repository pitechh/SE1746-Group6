using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs
{
    public class ChamCongDTO
    {
        public string MaNV { get; set; }

        public DateTime NgayChamCong { get; set; }

        public TimeSpan GioVao { get; set; }

        public TimeSpan GioRa { get; set; }
    }
}
