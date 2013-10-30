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
    public class Requirement
    {        
        public string applicationType;
        public string landUse;
        public int landSize;
        public string landSizeUnit;
    }
}