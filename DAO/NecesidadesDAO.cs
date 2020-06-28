using DAO.Abstract;
using DAO.Context;
using Entidades;
using Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace DAO
{
    public class NecesidadesDAO : Crud<Necesidades>
    {
        #region Crud
        public NecesidadesDAO(TpDBContext context) : base(context)
        {

        }
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
                                                               where c.IdUsuarioCreador.Equals(idSession)
                                                               where c.FechaFin > DateTime.Now
                                                               select c).ToList();

            return todasLasNecesidadesDelUsuario;
        }


        public List<Necesidades> ListarTodasLasNecesidades()
        {
            List<Necesidades> listadoNecesidades = new List<Necesidades>();

            var listaObtenida = (from nec in context.Necesidades
                                 where nec.FechaFin > DateTime.Now
                                 where nec.Estado == 1
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

                necesidadBd.Valoracion = (decimal)necesidadObtenida.Valoracion;
                necesidadBd.Descripcion = necesidadObtenida.Descripcion;
                necesidadBd.Estado = necesidadObtenida.Estado;
                necesidadBd.Foto = necesidadObtenida.Foto;
                necesidadBd.Nombre = necesidadObtenida.Nombre;
                necesidadBd.TelefonoContacto = necesidadObtenida.TelefonoContacto;
                necesidadBd.NecesidadesValoraciones = necesidadObtenida.NecesidadesValoraciones;


                try
                {

                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                return necesidadBd;
            }
        }
        public void ActivarNecesidad(int idNecesidad)
        {
            Necesidades n = context.Necesidades.Find(idNecesidad);
            n.Estado = 1;
            context.SaveChanges();
        }
        #endregion
        #region otros
        /// <summary>
        /// Buscar necesidades en relación al nombre de las necesidades existentes o bien según el nombre del
        /// usuario creador. Ordenado por fecha más cercana de cierre de necesidad y, luego,
        /// por mayor valoración de la necesidad.El resultado de la búsqueda no deberá incluir sus propias
        /// necesidades
        /// </summary>
        /// <param name="input"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public List<Necesidades> Buscar(string input, int idUser)
        {
            List<Necesidades> necesidadesObtenidas =
              (
              from necesidad in context.Necesidades.Include("Usuarios")
              where necesidad.Usuarios.Nombre.Contains(input) || necesidad.Nombre.Contains(input)
              where !necesidad.IdUsuarioCreador.Equals(idUser)
              select necesidad
              ).OrderBy(o => o.FechaFin).ThenByDescending(o => o.Valoracion).ToList();
            return necesidadesObtenidas;
        }

        public List<Necesidades> ListarTodasLasNecesidadesActivas()
        {
            List<Necesidades> listadoNecesidades = new List<Necesidades>();

            var listaObtenida = (from nec in context.Necesidades
                                 where nec.FechaFin > DateTime.Now
                                 where nec.Estado == 1
                                 select nec);

            foreach (var item in listaObtenida)
            {
                listadoNecesidades.Add(item);
            }

            return listadoNecesidades;

        }

        public List<Necesidades> TraerNecesidadesQueNoSonDelUsuario(int idSession)
        {
            List<Necesidades> listaNecesidades = new List<Necesidades>();

            var listaObtenida = (from nec in context.Necesidades
                                 where nec.FechaFin > DateTime.Now
                                 where nec.Estado == 1
                                 where !nec.IdUsuarioCreador.Equals(idSession)
                                 select nec);

            foreach (var item in listaObtenida)
            {
                listaNecesidades.Add(item);
            }

            return listaNecesidades;
        }
        public List<Necesidades> obtenerNecesidadesDenunciadas()
        {
            List<Necesidades> necesidadesBD = context.Necesidades.Where(o => o.Estado == (int)TipoEstadoNecesidad.Revision).ToList();
            return necesidadesBD;
        }

        public void EditarNecesidad(Necesidades n)
        {
            if(context.Entry(n).State == EntityState.Modified){
                context.SaveChanges();
            }     
        }

        public List<Necesidades> TraerNecesidadesConDonacionInsumosPorUserLogueado(int idUserLogueado)
        {
            List<Necesidades> listadoNecesidades = (from nec in context.Necesidades

                                                    join necDonacionesInsumos in context.NecesidadesDonacionesInsumos
                                                    on nec.IdNecesidad equals necDonacionesInsumos.IdNecesidad
                                                    join DonInsumos in context.DonacionesInsumos
                                                    on necDonacionesInsumos.IdNecesidadDonacionInsumo equals DonInsumos.IdNecesidadDonacionInsumo

                                                    where DonInsumos.IdUsuario == idUserLogueado
                                                    orderby DonInsumos.FechaCreacion descending
                                                    select nec).ToList();


            return listadoNecesidades;
        }

        public List<Necesidades> TraerNecesidadesConDonacionMonetariasPorUserLogueado(int idUserLogueado)
        {

            List<Necesidades> listadoNecesidades =
                (
                from nec in context.Necesidades
                join necDonacionesMonetarias in context.NecesidadesDonacionesMonetarias
                on nec.IdNecesidad equals necDonacionesMonetarias.IdNecesidad
                join DonMonetarias in context.DonacionesMonetarias
                on necDonacionesMonetarias.IdNecesidadDonacionMonetaria equals DonMonetarias.IdNecesidadDonacionMonetaria


                where DonMonetarias.IdUsuario == idUserLogueado
                orderby DonMonetarias.FechaCreacion descending
                select nec
                         ).ToList();
            return listadoNecesidades;
        }

        public List<NecesidadesReferencias> ObtenerReferenciasPorIdNecesidad(int id)
        {
            return (List<NecesidadesReferencias>)context.NecesidadesReferencias.Where(o => o.IdNecesidad == id).ToList();
        }

        public void ModificarReferencia(NecesidadesReferencias referencia)
        {
            NecesidadesReferencias r = context.NecesidadesReferencias.Find(referencia.IdReferencia);
            r.Nombre = referencia.Nombre;
            r.Telefono = referencia.Telefono;
            context.SaveChanges();
        }

        #endregion
        #region Insumos y Monetaria
        public NecesidadesDonacionesInsumos AgregarInsumos(NecesidadesDonacionesInsumos insumo)
        {
            NecesidadesDonacionesInsumos i = context.NecesidadesDonacionesInsumos.Add(insumo);
            context.SaveChanges();
            return i;
        }
        public NecesidadesDonacionesMonetarias AgregarMonetaria(NecesidadesDonacionesMonetarias monetaria)
        {
            NecesidadesDonacionesMonetarias m = context.NecesidadesDonacionesMonetarias.Add(monetaria);
            context.SaveChanges();
            return m;
        }
        public void ActualizarInsumos(NecesidadesDonacionesInsumos insumo)
        {
            if(context.Entry(insumo).State == EntityState.Modified)
            {
                context.SaveChanges();
            }       
        }
        public void ActualizarMonetaria(NecesidadesDonacionesMonetarias monetaria)
        {
            if (context.Entry(monetaria).State == EntityState.Modified)
            {
                context.SaveChanges();
            }
        }

        public NecesidadesDonacionesInsumos BuscarInsumoPorId(int id)
        {
            return context.NecesidadesDonacionesInsumos.Find(id);
        }

        public NecesidadesDonacionesMonetarias BuscarMonetariasPorId(int id)
        {
            return context.NecesidadesDonacionesMonetarias.Find(id);
        }

        public List<NecesidadesDonacionesInsumos> BuscarInsumosPorIdNecesidad(int id)
        {
            return (List<NecesidadesDonacionesInsumos>)context.NecesidadesDonacionesInsumos.Where(o => o.IdNecesidad == id).ToList();
        }
        public List<NecesidadesDonacionesMonetarias> BuscarMonetariasPorIdNecesidad(int id)
        {
            return (List<NecesidadesDonacionesMonetarias>)context.NecesidadesDonacionesMonetarias.Where(o => o.IdNecesidad == id).ToList();
        }
        #endregion
        #region Referencias
        public void AgregarReferencia(NecesidadesReferencias nr)
        {
            context.NecesidadesReferencias.Add(nr);
        }
        #endregion
    }
}
