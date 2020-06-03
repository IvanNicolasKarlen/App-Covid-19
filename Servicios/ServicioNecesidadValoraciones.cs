using DAO;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ServicioNecesidadValoraciones
    {
        public bool guardarValoracion(int idUsuario, int idNecesidad, string botonRecibido)
        {
            UsuarioDao usuarioDao = new UsuarioDao();
            NecesidadesDAO necesidadesDAO = new NecesidadesDAO();
            NecesidadValoracionesDao necesidadValoracionesDao = new NecesidadValoracionesDao();

            //Obtengo Usuario y Necesidad
            Usuarios usuarioObtenido = usuarioDao.ObtenerPorID(idUsuario);
            Necesidades necesidadObtenida = necesidadesDAO.ObtenerPorID(idNecesidad);

            //Valido si es que antes le dio Like or Dislike
            NecesidadesValoraciones necesidadRegistrada = necesidadValoracionesDao.obtenerNecesidadValoracionPor_IDUsuario_e_IdNecesidad(idUsuario, idNecesidad);

            if (necesidadRegistrada != null)
            {
                NecesidadesValoraciones valoracionObtenidaBD = new NecesidadesValoraciones();
                if (botonRecibido == "Like")
                {
                    if (necesidadRegistrada.IdNecesidad == idNecesidad)
                    {
                        if (necesidadRegistrada.Valoracion == "Like") //Si el estado en la BD tenia su MG, se lo remueve para que no quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Undefined";
                            valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }
                        else if (necesidadRegistrada.Valoracion != "Like") //Si el estado en la BD tenia su MG removido, se lo vuelve a poner en MG, para que quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Like";
                            valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }

                    }

                }
                else if (botonRecibido == "Dislike")
                {

                    if (necesidadRegistrada.IdNecesidad == idNecesidad)
                    {
                        if (necesidadRegistrada.Valoracion == "Dislike") //Si el estado en la BD tenia su Dislike, se lo remueve para que no quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Undefined";
                            valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }
                        else if (necesidadRegistrada.Valoracion != "Dislike") //Si el estado en la BD tenia su MG removido, se lo vuelve a poner en MG, para que quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Dislike";
                            valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }
                    }

                }
            }
            else //Es decir, nunca le habia dado MG a esa publicacion
            {

                //Asigno datos al objeto Necesidad Valoraciones
                NecesidadesValoraciones necesidadesValoraciones = new NecesidadesValoraciones();
                necesidadesValoraciones.IdUsuario = usuarioObtenido.IdUsuario;
                necesidadesValoraciones.IdNecesidad = necesidadObtenida.IdNecesidad;
                // necesidadesValoraciones.Usuarios = usuarioObtenido;
                //necesidadesValoraciones.Necesidades = necesidadObtenida;
                necesidadesValoraciones.Valoracion = (botonRecibido == "Like") ? "Like" : (botonRecibido == "Dislike") ? "Dislike" : null;
                   

                NecesidadesValoraciones valoracionObtenida = necesidadValoracionesDao.Crear(necesidadesValoraciones);
               


                  //  NecesidadesValoraciones valoracionObtenida = necesidadValoracionesDao.Crear(usuarioObtenido, necesidadObtenida);

                if (valoracionObtenida == null)
                {
                    return false;
                }
            }
            ServicioNecesidad servicioNecesidad = new ServicioNecesidad();

            Necesidades necesidadValorada = servicioNecesidad.calcularValoracion(necesidadObtenida);
            if (necesidadValorada == null)
            {
                return false;
            }
            return true;
        }

        public List<NecesidadesValoraciones> obtenerValoracionesPorIDNecesidad(int idNecesidad)
        {
            NecesidadValoracionesDao necesidadValoracionesDao = new NecesidadValoracionesDao();
            List<NecesidadesValoraciones> valoracionesDelaNecesidad = necesidadValoracionesDao.obtenerValoracionesPorIDNecesidad(idNecesidad);
            return valoracionesDelaNecesidad;
        }

        public List<NecesidadesValoraciones> obtenerValoracionesDelUsuario(int idSession)
        {
            NecesidadValoracionesDao necesidadValoracionesDao = new NecesidadValoracionesDao();
            List<NecesidadesValoraciones> valoracionesDelUsuario = necesidadValoracionesDao.obtenerValoracionesDelUsuario(idSession);
            return valoracionesDelUsuario;
        }
    }
}
