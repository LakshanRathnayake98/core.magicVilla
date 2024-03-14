using core.magicVilla.demo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.magicVilla.demo.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO{Id = 1,VillaName ="Pool View",VillaEmail = "kasun123@gmail.com"},
                new VillaDTO{Id = 2,VillaName ="Beach View",VillaEmail = "asela05@gmail.com"}
            };
    }
}
