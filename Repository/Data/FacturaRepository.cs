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
    public class FacturaRepository : IFactura
    {
        private IDbConnection conexionDB;
        public FacturaRepository(string _connectionString)
        {
            conexionDB = new DbConection(_connectionString).dbConnection();
        }


        public bool add(FacturaModel factura)
        {
            try
            {
                if (conexionDB.Execute("insert into factura (id, id_cliente, nro_factura, fecha_hora,total, total_iva5, total_iva10, total_iva, total_letras, sucursal) values (@id, @id_cliente, @nro_factura, @fecha_hora, @total, @total_iva5, @total_iva10, @total_iva, @total_letras, @sucursal)", factura) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;


               // throw new NotImplementedException();
        }
        }
        public FacturaModel get(string nro_factura)
        {
                try
                {
                    return conexionDB.Query<FacturaModel>("Select * from Factura where nro_factura = @nro_factura", new { nro_factura }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;

                    //throw new NotImplementedException();
                }
        }

        public IEnumerable<FacturaModel> list()
        {
                    try
                    {
                        return conexionDB.Query<FacturaModel>("Select * from Factura");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //throw new NotImplementedException();
                }

        public bool remove(string nro_factura)
        {
                    try
                    {
                        if (conexionDB.Execute("Delete from Factura where nro_factura = @nro_factura", new { nro_factura }) > 0)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                 //   throw new NotImplementedException();
        }

        public bool update(FacturaModel factura)
        {
                    try
                    {
                        if (conexionDB.Execute("Update Factura set id = @id, id_cliente = @id_cliente, sucursal = @sucursal, " +
                            "fecha_hora = @fecha_hora, total = @total, total_iva5 = @total_iva5, total_iva10 = @total_iva10, total_iva = @total_iva, total_letras= @total_letras " +
                            "where nro_factura = @nro_factura", factura) > 0)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
        }
    }
    }

