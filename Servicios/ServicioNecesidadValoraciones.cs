﻿using DAO;
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
            ServicioUsuario servicioUsuario = new ServicioUsuario();
            ServicioNecesidad servicioNecesidad = new ServicioNecesidad();

            NecesidadValoracionesDao necesidadValoracionesDao = new NecesidadValoracionesDao();

            Usuarios usuarioObtenido = servicioUsuario.obtenerUsuarioPorID(idUsuario);
            Necesidades necesidadObtenida = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);

            //Valido si es que antes le dio Like or Dislike
           NecesidadesValoraciones necesidadRegistrada = necesidadValoracionesDao.obtenerNecesidadValoracionPor_IDUsuario_e_IdNecesidad(idUsuario, idNecesidad);

        if (necesidadRegistrada != null)
         { 
            if (botonRecibido == "Like")
            {
                    if (necesidadRegistrada.IdNecesidad == idNecesidad)
                    {
                        if(necesidadRegistrada.Valoracion== "Like") //Si el estado en la BD tenia su MG, se lo remueve para que no quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = null;
                            NecesidadesValoraciones valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            if(valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }else if (necesidadRegistrada.Valoracion == null) //Si el estado en la BD tenia su MG removido, se lo vuelve a poner en MG, para que quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Like";
                            NecesidadesValoraciones valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }
                        
                    }

            }else if (botonRecibido == "DisLike")
            {
                
                    if (necesidadRegistrada.IdNecesidad == idNecesidad)
                    {
                        if (necesidadRegistrada.Valoracion == "Dislike") //Si el estado en la BD tenia su Dislike, se lo remueve para que no quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = null;
                            NecesidadesValoraciones valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }
                        else if (necesidadRegistrada.Valoracion == null) //Si el estado en la BD tenia su MG removido, se lo vuelve a poner en MG, para que quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Dislike";
                            NecesidadesValoraciones valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

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
                necesidadesValoraciones.Usuarios = usuarioObtenido;
                necesidadesValoraciones.Necesidades = necesidadObtenida;
            
                NecesidadesValoraciones valoracionObtenida = necesidadValoracionesDao.Guardar(necesidadesValoraciones);

                if(valoracionObtenida == null)
                {
                    return false;
                }

            }

            return true;
        }

        public List<NecesidadesValoraciones> obtenerValoracionesDelUsuario(int idSession)
        {
            NecesidadValoracionesDao necesidadValoracionesDao = new NecesidadValoracionesDao();
            List<NecesidadesValoraciones> valoracionesDelUsuario = necesidadValoracionesDao.obtenerValoracionesDelUsuario(idSession);
            return valoracionesDelUsuario;
        }
    }
}