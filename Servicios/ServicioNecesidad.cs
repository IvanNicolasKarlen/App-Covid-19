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
            return necesidadesDAO.ObtenerPorID(id);
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

            return necesidadesDAO.Crear(necesidades);
        }
        /// <summary>
        /// Trae todas las necesidades del usuario en base al estado de las mismas
        /// </summary>
        /// <param name="idSession"></param>
        /// <param name="estadoNecesidad"></param>
        /// <returns></returns>
        public List<Necesidades> TraerNecesidadesDelUsuario(int idSession, string estadoNecesidad)
        {
            if (estadoNecesidad == "on")
            {
                List<Necesidades> necesidadesBD = necesidadesDAO.TraerNecesidadesActivasDelUsuario(idSession);
                List<Necesidades> necesidadesReturn = AlgoritmoCalculaValoracionDeListadoNecesidades(necesidadesBD);
                return necesidadesReturn;
            }
            else
            {
                List<Necesidades> necesidadesBD = necesidadesDAO.TraerTodasLasNecesidadesDelUsuario(idSession);
                List<Necesidades> necesidadesReturn = AlgoritmoCalculaValoracionDeListadoNecesidades(necesidadesBD);
                return necesidadesReturn;

            }

        }

        public List<Necesidades> ListarTodasLasNecesidades()
        {
            List<Necesidades> necesidadesBD = necesidadesDAO.ListarTodasLasNecesidades();
            List<Necesidades> necesidadesReturn = AlgoritmoCalculaValoracionDeListadoNecesidades(necesidadesBD);

            return necesidadesReturn;
        }

        /// <summary>
        /// Obtiene un listado de necesidades y calcula los valores de cada necesidad
        /// </summary>
        /// <param name="lista"></param>
        /// <returns>List<Necesidades></returns>
        public List<Necesidades> AlgoritmoCalculaValoracionDeListadoNecesidades(List<Necesidades> lista)
        {
            List<Necesidades> necesidadesReturn = new List<Necesidades>();
            foreach (var item in lista)
            {
                necesidadesReturn.Add(calcularValoracion(item));
            }
            return necesidadesReturn;
        }

        /// <summary>
        /// Algoritmo que calcula la valoracion de la necesidad
        /// </summary>
        /// <param name="necesidad"></param>
        /// <returns>Necesidades</returns>
        public Necesidades calcularValoracion(Necesidades necesidad)
        {
            ServicioNecesidadValoraciones servicioNecesidadValoraciones = new ServicioNecesidadValoraciones();
            List<NecesidadesValoraciones> valoracionesObtenidas = servicioNecesidadValoraciones.obtenerValoracionesPorIDNecesidad(necesidad.IdNecesidad);
            decimal cantidadLikes = 0;
            decimal cantidadDeVotaciones = valoracionesObtenidas.Count;

            foreach (var item in valoracionesObtenidas)
            {
                if (item.Valoracion == "Like")
                {
                    cantidadLikes++;
                }
            }

            decimal valoracion = cantidadLikes * cantidadDeVotaciones / 100;

            necesidad.Valoracion = valoracion;

            NecesidadesDAO necesidadesDAO = new NecesidadesDAO();
            Necesidades necesidadBD = necesidadesDAO.Actualizar(necesidad);
            if (necesidadBD == null)
            {
                return null;
            }
            return necesidadBD;
        }
    }
}
