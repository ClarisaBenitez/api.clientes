using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;


namespace Repository.Data
{
  
        public class ClienteRepository : ICliente
        {
            private IDbConnection conexionDB;
            public ClienteRepository(string _connectionString)
            {
                conexionDB = new DbConection(_connectionString).dbConnection();
            }
            public bool add(ClienteModel cliente)
            {
                try
            {
                if (conexionDB.Execute("insert into cliente (id, id_banco, nombre, apellido,documento, direccion, mail, celular, estado) values (@id, @id_banco, @nombre, @apellido, @documento, @direccion, @mail, @celular, @estado)", cliente) > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            public ClienteModel get(string documento)
            {
                try
            {
                return conexionDB.Query<ClienteModel>("Select * from Cliente where documento = @documento", new { documento }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

                    

            }

            public IEnumerable<ClienteModel> list()
            {
            try
            {
                return conexionDB.Query<ClienteModel>("Select * from Cliente");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool remove(string documento)
            {
            try
            {
                if (conexionDB.Execute("Delete from Cliente where documento = @documento", new { documento }) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool update(ClienteModel cliente)
        {
            try
            {
                if (conexionDB.Execute("Update Cliente set id = @id, id_banco = @id_banco, nombre = @nombre, " +
                    "apellido = @apellido, direccion = @direccion, mail = @mail, celular = @celular, estado = @estado " +
                    "where documento = @documento", cliente) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

       

        // public bool update(ClienteModel cliente)
        // {
        //      throw new NotImplementedException();
        // }
    }
}
