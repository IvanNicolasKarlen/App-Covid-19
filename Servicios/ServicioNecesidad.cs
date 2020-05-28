using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO;
using Entidades;
namespace Servicios
{
    public class ServicioNecesidad
    {
        NecesidadesDAO necesidadesDAO = new NecesidadesDAO();
        public Necesidades obtenerNecesidadPorId(int id)
        {
            return necesidadesDAO.BuscarNecesidad(id);
        }

        public Necesidades buildNecesidad(VMNecesidad vmnecesidad, int idUser)
        {
            Necesidades necesidades = new Necesidades()
            {
                Nombre = vmnecesidad.Nombre,
                Descripcion = vmnecesidad.Descripcion,
                TelefonoContacto = vmnecesidad.TelefonoContacto,
                FechaCreacion = DateTime.Now,
                FechaFin = vmnecesidad.FechaFin,
                Foto = vmnecesidad.Foto,
                TipoDonacion = vmnecesidad.TipoDonacion,
                IdUsuarioCreador = idUser,
                Estado = 0,
                Valoracion = null
            };

            return necesidadesDAO.CrearNecesidades(necesidades);
        }

        public List<Necesidades> necesidadesDelUsuario(int idSession, string necesidad)
        {
            NecesidadesDAO necesidadesDao = new NecesidadesDAO();

            if (necesidad == "on")
            {
                // Aquellas que fueron creadas por él y
                //aún no están finalizadas pudiendo recibir donaciones
                List<Necesidades> necesidadesActivas = necesidadesDao.necesidadesActivas(idSession);
                return necesidadesActivas;
            }
            else
            {
                //En caso de que se destilde, se visualizarán
                //todas las del usuario sin importar si están o no finalizadas.

                List<Necesidades> todasLasNecesidadesDelUsuario = necesidadesDao.necesidadesDelUsuario(idSession);
                return todasLasNecesidadesDelUsuario;
            }
        }
    }
}