using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO;
using Entidades;
using Entidades.Metadata;

namespace Servicios
{
    public class ServicioNecesidad
    {
        NecesidadesDAO necesidadesDAO = new NecesidadesDAO();
        public Necesidades obtenerNecesidadPorId(int id)
        {
            return necesidadesDAO.BuscarNecesidad(id);
        }

        public Necesidades buildNecesidad(NecesidadesMetadata necesidadmd, int idUser)
        
        {
            Necesidades necesidades = new Necesidades()
            {
                Nombre = necesidadmd.Nombre,
                Descripcion = necesidadmd.Descripcion,
                TelefonoContacto = necesidadmd.TelefonoContacto,
                FechaCreacion = DateTime.Now,
                FechaFin = necesidadmd.FechaFin,
                Foto = necesidadmd.Foto,
                TipoDonacion = (necesidadmd.TipoDonacion == TipoDonacion.Monetaria) ? 1 : 2,
                IdUsuarioCreador = idUser,
                Estado = 0,
                Valoracion = null
            };

            return necesidadesDAO.CrearNecesidades(necesidades);
        }
        /// <summary>
        /// Trae todas las necesidades del usuario en base al estado de las mismas
        /// </summary>
        /// <param name="idSession"></param>
        /// <param name="estadoNecesidad"></param>
        /// <returns></returns>
        public List<Necesidades> TraerNecesidadesDelUsuario(int idSession, string estadoNecesidad=null)
        {
            if (estadoNecesidad == "on")
            {
                return necesidadesDAO.TraerNecesidadesActivasDelUsuario(idSession);
            }
            else
            {
                return necesidadesDAO.TraerTodasLasNecesidadesDelUsuario(idSession);
            }

        }

        public List<Necesidades> ListarTodasLasNecesidades()
        {
            return necesidadesDAO.ListarTodasLasNecesidades();
        }
    }
}
