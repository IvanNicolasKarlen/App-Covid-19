/*using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using DAO.Abstract;

namespace DAO
{
    public class NecesidadesDAO : Crud<Necesidades>
    {
        public override Necesidades ObtenerPorID(int idNecesidad)
        {
            Necesidades necesidad = context.Necesidades.Find(idNecesidad);
            return necesidad;
        }
        public override Necesidades Crear(Necesidades necesidadAGuardar)
        {
            Necesidades necesidades = context.Necesidades.Add(necesidadAGuardar);
            context.SaveChanges();
            return necesidades;
        }

        public List<Necesidades> TraerNecesidadesActivasDelUsuario(int idSession)
        {
            List<Necesidades> necesidadesActivas = (from c in context.Necesidades
                                                    where c.IdUsuarioCreador == idSession
                                                    where c.Estado == 1
                                                    where c.FechaFin > DateTime.Now
                                                    select c).ToList();
            return necesidadesActivas;
        }

        public List<Necesidades> TraerTodasLasNecesidadesDelUsuario(int idSession)
        {
            List<Necesidades> todasLasNecesidadesDelUsuario = (from c in context.Necesidades
                                        where c.IdUsuarioCreador == idSession
                                        where c.FechaFin > DateTime.Now
                                        select c).ToList();

            return todasLasNecesidadesDelUsuario;
        }


        public List<Necesidades> ListarTodasLasNecesidades()
        {
            List<Necesidades> listadoNecesidades = new List<Necesidades>();

            var listaObtenida = (from nec in context.Necesidades
                                 where nec.FechaFin > DateTime.Now
                                 select nec);

            foreach (var item in listaObtenida)
            {
                listadoNecesidades.Add(item);
            }

            return listadoNecesidades;
        }

        public override Necesidades Actualizar(Necesidades necesidadObtenida)
        {
            {
                Necesidades necesidadBd = ObtenerPorID(necesidadObtenida.IdNecesidad);

                necesidadBd.Valoracion = necesidadObtenida.Valoracion;
                necesidadBd.Descripcion = necesidadObtenida.Descripcion;
                necesidadBd.Estado = necesidadObtenida.Estado;
                necesidadBd.Foto = necesidadObtenida.Foto;
                necesidadBd.Nombre = necesidadObtenida.Nombre;
                necesidadBd.TelefonoContacto = necesidadObtenida.TelefonoContacto;
                necesidadBd.NecesidadesValoraciones = necesidadObtenida.NecesidadesValoraciones;
           

                context.SaveChanges();
                return necesidadBd;
            }
        }

        

    }
}
*/