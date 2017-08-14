using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Egharpay.Entity;

namespace Egharpay.Models
{
    public class ApartmentViewModel : BaseViewModel
    {
        public Apartment Apartment { get; set; }
        public SelectList Cities { get; set; }
        public SelectList States { get; set; }
        public SelectList MunicipalCorporations { get; set; }
        public SelectList CreatedBys { get; set; }
    }
}