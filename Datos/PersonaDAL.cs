using System.Data;
using Microsoft.Data.SqlClient;
using Entidades;

namespace Datos;

public class PersonaDAL
{
    public List<Persona> Listar(string filtro = "")
    {
        var personas = new List<Persona>();
        using var cn = new SqlConnection(Conexion.Cadena);
        using var cmd = new SqlCommand("SELECT IdPersona, NombreCompleto, DNI, Telefono, Activo FROM Persona WHERE Activo = 1 AND (@Filtro = '' OR NombreCompleto LIKE '%' + @Filtro + '%' OR DNI LIKE '%' + @Filtro + '%') ORDER BY IdPersona DESC", cn);
        cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 100).Value = filtro.Trim();
        cn.Open();
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) personas.Add(Mapear(rd));
        return personas;
    }

    public void Insertar(Persona p)
    {
        using var cn = new SqlConnection(Conexion.Cadena);
        using var cmd = new SqlCommand("INSERT INTO Persona (NombreCompleto, DNI, Telefono, Activo) VALUES (@Nombre, @DNI, @Telefono, 1)", cn);
        Parametros(cmd, p); cn.Open(); cmd.ExecuteNonQuery();
    }

    public void Actualizar(Persona p)
    {
        using var cn = new SqlConnection(Conexion.Cadena);
        using var cmd = new SqlCommand("UPDATE Persona SET NombreCompleto=@Nombre, DNI=@DNI, Telefono=@Telefono WHERE IdPersona=@Id AND Activo=1", cn);
        Parametros(cmd, p); cmd.Parameters.Add("@Id", SqlDbType.Int).Value = p.IdPersona;
        cn.Open(); cmd.ExecuteNonQuery();
    }

    public void EliminarLogico(int id)
    {
        using var cn = new SqlConnection(Conexion.Cadena);
        using var cmd = new SqlCommand("UPDATE Persona SET Activo=0 WHERE IdPersona=@Id AND Activo=1", cn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        cn.Open(); cmd.ExecuteNonQuery();
    }

    private static void Parametros(SqlCommand cmd, Persona p)
    {
        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = p.NombreCompleto;
        cmd.Parameters.Add("@DNI", SqlDbType.Char, 8).Value = p.DNI;
        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 15).Value = p.Telefono;
    }

    private static Persona Mapear(SqlDataReader rd) => new()
    {
        IdPersona = rd.GetInt32(0), NombreCompleto = rd.GetString(1).Trim(),
        DNI = rd.GetString(2).Trim(), Telefono = rd.GetString(3), Activo = rd.GetBoolean(4)
    };
}
