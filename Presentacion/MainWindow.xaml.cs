using System.Windows;
using System.Windows.Controls;
using Entidades;
using Negocio;

namespace Presentacion;

public partial class MainWindow : Window
{
    private readonly PersonaBLL _bll = new();
    public MainWindow() { InitializeComponent(); Cargar(); }

    private void Cargar(string filtro = "")
    {
        try { var datos = _bll.Listar(filtro); dgPersonas.ItemsSource = datos; lblTotal.Text = $"{datos.Count} registros activos"; }
        catch (Exception ex) { MessageBox.Show("No se pudo conectar con la base de datos. Ejecuta Database/script.sql y revisa Datos/Conexion.cs.\n\n" + ex.Message, "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error); }
    }

    private Persona LeerFormulario() => new() { NombreCompleto = txtNombre.Text, DNI = txtDni.Text, Telefono = txtTelefono.Text };
    private void Guardar_Click(object sender, RoutedEventArgs e)
    {
        try { _bll.Registrar(LeerFormulario()); MessageBox.Show("Persona registrada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information); Limpiar(); Cargar(txtBuscar.Text); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Warning); }
    }
    private void Actualizar_Click(object sender, RoutedEventArgs e)
    {
        if (dgPersonas.SelectedItem is not Persona p) { MessageBox.Show("Selecciona una persona de la tabla."); return; }
        try { var editada = LeerFormulario(); editada.IdPersona = p.IdPersona; _bll.Actualizar(editada); MessageBox.Show("Persona actualizada."); Limpiar(); Cargar(txtBuscar.Text); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Warning); }
    }
    private void Eliminar_Click(object sender, RoutedEventArgs e)
    {
        if (dgPersonas.SelectedItem is not Persona p) { MessageBox.Show("Selecciona una persona de la tabla."); return; }
        if (MessageBox.Show($"¿Deseas eliminar lógicamente a {p.NombreCompleto}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
        try { _bll.Eliminar(p.IdPersona); Limpiar(); Cargar(txtBuscar.Text); MessageBox.Show("Registro desactivado correctamente."); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    private void Buscar_TextChanged(object sender, TextChangedEventArgs e) { if (IsLoaded) Cargar(txtBuscar.Text); }
    private void Personas_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dgPersonas.SelectedItem is Persona p) { txtNombre.Text = p.NombreCompleto; txtDni.Text = p.DNI; txtTelefono.Text = p.Telefono; }
    }
    private void Limpiar_Click(object sender, RoutedEventArgs e) => Limpiar();
    private void Limpiar() { txtNombre.Clear(); txtDni.Clear(); txtTelefono.Clear(); dgPersonas.SelectedItem = null; }
}
