using Datos;
using Entidades;
using System.Text.RegularExpressions;

namespace Negocio;

public class PersonaBLL
{
    private readonly PersonaDAL _dal = new();
    public List<Persona> Listar(string filtro = "") => _dal.Listar(filtro);

    public void Registrar(Persona p)
    {
        Validar(p);
        _dal.Insertar(p);
    }

    public void Actualizar(Persona p)
    {
        if (p.IdPersona <= 0) throw new ArgumentException("Selecciona una persona válida.");
        Validar(p); _dal.Actualizar(p);
    }

    public void Eliminar(int id)
    {
        if (id <= 0) throw new ArgumentException("Selecciona una persona válida.");
        _dal.EliminarLogico(id);
    }

    private static void Validar(Persona p)
    {
        p.NombreCompleto = p.NombreCompleto.Trim(); p.DNI = p.DNI.Trim(); p.Telefono = p.Telefono.Trim();
        if (p.NombreCompleto.Length < 3 || p.NombreCompleto.Length > 100) throw new ArgumentException("El nombre debe tener entre 3 y 100 caracteres.");
        if (!Regex.IsMatch(p.DNI, @"^\d{8}$")) throw new ArgumentException("El DNI debe contener exactamente 8 dígitos.");
        if (p.Telefono.Length > 15 || !Regex.IsMatch(p.Telefono, @"^[0-9+ -]{6,15}$")) throw new ArgumentException("Ingresa un teléfono válido (6 a 15 caracteres).");
    }
}
