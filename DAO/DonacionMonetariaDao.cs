using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Context;
using Entidades.Views;
using Entidades;

namespace DAO
{
    public class DonacionMonetariaDao
    {
        TpDBContext context = new TpDBContext();

        public void agregarFotoComprobante(VMDonacionMonetaria foto)
        {
            //ToDo: Verificar conexion a la bd, xq esto devuelve null
            context.SaveChanges();

        }
    }
}
