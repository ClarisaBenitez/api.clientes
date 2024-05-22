using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public interface ICliente
    {
        Task<bool> add(ClienteModel cliente);
        Task<bool> remove(int id);
        Task<bool> update(ClienteModel cliente);
        Task< ClienteModel> get(int id);
        Task<IEnumerable<ClienteModel>> list();
    }
}
