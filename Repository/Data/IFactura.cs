using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public interface IFactura
    {
        Task<bool> add(FacturaModel factura);
        Task<bool> remove(int id);
        Task<bool> update(FacturaModel factura);
        Task<FacturaModel> get(int id);
        Task<IEnumerable<FacturaModel>> list();
    }
}
