namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Maintenance")]
    public partial class Maintenance
    {
        public int MaintenanceId { get; set; }

        public int BillNumbar { get; set; }

        [Column(TypeName = "date")]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Wing { get; set; }

        public int? FlatNumber { get; set; }

        [StringLength(500)]
        public string Month { get; set; }

        public decimal MaintenanceCharge { get; set; }

        public decimal? ServiceCharge { get; set; }

        public decimal? VehicalCharge { get; set; }

        public decimal? RentSurcharge { get; set; }

        public decimal? DelayCharge { get; set; }

        public decimal? OutstandingInterest { get; set; }

        public decimal? SinkinFund { get; set; }

        public decimal? ConveyanceDeedOutstanding { get; set; }

        public decimal? OtherCharges { get; set; }

        public decimal? TotalCharge { get; set; }

        public string RupeesInWords { get; set; }

        public int CentreId { get; set; }

        public int OrganisationId { get; set; }

        public virtual Centre Centre { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
