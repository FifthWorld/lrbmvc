using ApplicationLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLibrary
{
    class SolaPartyService
    {
        private Party _party;
        private Application _application;

        public Application App
        {
            get { return _application; }
            set { _application = value; }
        }

        public Party Party
        {
            get { return _party; }
            set { _party = value; }
        }

        public SolaPartyService(Application app)
        {
            app = App;
            if (app.Parties.Count > 0)
	        {
		         Party= app.Parties.First();
	        }
        }
    }
}
