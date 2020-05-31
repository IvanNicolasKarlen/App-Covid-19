using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DAO.Context;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace DAO
{
    public class NecesidadesDAO
    {
        TpDBContext context = new TpDBContext();

        public Necesidades BuscarNecesidad(int idNecesidad)
        {
            Necesidades necesidad = context.Necesidades.Find(idNecesidad);
            return necesidad;
        }
        public Necesidades CrearNecesidades(Necesidades necesidadAGuardar)
        {
            Necesidades necesidades = context.Necesidades.Add(necesidadAGuardar);
            context.SaveChanges();
            return necesidades;
        }

        public List<Necesidades> necesidadesActivas(int idSession)
        {
            List<Necesidades> necesidadesActivas = new List<Necesidades>();
            var necesidadesObtenidas = (from c in context.Necesidades
                                        where c.IdUsuarioCreador == idSession
                                        where c.Estado == 1
                                        where c.FechaFin > DateTime.Now
                                        select c);

            foreach (var item in necesidadesObtenidas)
            {
                necesidadesActivas.Add(item);
            }

            return necesidadesActivas;
        }

        public List<Necesidades> necesidadesDelUsuario(int idSession)
        {
            List<Necesidades> todasLasNecesidadesDelUsuario = new List<Necesidades>();
            
            var necesidadesObtenidas = (from c in context.Necesidades
                                        where c.IdUsuarioCreador == idSession
                                        where c.FechaFin > DateTime.Now
                                        select c);

            foreach (var item in necesidadesObtenidas)
            {
                todasLasNecesidadesDelUsuario.Add(item);
            }


            return todasLasNecesidadesDelUsuario;
        }

        public List<Necesidades> listadoNecesidades()
        {
            //  List < Necesidades > listadoNecesidades = context.Necesidades.Where(o => o.FechaFin > DateTime.Now)

            /*.Where(o=> o.FechaFin.Hour < DateTime.UtcNow.Hour)
            .Where(o => o.FechaFin.Minute < DateTime.UtcNow.Minute)
            .Where(o => o.FechaFin.Second < DateTime.UtcNow.Second)
            .ToList();*/

            List<Necesidades> listadoNecesidades = new List<Necesidades>();

            var listaObtenida = (from nec in context.Necesidades
                                // join user in context.Usuarios on nec.IdUsuarioCreador equals user.IdUsuario
                                 where nec.FechaFin > DateTime.Now
                                 select nec);

            foreach (var item in listaObtenida)
            {
                listadoNecesidades.Add(item);
            }

            return listadoNecesidades;
        }
    }
}
