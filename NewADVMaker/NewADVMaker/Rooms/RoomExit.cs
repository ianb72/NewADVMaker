using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Rooms
{
    public class RoomExit
    {
        public Objects.Door door { get; set; }
        public string direction { get; set; }
        public bool hasDoor { get; set; }

        public RoomExit(string direction, bool hasDoor, Objects.Door door)
        {
            this.direction = direction;
            this.hasDoor = hasDoor;
            this.door = door;
        }
        public RoomExit(string direction)
            :this(direction,false,null)
        {
            
        }
        
    }
}
