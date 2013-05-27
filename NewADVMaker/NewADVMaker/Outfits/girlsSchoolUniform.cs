using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewADVMaker.Clothing;

namespace NewADVMaker.Outfits
{
    public class girlsSchoolUniform : OutfitBase
    {
        public girlsSchoolUniform()
        {
            MiniSkirt schoolSkirt = new MiniSkirt();
            schoolSkirt.ObjectName = "skirt";
            schoolSkirt.colour = "navy blue";
            schoolSkirt.Description = "pretty school skirt";

            Blouse schoolBlouse = new Blouse();
            schoolBlouse.ObjectName = "blouse";
            schoolBlouse.Description = "almost sexy school blouse";

            Panties cottonPanties = new Panties();
            cottonPanties.colour = "white";
            cottonPanties.Description = "plain cotton panties";
            cottonPanties.ObjectName = "panties";

            this.clothing.Add(schoolSkirt.ObjectName, schoolSkirt);
            this.clothing.Add(schoolBlouse.ObjectName, schoolBlouse);
            this.clothing.Add(cottonPanties.ObjectName, cottonPanties);
        }
    }
}
