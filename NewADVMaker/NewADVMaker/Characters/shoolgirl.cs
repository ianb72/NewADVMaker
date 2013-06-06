using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Characters
{
    public class schoolgirl : Character
    {
        public schoolgirl()
            : base(Gender.Female)
        {
            this.CharacterName = "schoolgirl";
            this.Firstname = "lucy";
            this.alias = "schoolgirl";
            this.alternateNames.Add("girl");
            this.alternateNames.Add("lucy");
            this.alternateNames.Add("schoolgirl");
            this.ObjectName = "schoolgirl";
            this.Surname = NewADVMaker.Surname.Williams;
        }
    }
}
