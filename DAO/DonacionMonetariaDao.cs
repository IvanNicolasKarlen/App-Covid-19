using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Context;
using Entidades.Views;
using Entidades;
using Entidades.Abstract;

namespace DAO
{
    public class DonacionMonetariaDao : Crud<DonacionesMonetarias> //Uso de Generics
    {
        public override DonacionesMonetarias Actualizar(DonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public void agregarFotoComprobante(VMDonacionMonetaria foto)
        {
            //ToDo: Verificar conexion a la bd, xq esto devuelve null
            context.SaveChanges();

        }

        public override DonacionesMonetarias Crear(DonacionesMonetarias generics)
        {
            throw new NotImplementedException();
        }

        public override DonacionesMonetarias ObtenerPorID(int generics)
        {
            throw new NotImplementedException();
        }
    }
}
