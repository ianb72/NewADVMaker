using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class CommandParameters
    {
        public GameObject[] commandObjects { get; set; }
        public Dictionary<string, GameObject> gameObjects;
        public Games.GameBase currentGame { get; set; }

        public Rooms.RoomBase currentRoom { get; set; }

        public string stringParam { get; set; }

        public object mainGameObject { get; set; }

        public object objectContainingCommand { get; set; }

        public CommandParameters()
        {
        }
        public CommandParameters(GameObject[] commandObjects)
        {
            this.commandObjects = commandObjects;
        }
        public CommandParameters(string stringParam)
        {
            this.stringParam = stringParam;
        }

    }
}
