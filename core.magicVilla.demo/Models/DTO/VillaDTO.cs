using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace core.magicVilla.demo.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string VillaName { get; set; }

        public DateTime CretaedDate { get; set; }

        public string VillaEmail { get; set; }
    }
}
