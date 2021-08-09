using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace pokedex_form
{
    class PokemonNegocio
    {
        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            try
            {
                conexion.ConnectionString = "data source=.\\SQLEXPRESS; initial catalog=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select a.nombre, a.descripcion, a.Numero, a.UrlImagen, b.descripcion, a.id from pokemons a join elementos b on a.IdTipo = b.Id";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Nombre = lector.GetString(0);
                    aux.Descripcion = (string)lector["descripcion"];
                    aux.Numero = lector.GetInt32(2);
                    aux.UrlImagen = (string)lector["UrlImagen"];
                    // tipo = getEmoteByType(lector.GetString(4));
                    aux.Tipo = lector.GetString(4);
                    aux.Id = lector.GetInt32(5);

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public bool actualizar(int id, string descripcion)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.ConnectionString = "data source=.\\SQLEXPRESS; initial catalog=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "update POKEMONS set descripcion = @descripcion where id = @id";
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                comando.Connection = conexion;

                conexion.Open();
                int rows = comando.ExecuteNonQuery();
                conexion.Close();
                if (rows == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*public string getEmoteByType(string tipo)
        {
            string resultado = "";
            switch (tipo)
            {
                case ("Fuego"):
                    resultado = "🔥";
                    break;
                case ("Planta"):
                    resultado = "🍃";
                    break;
                case ("Agua"):
                    resultado = "💧";
                    break;
            }
            return resultado;
        }*/
    }
}
