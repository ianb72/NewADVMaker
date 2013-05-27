using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewADVMaker.Clothing;

namespace NewADVMaker.Objects
{
    public class Strapon : Clothing.ClothingBase
    {
        public Strapon()
        {
            this.Description = "strapon dildo";
            this.ObjectName = "strapon";
            this.layer = 5;
            this.clothingPosition.Add(ClothingPosition.waist);
            this.clothingPosition.Add(ClothingPosition.crotch);
        }
        public override void Wear(Character targetCharacter)
        {
            targetCharacter.HasCock = true;
            targetCharacter.CockLength = 8;
            targetCharacter.CockThickness = 3;
            targetCharacter.CockDescription = this.Description;
        }
        public override void Remove(Character targerCharacter)
        {
            targerCharacter.HasCock = false;
        }
    }
}
