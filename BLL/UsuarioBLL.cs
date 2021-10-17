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
    class UsuarioBLL
    {
        public static bool Guardar(Usuario Usuario)
    {
        if (!Existe(Usuario.UsuarioId))
            return Insertar(Usuario);
        else
            return Modificar(Usuario);
    }
    private static bool Insertar(Usuario Usuario)
    {
        bool paso = false;
        Contexto db = new Contexto();
        try
        {
            if (db.Usuario.Add(Usuario) != null)
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

    private static bool Modificar(Usuario Usuario)
    {
        bool paso = false;
        Contexto db = new Contexto();
        try
        {
            db.Entry(Usuario).State = EntityState.Modified;
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
            Usuario Usuario = db.Usuario.Find(id);

            if (Existe(id))
            {
                db.Usuario.Remove(Usuario);
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

    public static Usuario Buscar(int id)
    {
        Contexto db = new Contexto();
        Usuario Usuario = new Usuario();
        try
        {
            Usuario = db.Usuario.Find(id);

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            db.Dispose();
        }
        return Usuario;
    }

    public static List<Usuario> GetList(Expression<Func<Usuario, bool>> expression)
    {
        List<Usuario> Usuario = new List<Usuario>();
        Contexto db = new Contexto();
        try
        {
            Usuario = db.Usuario.Where(expression).ToList();

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            db.Dispose();
        }
        return Usuario;
    }
    private static bool Existe(int id)
    {
        bool encontrado = false;
        Contexto db = new Contexto();
        try
        {
            encontrado = db.Usuario.Any(x => x.UsuarioId == id);
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
}
}