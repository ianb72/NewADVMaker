using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker
{
    public class Container : GameObject
    {
        public List<GameObject> contents { get; set; }
        public bool isOpen { get; set; }

        public Container()
        {
            contents = new List<GameObject>();
        }
    }
}
