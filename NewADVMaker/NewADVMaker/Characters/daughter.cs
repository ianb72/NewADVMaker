using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Characters
{
    public class daughter : Character
    {
        public daughter()
            : base(Gender.Female)
        {
            this.CharacterName = "daughter";
            this.Firstname = "wendy";
            this.alias = "daughter";
            this.alternateNames.Add("girl");
            this.alternateNames.Add("wendy");
            this.alternateNames.Add("daughter");
            this.ObjectName = "daughter";
            this.Surname = NewADVMaker.Surname.Smith;
        }
    }
}
