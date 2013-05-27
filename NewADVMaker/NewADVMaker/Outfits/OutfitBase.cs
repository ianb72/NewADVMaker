using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewADVMaker.Clothing;

namespace NewADVMaker.Outfits
{
    public class OutfitBase : NewADVMaker.Clothing.ClothingBase
    {
        public Dictionary<string, ClothingBase> clothing { get; set; }

        public OutfitBase()
        {
            this.clothing = new Dictionary<string, ClothingBase>();
        }
    }
}
