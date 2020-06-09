using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO;
using Entidades;
using Entidades.Enum;
using Entidades.Metadata;
using DAO.Context;

namespace Servicios
{
    public class ServicioNecesidad
    {
        NecesidadesDAO necesidadesDAO;
        TpDBContext contexto;
        //  ServicioNecesidadValoraciones servicioNecesidadValoraciones;
        public ServicioNecesidad(TpDBContext context)
        {
            necesidadesDAO = new NecesidadesDAO(context);
            contexto = context;
            // servicioNecesidadValoraciones = new ServicioNecesidadValoraciones(context);
        }

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
                //  List<Necesidades> necesidadesReturn = AlgoritmoCalculaValoracionDeListadoNecesidades(necesidadesBD);
                return necesidadesBD;
            }
            else
            {
                List<Necesidades> necesidadesBD = necesidadesDAO.TraerTodasLasNecesidadesDelUsuario(idSession);
                // List<Necesidades> necesidadesReturn = AlgoritmoCalculaValoracionDeListadoNecesidades(necesidadesBD);
                return necesidadesBD;

            }

        }

        public List<Necesidades> ListarTodasLasNecesidades()
        {
            List<Necesidades> necesidadesBD = necesidadesDAO.ListarTodasLasNecesidades();
            //List<Necesidades> necesidadesReturn = AlgoritmoCalculaValoracionDeListadoNecesidades(necesidadesBD);

            return necesidadesBD;
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
            ServicioNecesidadValoraciones servicioNecesidadValoraciones = new ServicioNecesidadValoraciones(contexto);

            List<NecesidadesValoraciones> valoracionesObtenidas = servicioNecesidadValoraciones.obtenerValoracionesPorIDNecesidad(necesidad.IdNecesidad);
            decimal cantidadLikes = 0;
            decimal cantidadDeVotaciones = valoracionesObtenidas.Count;
            decimal resultado = 0;

            //   foreach (var item in valoracionesObtenidas)
            foreach (var item in necesidad.NecesidadesValoraciones)
            {
                if (item.Valoracion == "Like")
                {
                    cantidadLikes++;
                }
                resultado = (cantidadLikes / cantidadDeVotaciones * 100);
            }

            necesidad.Valoracion = Math.Round(resultado, 2);

            // necesidad.Valoracion = valoracion;


            Necesidades necesidadBD = necesidadesDAO.Actualizar(necesidad);
            if (necesidadBD == null)
            {
                return null;
            }
            return necesidadBD;
        }


        /// <summary>
        /// Divide en cada espacio con Split, el string ingresado en el buscador
        /// para luego buscar por cada palabra ingresada
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<Necesidades> Buscar(string input)
        {
            int idUser = int.Parse(HttpContext.Current.Session["UserId"].ToString());
            List<Necesidades> necesidades = new List<Necesidades>();
            char delimitador = ' ';
            string[] valores = input.Split(delimitador);

            foreach (var item in valores)
            {
                var necesidadesObtenidas = necesidadesDAO.Buscar(item, idUser);
                foreach (var itemObtenida in necesidadesObtenidas)
                {
                    if (!necesidades.Contains(itemObtenida))
                    {
                        necesidades.Add(itemObtenida);
                    }
                }
            }
            return necesidades;
        }

            public List<Necesidades> obtener5NecesidadesMasValoradas()
            {
                List<Necesidades> listadoNecesidades = necesidadesDAO.ListarTodasLasNecesidadesActivas();
                List<Necesidades> necesidadesMasValoradas = new List<Necesidades>();
                int cantidad = (listadoNecesidades.Count >= 5) ? 5 : listadoNecesidades.Count;

                foreach (var item in listadoNecesidades.OrderByDescending(n => n.Valoracion).ToList())
                {
                    necesidadesMasValoradas.Add(item);

                    if (necesidadesMasValoradas.Count == cantidad)
                    {
                        break;
                    }
                }

                return necesidadesMasValoradas;

            }

        public List<Necesidades> TraerNecesidadesQueNoSonDelUsuario(int idSession)
        {
            List<Necesidades> necesidadesBD = necesidadesDAO.TraerNecesidadesQueNoSonDelUsuario(idSession);
            return necesidadesBD;
        }
    }
    }

