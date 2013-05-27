using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Clothing
{
    public class Bra : ClothingBase
    {
        public Bra()
        {
            this.layer = 1;
            this.ObjectName = "bra";
            this.Description = "sexy pink bra";
            this.clothingPosition.Add(Clothing.ClothingPosition.top);
        }
    }
}
