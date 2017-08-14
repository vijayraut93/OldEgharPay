using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egharpay.Entity
{
    [Table("Personnel")]
    public partial class Personnel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personnel()
        {
        }

        public int PersonnelId { get; set; }

        public int OrganisationId { get; set; }

        public int CentreId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Forenames { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DOB { get; set; }

        public int CountryId { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        [Required]
        [StringLength(12)]
        public string Postcode { get; set; }

        [StringLength(15)]
        public string Telephone { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(10)]
        public string PANNumber { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Centre Centre { get; set; }

    }
}
