using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Rooms
{
    public class RoomBase : GameObject
    {
        public Dictionary<string,GameObject> contents { get; set; }
        public List<Character> peoplePresent { get; set; }
        public Dictionary<string, RoomBase> exits { get; set; }

        public RoomBase()
        {
            this.contents = new Dictionary<string,GameObject>();
            this.peoplePresent = new List<Character>();
            this.exits = new Dictionary<string, RoomBase>();
        }
    }
}
