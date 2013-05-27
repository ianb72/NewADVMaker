using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Clothing
{
    public class MiniSkirt : ClothingBase
    {
        public MiniSkirt()
        {
            this.layer = 2;
            this.ObjectName = "miniskirt";
            this.Description = "slutty little mini skirt";
            this.clothingPosition.Add(Clothing.ClothingPosition.crotch);
        }
    }
}
