using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Objects
{
    public class Door : GameObject
    {
        public bool locked { get; set; }
        public bool closed { get; set; }
        public GameObject keyObject { get; set; }
    }
}
