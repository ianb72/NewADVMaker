using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Characters
{
    public class mother : Character
    {
        public mother()
            : base(Gender.Female)
        {
            this.CharacterName = "mother";
            this.Firstname = "jane";
            this.alias = "mother";
            this.alternateNames.Add("mother");
            this.alternateNames.Add("jane");
            this.ObjectName = "mother";
            this.Surname = NewADVMaker.Surname.Williams;
        }
    }
}
