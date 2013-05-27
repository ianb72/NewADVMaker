using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class CommandParameters
    {
        public GameObject firstObject { get; set; }
        public GameObject secondObject { get; set; }
        public GameObject thirdObject { get; set; }

        public Rooms.RoomBase currentRoom { get; set; }

        public string stringParam { get; set; }

        public object mainGameObject { get; set; }

        public object objectContainingCommand { get; set; }

        public CommandParameters()
        {
        }
        public CommandParameters(GameObject firstObject,GameObject secondObject,GameObject thirdObject)
        {
            this.firstObject = firstObject;
            this.secondObject = secondObject;
            this.thirdObject = thirdObject;
        }
        public CommandParameters(GameObject firstObject, GameObject secondObject)
        {
            this.firstObject = firstObject;
            this.secondObject = secondObject;
        }
        public CommandParameters(GameObject firstObject)
        {
            this.firstObject = firstObject;
        }
        public CommandParameters(string stringParam)
        {
            this.stringParam = stringParam;
        }

    }
}
