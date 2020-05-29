using DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.ValidationCustom
{
    public class ValidarSiExisteEmail : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                ErrorMessage = "No existe ese email, debera registrarse primero";
                return false;
            }

            UsuarioDao usuarioDao = new UsuarioDao();

            Usuarios usuarioExistente = usuarioDao.obtenerUsuarioPorEmail((string)value);
            if (usuarioExistente == null)
            {
                ErrorMessage = "No existe ese email, debera registrarse primero";
                return false;
            }

            return true;
            
        }
    }          
}
