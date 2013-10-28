using LRB.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRB.Lib.Repositories
{
    public class LandApplicationRepository:GenericRepository<Application>
    {
        public LandApplicationRepository(LandsContext context) : base(context) { }
    }
}
