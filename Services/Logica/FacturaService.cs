using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Logica
{
    public class FacturaService
    {
       
            FacturaRepository facturaRepository;

            public FacturaService(string connectionString)
            {
                facturaRepository = new FacturaRepository(connectionString);
            }
            public bool add(FacturaModel modelo)
            {
                if (validacionFactura(modelo))
                    return facturaRepository.add(modelo);
                else
                    return false;
            }

            public object get(string nro_factura)
            {
                throw new NotImplementedException();
            }

            public object list()
            {
                throw new NotImplementedException();
            }

            public bool remove(string nro_factura)
            {
                throw new NotImplementedException();
            }

            private bool validacionFactura(FacturaModel factura)
            {
            // Validación del número de factura
            if (!Regex.IsMatch(factura.nro_factura, @"^\d{3}-\d{3}-\d{6}$"))
                throw new ArgumentException("Error al agregar factura: El número de factura no cumple con el formato requerido.");
            //return false;

            // Validación de los campos numéricos
            if (factura.total <= 0 || factura.total_iva5 <= 0 || factura.total_iva10 <= 0 || factura.total_iva <= 0)
                throw new ArgumentException("Error al agregar factura: Los campos numéricos de la factura son inválidos.");


            //return false;

            // Validación del total en letras
            if (string.IsNullOrWhiteSpace(factura.total_letras) || factura.total_letras.Length < 6)
                throw new ArgumentException("Error al agregar factura: El campo 'Total en letras' de la factura es obligatorio y debe tener al menos 6 caracteres.");
            //return false;

            return true;
           }

        }
    }

