using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Characters
{
    public class youngboy : Character
    {
        public youngboy()
            : base(Gender.Male)
        {
            this.Age = 11;
            this.Firstname = "luke";
            this.ObjectName = "youngboy";
        }
    }
}
