using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class NecesidadesDonacionesMonetariasDTO
    {
        public int IdNecesidadDonacionMonetaria { get; set; }
        public int IdNecesidad { get; set; }
        public decimal Dinero { get; set; }
        public string CBU { get; set; }
        public virtual ICollection<DonacionesMonetariasDTO> DonacionesMonetarias { get; set; }

        public NecesidadesDonacionesMonetariasDTO()
        {
        }

        public NecesidadesDonacionesMonetariasDTO(NecesidadesDonacionesMonetarias necesidadesDonacionesMonetariasEF, bool mapearRelacionadas = true)
        {
            DonacionesMonetariasDTO donacionMonetariasDTO = new DonacionesMonetariasDTO();

            this.IdNecesidadDonacionMonetaria = necesidadesDonacionesMonetariasEF.IdNecesidadDonacionMonetaria;
            this.IdNecesidad = necesidadesDonacionesMonetariasEF.IdNecesidad;
            this.Dinero = necesidadesDonacionesMonetariasEF.Dinero;
            this.CBU = necesidadesDonacionesMonetariasEF.CBU;

            if (mapearRelacionadas && necesidadesDonacionesMonetariasEF.DonacionesMonetarias.Count > 0)
            {
                this.DonacionesMonetarias = donacionMonetariasDTO.MapearListaDTO(necesidadesDonacionesMonetariasEF.DonacionesMonetarias);
            }
        }

         public static List<NecesidadesDonacionesMonetariasDTO> MapearListaEF(List<NecesidadesDonacionesMonetarias> NecesidadesDonacionesMonetariasEF, bool mapearRelacionadas = true)
        {
            List<NecesidadesDonacionesMonetariasDTO> necesidadesDonacionesMonetariasDTO = new List<NecesidadesDonacionesMonetariasDTO>();
            //mapeamos las necesidades a DTO y las agregamos a la lista que quiero retornar
            foreach (NecesidadesDonacionesMonetarias necesidadDonacionMonetariaEF in NecesidadesDonacionesMonetariasEF)
            {
                necesidadesDonacionesMonetariasDTO.Add(new NecesidadesDonacionesMonetariasDTO(necesidadDonacionMonetariaEF, mapearRelacionadas));
            }
            return necesidadesDonacionesMonetariasDTO;
        }
    }
}