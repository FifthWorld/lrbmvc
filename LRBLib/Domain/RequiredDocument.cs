using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Domain
{
    public class DocumentManager
    {
        public int Id { get; set; }
        public string SurveyPlan { get; set; }
        public string SurveyPlan_Number { get; set; }
        public DateTime? SurveyPlan_ApprovalDate { get; set; }

        public string DevelopmentLevy { get; set; }
        public String DevelopmentLevy_RecieptNumber { get; set; }
        public string EvidenceOfOwnership { get; set; }
        public string EvidenceType { get; set; }
        public string Passport { get; set; }
        public string DateofIncorporation { get; set; }

        public Application Application { get; set; }
    }
}
