using DAO.Context;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class NecesidadValoracionesDao
    {
        TpDBContext context = new TpDBContext();

        public NecesidadesValoraciones obtenerValoracionPorID(int idValoracion)
        {
            NecesidadesValoraciones valoracionObtenida = context.NecesidadesValoraciones.Find(idValoracion);
            return valoracionObtenida;
        }

        public NecesidadesValoraciones Guardar(NecesidadesValoraciones valoracion)
        {
        NecesidadesValoraciones valoracionGuardada = context.NecesidadesValoraciones.Add(valoracion);
        int resultado = context.SaveChanges();
            if(resultado < 0)
            {
                return null;
            }
        return valoracionGuardada;
        }

        public List<NecesidadesValoraciones> obtenerValoracionesDelUsuario(int idSession)
        {
            List<NecesidadesValoraciones> valoracionesObtenidas = context.NecesidadesValoraciones.Where(o => o.IdUsuario == idSession).ToList();
            return valoracionesObtenidas;
        }

        public NecesidadesValoraciones obtenerNecesidadValoracionPor_IDUsuario_e_IdNecesidad(int idUsuario, int idNecesidad)
        {
            NecesidadesValoraciones valoracionObtenida = context.NecesidadesValoraciones.Where(o => o.IdUsuario == idUsuario).Where(n => n.IdNecesidad == idNecesidad).FirstOrDefault();
            return valoracionObtenida;
        }

        public NecesidadesValoraciones Actualizar(NecesidadesValoraciones valoracionNueva)
        {
            NecesidadesValoraciones valoracionObtenida = obtenerValoracionPorID(valoracionNueva.IdValoracion);
            valoracionObtenida.Valoracion = valoracionNueva.Valoracion;
            context.SaveChanges();
            return valoracionObtenida;
        }
    }
}
