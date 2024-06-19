using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class ClienteRepository : ICliente
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> add(ClienteModel cliente)
        {
            try
            {
                await _context.ClientesEF.AddAsync(cliente);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cliente", ex);
            }
        }

        public async Task<ClienteModel> get(int id)
        {
            try
            {
                return await _context.ClientesEF.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cliente", ex);
            }
        }

        public async Task<IEnumerable<ClienteModel>> list()
        {
            try
            {
                return await _context.ClientesEF.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes", ex);
            }
        }

        public async Task<bool> remove(int id)
        {
            try
            {
                var cliente = await _context.ClientesEF.FindAsync(id);
                if (cliente != null)
                {
                    _context.ClientesEF.Remove(cliente);
                    return await _context.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente", ex);
            }
        }

        public async Task<bool> update(ClienteModel cliente)
        {
            try
            {
                _context.ClientesEF.Update(cliente);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar cliente", ex);
            }
        }

        public async Task<bool> DocumentoExiste(string documento)
        {
            try
            {
                return await _context.ClientesEF.AnyAsync(c => c.documento == documento);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la existencia del documento", ex);
            }
        }
    }
}