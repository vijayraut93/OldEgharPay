namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Apartment")]
    public partial class Apartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apartment()
        {
            ApartmentWings = new HashSet<ApartmentWing>();
        }

        public int ApartmentId { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(500)]
        public string Address3 { get; set; }

        [StringLength(500)]
        public string Address4 { get; set; }

        public int CityId { get; set; }

        public int StateId { get; set; }

        public int Pincode { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        public int MunicipalCorporationId { get; set; }

        public int NumberOfMembers { get; set; }

        [StringLength(500)]
        public string EmailId { get; set; }

        public long? Telephone { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual State State { get; set; }

        public virtual City City { get; set; }

        public virtual MunicipalCorporation MunicipalCorporation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmentWing> ApartmentWings { get; set; }
    }
}
