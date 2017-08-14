namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ApartmentWing")]
    public partial class ApartmentWing
    {
        public int ApartmentWingId { get; set; }

        public int ApartmentId { get; set; }

        public int WingId { get; set; }

        public virtual Apartment Apartment { get; set; }

        public virtual Wing Wing { get; set; }
    }
}
