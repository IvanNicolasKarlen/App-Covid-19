using DAO;
using DAO.Context;
using Entidades;
using Entidades.Enum;
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
        /// Guardar la denuncia 
        /// </summary>
        /// <param name="denuncia"></param>
        /// <returns>True o False</returns>
        public Denuncias GuardarDenuncia(Denuncias denuncia, int idUsuario)
        {
            denuncia.FechaCreacion = DateTime.Now;
            denuncia.IdUsuario = idUsuario;

            return denunciasDao.Crear(denuncia);
        }

        public List<MotivoDenuncia> ObtenerMotivosDenuncia()
        {
            return denunciasDao.ObtenerMotivosDenuncia();
        }

        public List<Denuncias> ObtenerDenuncias()
        {
            List<Denuncias> listaDenuncias = denunciasDao.ObtenerDenuncias();
            return listaDenuncias;
        }

        public bool necesidadEvaluada(int idNecesidad, bool estado)
        {
             Denuncias denunciaObtenida = denunciasDao.obtenerDenunciaPorIdNecesidad(idNecesidad);
            if (estado) //True es para dejarla bloqueada/Inactiva a la Necesidad
            {
               
                if (denunciaObtenida == null)
                {
                    return false;
                }
                
                //Pongo la necesidad en estado inactivo
                denunciaObtenida.Necesidades.Estado = (int)TipoEstadoNecesidad.Bloqueada;
                denunciaObtenida.Estado = (int)TipoEstadoDenuncia.Revisada; // 1 revisada
                //Actualizo el estado
                Denuncias denunciaActualizada = denunciasDao.Actualizar(denunciaObtenida);
               
                if (denunciaActualizada == null)
                    {
                        return false;
                    }
                }
            else //Al ser false, esta necesidad no le deberia volver a aparecer al Administrador
            {
                if (denunciaObtenida == null)
                {
                    return false;
                }

               
                // Enum.GetValues(typeof(TipoDonacion)).ToString() ;

                denunciaObtenida.Necesidades.Estado = (int)TipoEstadoNecesidad.Activa; // activa 1
                denunciaObtenida.Estado = (int)TipoEstadoDenuncia.Revisada;
                Denuncias denunciaActualizada = denunciasDao.Actualizar(denunciaObtenida);
                
            }


                return true;
        }
    }
}