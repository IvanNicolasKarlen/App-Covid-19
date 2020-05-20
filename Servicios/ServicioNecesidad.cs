using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO;
namespace WebCovid19.Services
{
    public class ServicioNecesidad
    {
        NecesidadesDAO necesidadesDAO = new NecesidadesDAO();
        public Necesidades obtenerNecesidadPorId(int? id)
        {
            Necesidades necesidad = new Necesidades();
            //Buscar necesidad por id
            return necesidad;
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
                Estado = 1,
                Valoracion = null
            };

            return necesidadesDAO.CrearNecesidades(necesidades);
        }
    }
}