using BE;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class MenuDal200MI
    {
        private readonly ConexionSql200MI _conexion = new ConexionSql200MI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public List<Pantalla200MI> GetMenuPadres()
        {
            var resultado = new List<Pantalla200MI>();

            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();

                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * from Pantallas_200MI";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                resultado.Add(new Pantalla200MI
                                {
                                    IdPantalla_200MI = Convert.ToInt32(reader["IdPantalla_200MI"]),
                                    Nombre_200MI = reader["Nombre_200MI"].ToString() ?? "",
                                    NombreForm_200MI = reader["NombreForm_200MI"].ToString() ?? "",
                                    IdPadre_200MI = reader["IdPadre_200MI"] == DBNull.Value 
                                                    ? null 
                                                    : Convert.ToInt32(reader["IdPadre_200MI"]),
                                    Orden_200MI = Convert.ToInt32(reader["Orden_200MI"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex){
                _logger.Error(ex, "Error al obtener el menú de Pantallas_200MI");

            }

            return resultado;
        }

        public List<Pantalla200MI> GetMenuHijos(int idPadre)
        {
            var resultado = new List<Pantalla200MI>();

            try
            {
                using (var con = _conexion.GetConnection())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = """
                            select * from Pantallas_200MI
                            where IdPadre_200MI = @IdPadre
                            and Activo_200MI = 1
                            order by Orden_200MI
                            """;

                        cmd.Parameters.AddWithValue("@IdPadre", idPadre);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultado.Add(new Pantalla200MI
                                {
                                    NombreForm_200MI = reader["NombreForm_200MI"].ToString() ?? "",
                                    Orden_200MI = Convert.ToInt32(reader["Orden_200MI"]),
                                    Nombre_200MI = reader["Nombre_200MI"].ToString() ?? "",
                                    EsAccion_200MI = Convert.ToBoolean(reader["EsAccion_200MI"]),
                                });
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                _logger.Error(ex, $"Error al obtener el menú de hijos para IdPadre: {idPadre}");
            }

            return resultado;
        }
    }
}
