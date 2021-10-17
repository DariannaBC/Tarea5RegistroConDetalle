using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tarea5RegistroConDetalle.DAL;
using Tarea5RegistroConDetalle.Entidades;

namespace Tarea5RegistroConDetalle.BLL
{
    public class PermisoBLL
    {

        public static bool Guardar(Permisos permiso)
        {
            if (!Existe(permiso.PermisoId))
                return Insertar(permiso);
            else
                return Modificar(permiso);
        }

        private static bool Insertar(Permisos permiso)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Permiso.Add(permiso) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            { throw; }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        private static bool Modificar(Permisos permiso)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                db.Entry(permiso).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            { throw; }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                Permisos permiso = db.Permiso.Find(id);

                if (Existe(id))
                {
                    db.Permiso.Remove(permiso);
                    paso = db.SaveChanges() > 0;
                }

            }
            catch (Exception)
            { throw; }
            finally
            {
                db.Dispose();
            }
            return paso;
        }


        public static Permisos Buscar(int id)
        {
            Contexto db = new Contexto();
            Permisos permiso = new Permisos();
            try
            {
                permiso = db.Permiso.Find(id);
                db.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return permiso;
        }

        public static List<Permisos> GetList(Expression<Func<Permisos, bool>> expression)
        {
            List<Permisos> Permisos = new List<Permisos>();
            Contexto db = new Contexto();
            try
            {
                Permisos = db.Permiso.Where(expression).ToList();
                db.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Permisos;
        }
        private static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto db = new Contexto();
            try
            {
                encontrado = db.Permiso.Any(x => x.PermisoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return encontrado;
        }

        public static List<Permisos> GetPermisos()
        {
            List<Permisos> lista = new List<Permisos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Permiso.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}