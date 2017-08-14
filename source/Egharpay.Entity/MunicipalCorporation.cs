namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MunicipalCorporation")]
    public partial class MunicipalCorporation
    {
        [Key]
        [Column(Order = 0)]
        public int MunicipalCorporationId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}
