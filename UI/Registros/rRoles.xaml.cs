using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tarea5RegistroConDetalle.Entidades;
using Tarea5RegistroConDetalle.BLL;

namespace Tarea5RegistroConDetalle.UI.Registros
{
    /// <summary>
    /// Interaction logic for rRoles.xaml
    /// </summary>
    public partial class rRoles : Window
    {
        private Roles rol = new Roles();
        //private Permisos permiso = new Permisos();
        public List<RolesDetalle> Detalle { get; set; }
        public rRoles()
        {
            InitializeComponent();
            this.DataContext = rol;
            PermisosComboBox.ItemsSource = PermisoBLL.GetPermisos();
            PermisosComboBox.SelectedValuePath = "PermisoId";
            PermisosComboBox.DisplayMemberPath = "Descripcion";
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Roles encontrado = RolBLL.Buscar(rol.RolId);

            if (encontrado != null)
            {
                rol = encontrado;
                Actualizar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Este rol no existe en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            rol.Detalle.Add(new RolesDetalle { RolId = rol.RolId, PermisoId = (int)PermisosComboBox.SelectedValue, EsAsignado = (bool)ActivoCheckBox.IsEnabled, Descripcion = DescripcionTextBox.Text.ToString() });

            Actualizar();

        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                rol.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Actualizar();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Roles esValido = RolBLL.Buscar(rol.RolId);

            return (esValido != null);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarGuardar())
                return;
            bool paso = false;

            if (rol.RolId == 0)
            {
                paso = RolBLL.Guardar(rol);
            }
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = RolBLL.Guardar(rol);
                }
                else
                {
                    MessageBox.Show("No existe en la base de datos", "ERROR");
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Fallo al guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Roles existe = RolBLL.Buscar(rol.RolId);

            if (existe == null)
            {
                MessageBox.Show("No existe la tarea en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                RolBLL.Eliminar(rol.RolId);
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = rol;
        }

        private void Limpiar()
        {
            this.rol = new Roles();
            this.DataContext = rol;
        }

        private bool Validar()
        {
            bool esValido = true;
            if (PermisosComboBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Ha ocurrido un error, Inserte el permiso", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return esValido;
        }

        private bool ValidarGuardar()
        {
            bool esValido = true;
            if (DetalleDataGrid.Items.Count == 0)
            {
                esValido = false;
                MessageBox.Show("Ha ocurrido un error, Debe agregar roles", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return esValido;
        }

        private void CargarGrid()
        {
            DetalleDataGrid.DataContext = null;
            DetalleDataGrid.DataContext = this.Detalle;
        }
    }
}
