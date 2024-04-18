using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public interface IFactura
    {
        bool add(FacturaModel factura);
        bool remove(string factura);
        bool update(FacturaModel factura);
        FacturaModel get(string id);
        IEnumerable<FacturaModel> list();
    }
}
