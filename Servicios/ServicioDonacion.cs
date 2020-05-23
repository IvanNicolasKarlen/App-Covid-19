using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades.Views;

namespace Servicios
{
    public class ServicioDonacion
    {
        public bool MontoADonarRecibido(VMDonacionMonetaria vmdonacionMonetaria)
        {
            if (vmdonacionMonetaria.Dinero < 100)
            {
                return false;
            }
            return true;
        }
    }
}