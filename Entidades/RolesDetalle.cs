using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea5RegistroConDetalle.Entidades
{
    public class RolesDetalle
    {
        [Key]
        public int Id { get; set; }
        public int RolId { get; set; }
        public int PermisoId { get; set; }
        public bool EsAsignado { get; set; }
        public string Descripcion { get; set; }

        public RolesDetalle()
        {
            Id = 0;
            RolId = 0;
            PermisoId = 0;
            EsAsignado = false;
            Descripcion = "";
        }

        public RolesDetalle(int id, int rolId, int permisoId, bool esAsignado)
        {
            Id = id;
            RolId = rolId;
            PermisoId = permisoId;
            EsAsignado = esAsignado;
        }

        public RolesDetalle(int rolId, int permisoId, bool esAsignado)
        {
            RolId = rolId;
            PermisoId = permisoId;
            EsAsignado = esAsignado;
        }

        public RolesDetalle(int rolId, int permisoId, bool esAsignado, string descripcion)
        {
            RolId = rolId;
            PermisoId = permisoId;
            EsAsignado = esAsignado;
            Descripcion = descripcion;
        }

        public RolesDetalle(int id, int rolId, int permisoId, bool esAsignado, string descripcion)
        {
            Id = id;
            RolId = rolId;
            PermisoId = permisoId;
            EsAsignado = esAsignado;
            Descripcion = descripcion;
        }
    }
}
