namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaintenanceGrid")]
    public partial class MaintenanceGrid
    {
        [Key]
        [Column(Order = 0)]
        public int MaintenanceId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillNumbar { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime MaintenanceDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string MiddleName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Wing { get; set; }

        public int? FlatNumber { get; set; }

        [StringLength(500)]
        public string Month { get; set; }

        [Key]
        [Column(Order = 7)]
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

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CentreId { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganisationId { get; set; }

        [StringLength(1080)]
        public string SearchField { get; set; }
    }
}
