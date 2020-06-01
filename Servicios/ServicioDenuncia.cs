using DAO;
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
        DenunciasDao denunciasDao = new DenunciasDao();

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
            ServicioNecesidad servicioNecesidad = new ServicioNecesidad();
            NecesidadesDAO necesidadesDAO = new NecesidadesDAO();
            Necesidades necesidadObtenida = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);

            if (estado) //True es para dejarla Inactiva a la necesidad
            {
                necesidadObtenida.Estado = 0;
                Necesidades necesidadActualizada = necesidadesDAO.Actualizar(necesidadObtenida);

                Denuncias denunciaObtenida = denunciasDao.obtenerDenunciaPorIdNecesidad(necesidadObtenida.IdNecesidad);
                denunciaObtenida.Estado = 0;
                Denuncias denunciaActualizada = denunciasDao.Actualizar(denunciaObtenida);

                if (necesidadActualizada == null)
                {
                    return false;
                }
                else if (denunciaActualizada == null)
                {
                    return false;
                }
            }
            else //Al ser false, esta necesidad no le deberia volver a aparecer al Administrador
            {
                Denuncias denunciaObtenida = denunciasDao.obtenerDenunciaPorIdNecesidad(necesidadObtenida.IdNecesidad);
                denunciaObtenida.Estado = 0;
                denunciaObtenida.Necesidades.Denuncias = null; //ToDo: Ver porque me devuelve null en necesidades dentro de Denuncia
                Denuncias denunciaActualizada = denunciasDao.Actualizar(denunciaObtenida);

                if(denunciaActualizada == null)
                {
                    return false;
                }
            }


            return true;
        }
    }
}