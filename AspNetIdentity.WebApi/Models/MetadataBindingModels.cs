using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetIdentity.WebApi.Models
{
    public class MetaBlock
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Block Id")]
        public int BlockId { get; set; }

        [Required]
        [Display(Name = "Block Name")]
        public string BlockName { get; set; }

        [Required]
        [Display(Name = "Block Description")]
        public string BlockDescription { get; set; }
        
        [Required]
        [Display(Name = "Block Coordinator")]
        public string UserId { get; set; }
    }
}