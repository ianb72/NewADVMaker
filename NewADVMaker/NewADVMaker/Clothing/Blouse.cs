using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Clothing
{
    public class Blouse : ClothingBase
    {
        public Blouse()
        {
            this.layer = 4;
            this.ObjectName = "blouse";
            this.Description = "sexy cotton blouse";
            this.clothingPosition.Add(Clothing.ClothingPosition.top);
            this.clothingPosition.Add(Clothing.ClothingPosition.waist);
        }

    }
}
