using System.Globalization;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository.Data;

namespace Services.Logica
{
    public  class FacturaService
    {
        private readonly IFactura _facturaRepository;

        public FacturaService(IFactura facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<bool> Add(FacturaModel factura)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(factura.total_letras) || factura.total_letras.Length < 6)
                {
                    throw new ArgumentException("El campo 'Total en letras' es obligatorio y debe tener al menos 6 caracteres.");
                }
                factura.total_iva5 = (int)CalculateIva5(factura.total);
                factura.total_iva10 = (int)CalculateIva10(factura.total);
                factura.total_iva = (int)CalculateTotalIva(factura.total);
                //factura.TotalLetras = ConvertNumberToWords((int)factura.Total);
                return await _facturaRepository.add(factura);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la factura", ex);
            }
        }

        public async Task<bool> Update(FacturaModel factura)
        {
            try
            {
                factura.total_iva5 = (int)CalculateIva5(factura.total);
                factura.total_iva10 = (int)CalculateIva10(factura.total);
                factura.total_iva = (int)CalculateTotalIva(factura.total);
                //factura.TotalLetras = ConvertNumberToWords((int)factura.Total);
                return await _facturaRepository.update(factura);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la factura", ex);
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                return await _facturaRepository.remove(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la factura", ex);
            }
        }

        public async Task<FacturaModel> Get(int id)
        {
            try
            {
                return await _facturaRepository.get(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la factura", ex);
            }
        }

        public async Task<IEnumerable<FacturaModel>> List()
        {
            try
            {
                return await _facturaRepository.list();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las facturas", ex);
            }
        }

        public static decimal CalculateIva5(decimal amount)
            => amount * 0.05m; // 5% de IVA

        public static decimal CalculateIva10(decimal amount)
            => amount * 0.10m; // 10% de IVA

        public static decimal CalculateTotalIva(decimal amount)
        {
            decimal iva5 = CalculateIva5(amount);
            decimal iva10 = CalculateIva10(amount);
            return iva5 + iva10;
        }

       // public static string ConvertNumberToWords(int number) 
    }
}
