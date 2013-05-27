using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Clothing
{
    public class Jeans : ClothingBase
    {
        public Jeans()
        {
            this.layer = 5;
            this.ObjectName = "jeans";
            this.Description = "tight crotch hugging jeans";
            this.clothingPosition.Add(ClothingPosition.crotch);
            this.clothingPosition.Add(ClothingPosition.legs);
        }
    }
}
