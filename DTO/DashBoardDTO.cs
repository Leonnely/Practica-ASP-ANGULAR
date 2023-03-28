using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DashBoardDTO
    {
        public int TotalVentas { get; set; }
        public string? TotalIngesos { get; set; }
        public int TotalProductos { get; set; }

        public List<VentaSemanaDTO> VentasUltimaSemana { get; set; }
    }
}
