using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Clothing
{
    public class ClothingBase : GameObject
    {
        public int layer { get; set; }
        public bool isVisible { get; set; }
        public List<ClothingPosition> clothingPosition { get; set; }
        public string colour { get; set; }

        public ClothingBase()
        {
            this.isWearable = true;
            this.clothingPosition = new List<ClothingPosition>();
        }
    }
    public enum ClothingPosition
    {
        top,
        waist,
        crotch,
        legs,
        feet
    }
}
