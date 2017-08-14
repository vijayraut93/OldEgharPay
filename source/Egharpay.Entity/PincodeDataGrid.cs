namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PincodeDataGrid")]
    public partial class PincodeDataGrid
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PincodeId { get; set; }

        [StringLength(255)]
        public string OfficeName { get; set; }

        public double? PincodeNumber { get; set; }

        [StringLength(255)]
        public string OfficeType { get; set; }

        [StringLength(255)]
        public string DeliveryStatus { get; set; }

        [StringLength(255)]
        public string DivisionName { get; set; }

        [StringLength(255)]
        public string RegionName { get; set; }

        [StringLength(255)]
        public string CircleName { get; set; }

        [StringLength(255)]
        public string TalukaName { get; set; }

        [StringLength(255)]
        public string DistrictName { get; set; }

        [StringLength(255)]
        public string StateName { get; set; }

        [StringLength(255)]
        public string Telephone { get; set; }

        [StringLength(255)]
        public string RelatedSubOffice { get; set; }

        [StringLength(255)]
        public string RelatedHeadOffice { get; set; }

        [StringLength(255)]
        public string Longitude { get; set; }

        [StringLength(255)]
        public string Latitude { get; set; }

        [StringLength(2325)]
        public string SearchField { get; set; }
    }
}
