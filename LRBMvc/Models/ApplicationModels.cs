using LRB;
using LRB.Lib;
using LRB.Lib.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LRBMvc.Models
{
    class SurveyPlan
    {
        public string SurveyPlan_Number { get; set; }
        public DateTime? SurveyPlan_ApprovalDate { get; set; }
    }

    class DevelopmentLevy
    {
        public string RecieptNumber { get; set; }
    }

    class EvidenceOfOwnership
    {
        public String EvidenceType { get; set; }
        public string EvidenceNumber { get; set; }
    }
}