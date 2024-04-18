using Repository.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logica
{
    public class ClienteService
    {
        ClienteRepository clienteRepository;

        public ClienteService (string connectionString)
        {
            clienteRepository = new ClienteRepository(connectionString);
        }
        public bool add(ClienteModel modelo)
        {
            if (validacionCliente(modelo))
                return clienteRepository.add(modelo);
            else
                return false;
        }

        public object get(string documento)
        {
            throw new NotImplementedException();
        }

        public object list()
        {
            throw new NotImplementedException();
        }

        public bool remove(string documento)
        {
            throw new NotImplementedException();
        }

        private bool validacionCliente(ClienteModel cliente)
        {
            if (cliente == null)
                return false;



            if (string.IsNullOrWhiteSpace(cliente.nombre) || cliente.nombre.Length < 3)
                return false;

            if (string.IsNullOrWhiteSpace(cliente.apellido) || cliente.apellido.Length < 3)
                return false;

            if (string.IsNullOrWhiteSpace(cliente.documento) || cliente.documento.Length < 3)
                return false;
            if (string.IsNullOrWhiteSpace(cliente.celular) && (cliente.celular.Length != 10 || !EsNumero(cliente.celular)))
              
                return false;


            // Si todas las validaciones pasan, retornar true
            //eturn false;


           // if (string.IsNullOrEmpty(cliente.nombre))
             //    return false;

           return true;
        }

        private bool EsNumero(string celular)
        {
            {
                foreach (char c in celular)
                {
                    if (!char.IsDigit(c))
                        return false;
                }
                return true;
                //throw new NotImplementedException();
            }
        }

        public bool ValidarCelular(string celular)
        {
            throw new NotImplementedException();
        }
    }
}
