using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Clothing
{
    public class Panties : ClothingBase
    {
        public Panties(string objectName)
        {
            this.layer = 1;
            this.ObjectName = objectName;
            this.Description = "pretty pink panties";
            this.clothingPosition.Add(Clothing.ClothingPosition.crotch);
        }
        public Panties()
        {
            this.layer = 1;
            this.ObjectName = "panties";
            this.Description = "pretty pink panties";
            this.clothingPosition.Add(Clothing.ClothingPosition.crotch);
        }

         
    }
}
