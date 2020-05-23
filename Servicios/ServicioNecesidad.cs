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
    }
}