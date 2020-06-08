using DAO;
using DAO.Context;
using Entidades;
using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace Servicios
{
    public class ServicioDenuncia
    {
        DenunciasDao denunciasDao;
        ServicioNecesidad servicioNecesidad;
        NecesidadesDAO necesidadesDAO;
        public ServicioDenuncia(TpDBContext context)
        {
            denunciasDao = new DenunciasDao(context);
            servicioNecesidad = new ServicioNecesidad(context);
            necesidadesDAO = new NecesidadesDAO(context);
        }


        /// <summary>
        /// Guardar la denuncia y validar si es necesario enviarselo al Admin o no
        /// </summary>
        /// <param name="denuncia"></param>
        /// <returns>True o False</returns>
        public bool guardarDenuncia(Denuncias denuncia)
        {
            List<Denuncias> denunciasObtenidas = new List<Denuncias>();
            //Registro la denuncia
            //Recibo lista de denuncias de esa publicacion
            //Validar si supera a 5 para enviarle la publicacion al admin.

            //Validar si se guardo 



            if (denunciasObtenidas.Count >= 5)
            {
                //Enviarsela al admin
            }


            return true;
        }

        public List<Denuncias> obtenerDenuncias()
        {
            List<Denuncias> listaDenuncias = denunciasDao.obtenerDenuncias();
            return listaDenuncias;
        }

        public bool necesidadEvaluada(int idNecesidad, bool estado)
        {
         

            if (estado) //True es para dejarla bloqueada/Inactiva a la Necesidad
            {
                Denuncias denunciaObtenida = denunciasDao.obtenerDenunciaPorIdNecesidad(idNecesidad);
                if (denunciaObtenida == null)
                {
                    return false;
                }
                
                //Pongo la necesidad en estado inactivo
                denunciaObtenida.Necesidades.Estado = 0;
                //Actualizo el estado
                Denuncias denunciaActualizada = denunciasDao.Actualizar(denunciaObtenida);
                //Elimino la denuncia realizada
                denunciasDao.Eliminar(denunciaObtenida);

                if (denunciaActualizada == null)
                    {
                        return false;
                    }
                


                }
            else //Al ser false, esta necesidad no le deberia volver a aparecer al Administrador
            {
                Denuncias denunciaObtenida = denunciasDao.obtenerDenunciaPorIdNecesidad(idNecesidad);
                if (denunciaObtenida == null)
                {
                    return false;
                }

                    denunciasDao.Eliminar(denunciaObtenida);
                
            }


                return true;
        }
    }
}