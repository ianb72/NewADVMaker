using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewADVMaker.Clothing;

namespace NewADVMaker.Outfits
{
    public class SexyCasual : OutfitBase
    {
        public SexyCasual()
        {
            this.clothing.Add("panties", new Panties());
            this.clothing.Add("blouse", new Blouse());
            this.clothing.Add("miniskirt", new MiniSkirt());
            this.clothing.Add("bra", new Bra());
        }
    }
}
