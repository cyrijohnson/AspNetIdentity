using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetIdentity.WebApi.Models
{
    public class Event
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Assurance Id")]
        public int AssuranceId { get; set; }
        [Required]
        [Display(Name = "Member Id")]
        public int MemberId { get; set; }

        [Display(Name = "Local Assembly Id")]
        public int LocalAssemblyId { get; set; }

        [Display(Name = "Officiating Minister")]
        public int OfficiatingMinister { get; set; }

        [Display(Name = "Salvation Status")]
        public string SalvationStatusText { get; set; }

        [Display(Name = "Date Of Salvation")]
        public string Date { get; set; }

        [Display(Name = "Communicant")]
        public int Communicant { get; set; }
    }
}