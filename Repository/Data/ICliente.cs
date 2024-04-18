using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public interface ICliente
    {
        bool add(ClienteModel cliente);
        bool remove(string cliente);
        bool update(ClienteModel cliente);
        ClienteModel get(string id);
        IEnumerable<ClienteModel> list();
    }
}
