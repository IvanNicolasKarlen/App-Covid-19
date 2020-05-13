using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCovid19.Models.Views;

namespace WebCovid19.Services
{
    public class ServicioDonacion
    {
         internal bool MontoADonarRecibido(VMDonacionMonetaria vmdonacionMonetaria)
        {
            if (vmdonacionMonetaria.Dinero <100)
            {
                return false;
            }
            return true;
        }
    }
}