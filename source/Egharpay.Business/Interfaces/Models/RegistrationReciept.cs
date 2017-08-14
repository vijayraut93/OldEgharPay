﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egharpay.Business.Models
{
    public class RegistrationReciept
    {
        public string OrganisationName { get; set; }
        public string CentreAddress { get; set; }
        public string CentreName { get; set; }
        public string CandidateName { get; set; }
        public string EmailId { get; set; }
        public string CandidateAddress { get; set; }
        public string MobileNumber { get; set; }
        public string CourseName { get; set; }
        public string CourseDuration { get; set; }
        public string TotalCourseFee { get; set; }
        public string RecievedAmount { get; set; }
        public string PaymentDate { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
