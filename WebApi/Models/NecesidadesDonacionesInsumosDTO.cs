using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class NecesidadesDonacionesInsumosDTO
    {
        public int IdNecesidadDonacionInsumo { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        public virtual ICollection<DonacionInsumosDTO> DonacionesInsumos { get; set; }
      
        public NecesidadesDonacionesInsumosDTO()
        {
        }
        public NecesidadesDonacionesInsumosDTO(NecesidadesDonacionesInsumos necesidadesDonacionesInsumosEF, bool mapearRelacionadas = true)
        {
            DonacionInsumosDTO donacionInsumosDTO = new DonacionInsumosDTO();

            this.IdNecesidadDonacionInsumo = necesidadesDonacionesInsumosEF.IdNecesidadDonacionInsumo;
            this.Nombre = necesidadesDonacionesInsumosEF.Nombre;
            this.Cantidad = necesidadesDonacionesInsumosEF.Cantidad;

            if (mapearRelacionadas && necesidadesDonacionesInsumosEF.DonacionesInsumos.Count > 0)
            {
               this.DonacionesInsumos = donacionInsumosDTO.MapearDTO(necesidadesDonacionesInsumosEF.DonacionesInsumos, true);
            }
        }

        public static List<NecesidadesDonacionesInsumosDTO> MapearListaEF(List<NecesidadesDonacionesInsumos> NecesidadesDonacionesInsumosEF, bool mapearRelacionadas = true)
        {
            List<NecesidadesDonacionesInsumosDTO> necesidadesDonacionesInsumosDTO = new List<NecesidadesDonacionesInsumosDTO>();

            //mapeamos las necesidades a DTO y las agregamos a la lista que quiero retornar
            foreach (NecesidadesDonacionesInsumos necesidadDonacionInsumoEF in NecesidadesDonacionesInsumosEF)
            {
                necesidadesDonacionesInsumosDTO.Add(new NecesidadesDonacionesInsumosDTO(necesidadDonacionInsumoEF, mapearRelacionadas));
            }
            return necesidadesDonacionesInsumosDTO;
        }
    }
}