using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NewADVMaker.Commands
{
    public class StandardCommandList : Interfaces.IcommandList
    {
        public MainGameForm.MessageHandler messageHandler { get; set; }
        public Games.GameBase currentGame { get; set; }

        public StandardCommandList(Games.GameBase currentGame, MainGameForm.MessageHandler messageHandler)
        {
            this.currentGame = currentGame;
            this.messageHandler = messageHandler;
        }

        public void msg(CommandParameters commandParameters, msgParams messageParams)
        {
            messageParams.commandParameters = commandParameters;
            messageHandler.Invoke(messageParams);
        }
        public void msg(string message)
        {
            messageHandler.Invoke(new msgParams(message));
        }
        public void msg(CommandParameters commandParameters, string message)
        {
            msgParams newmsgParams = new msgParams(message);
            newmsgParams.commandParameters = commandParameters;
            messageHandler.Invoke(new msgParams(message));
        }

        public void look_at(CommandParameters commandParameters)
        {
          
        }
        public void marry(CommandParameters commandParameters)
        {
           
        }
        public void proposeto(CommandParameters commandParameters)
        {
          
        }
        public void listchars(CommandParameters commandParameters)
        {
           
        }
        public void random(CommandParameters commandParameters)
        {
        }
        public void random_male(CommandParameters commandParameters)
        {
        }
        public void random_female(CommandParameters commandParameters)
        {
        }
        public void random_shemale(CommandParameters commandParameters)
        {
        }
        public void random_herm(CommandParameters commandParameters)
        {
        }
        public void rub(CommandParameters commandParameters)
        {

        }
        public void look(CommandParameters commandParameters)
        {
            msg(commandParameters, new msgParams("You are in the [currentRoom.ObjectName]\n"));
            msg("You can see\n");


            foreach (KeyValuePair<string,GameObject> gameObject in commandParameters.currentRoom.contents )
            {
                msg(null, new msgParams("A " + gameObject.Value.ObjectName + "\n",false,false,Color.Blue));
            }

            msg("\n");

            foreach (Character character in commandParameters.currentRoom.peoplePresent )
            {
                commandParameters.gameObjects["char1"] = character;
                msg(commandParameters, new msgParams("[char1.alias.uf]\n",false,false,Color.Green));
            }

            if (commandParameters.currentRoom.exits.Count == 0)
            {
                msg("\nThere are no exits");
            }
            else
            {
                foreach (KeyValuePair<Rooms.RoomExit, Rooms.RoomBase> kvp in commandParameters.currentRoom.exits)
                {
                    if (kvp.Key.hasDoor && kvp.Key.door.closed)
                    {
                         msg(string.Format("\nThere is a closed door to the {0}", kvp.Key.direction));
                    }
                    else
                    {
                        msg(string.Format("\nYou can go {0} to the {1}", kvp.Key.direction, kvp.Value.ObjectName));
                    }
                }
            }

        }
        public void go(CommandParameters commandParameters)
        {
            try
            {
                Rooms.RoomBase roomToEnter = currentGame.currentRoom.exits.Where(p => p.Key.direction == commandParameters.commandObjects[1].ObjectName).First().Value;
                currentGame.EnterRoom(roomToEnter);
            }
            catch
            {
                msg("\nYou can't go " + commandParameters.commandObjects[1].ObjectName);
            }
        }
        public void exits(CommandParameters commandParameters)
        {
          
        }
        public void call(CommandParameters commandParameters)
        {
            
        }
        public void wear(CommandParameters commandParameters)
        {
            
        }
        public void remove(CommandParameters commandParameters)
        {
            
        }
        public void get_on(CommandParameters commandParameters)
        {
           
        }
        public void use(CommandParameters commandParameters)
        {
        }
        public void kiss(CommandParameters commandParameters)
        {
            
        }
        public void examine(CommandParameters commandParameters)
        {

        }
        public void clothing(CommandParameters commandParameters)
        {
           

        }
        public void open(CommandParameters commandParameters)
        {
            string[] splitStringParams = commandParameters.stringParam.Split(' ');

            if (commandParameters.stringParam.Contains("door"))
            {
                ChangeDoorState(commandParameters,splitStringParams,false);
            }
        }
        public void close(CommandParameters commandParameters)
        {
            string[] splitStringParams = commandParameters.stringParam.Split(' ');

            if (commandParameters.stringParam.Contains("door"))
            {
                ChangeDoorState(commandParameters, splitStringParams, true);
                return;
            }
        }
        public void i(CommandParameters commandParameters)
        {
            inventory(commandParameters);
        }
        public void inventory(CommandParameters commandParameters)
        {
            Character requestedChar = (Character)commandParameters.commandObjects[1] != null ? (Character)commandParameters.commandObjects[1] : currentGame.player;

            commandParameters.gameObjects["char1"] = requestedChar;

            msg("\n[char1.alias.uf] <replace,are,char1> carrying :-\n");
            if (requestedChar.inventory.Count == 0) { msg("Nothing\n"); return; }

            foreach (GameObject item in requestedChar.inventory)
            {
                if (item != null)
                {
                    msg(item.ObjectName + "\n");
                }
            }
            msg("\n"); 
        }
        public void take(CommandParameters commandParameters)
        {
            Character firstCharacter = (Character)commandParameters.commandObjects[0];

            firstCharacter.inventory.Add(commandParameters.commandObjects[1]);
            currentGame.currentRoom.contents.Remove(commandParameters.commandObjects[1].ObjectName);
            commandParameters.gameObjects["char1"] = firstCharacter;

            msg(commandParameters,new msgParams("\n[char1.alias.uf] <replace,take,char1> the " + commandParameters.commandObjects[1].ObjectName + "\n"));
        }
        public void put_away(CommandParameters commandParameters)
        {
          
        }
        public void drop(CommandParameters commandParameters)
        {
            GameObject objectToDrop = commandParameters.commandObjects[1];
            Character firstCharacter = (Character)commandParameters.commandObjects[0];
            if (!firstCharacter.inventory.Contains(objectToDrop))
            {
                msg("\nYou do not have a " + objectToDrop.ObjectName + " to drop");
                return;
            }
            firstCharacter.inventory.Remove(objectToDrop);
            currentGame.currentRoom.contents.Add(objectToDrop.ObjectName, objectToDrop);

            msg(commandParameters, new msgParams("\n[char1.alias.uf] <replace,drop,char1> the " + commandParameters.commandObjects[1].ObjectName + "\n"));
        }
        public void give(CommandParameters commandParameters)
        {
            GameObject objectToGive = (GameObject)commandParameters.commandObjects[1];
            Character firstCharacter = (Character)commandParameters.commandObjects[0];
            Character secondCharacter = (Character)commandParameters.commandObjects[2];

            commandParameters.gameObjects["char1"] = firstCharacter;
            commandParameters.gameObjects["char2"] = secondCharacter;
            commandParameters.gameObjects["object1"] = objectToGive;

            firstCharacter.inventory.Remove(objectToGive);
            secondCharacter.inventory.Add(objectToGive);

            msg(commandParameters, new msgParams("\n[char1.alias.uf] <replace,give,char1> the [object1.ObjectName] to [char2.alias.uf]"));
        }
        public void lockCMD(CommandParameters commandParameters)
        {
            ChangeLockState(commandParameters, true);
        }
        public void unlock(CommandParameters commandParameters)
        {
            ChangeLockState(commandParameters, false);
        }



        private void ChangeDoorState(CommandParameters commandParameters,string[] splitParams,bool closed)
        {
            if (splitParams.Length == 2)
            {
                if (currentGame.currentRoom.exits.Where(p => p.Key.hasDoor == true).Count() > 1)
                {
                    msg("\nYou really need to tell me which door to " + (closed ? "close" : "open"));
                    return;
                }
                else
                {
                    Objects.Door doorToChange = currentGame.currentRoom.exits.Where(p => p.Key.hasDoor == true).First().Key.door;
                    if (doorToChange.locked) { msg("\nThis door is locked"); return; }
                    doorToChange.closed = closed;
                    msg("\nYou " + (closed?"close":"open") +  " the door");
                    return;
                }
            }
            else
            {
                Objects.Door doorToChange = null;
                doorToChange = currentGame.findDoor(doorToChange, currentGame.currentRoom, splitParams[0]);
                if (doorToChange == null) { msg("\nI'm sorry I can't see any such door"); return; }
                if (doorToChange.locked) { msg("\nThis door is locked"); return; }
                doorToChange.closed = closed;
                msg("\nYou " + (closed ? "close" : "open") + " the door");
            }
        }
        private void ChangeLockState(CommandParameters commandParameters, bool locked)
        {
            string[] splitStringParams = commandParameters.stringParam.Split(' ');
            Objects.Door doorToChange = null;
            doorToChange = currentGame.findDoor(doorToChange, currentGame.currentRoom, splitStringParams[0]);
            if (doorToChange == null) { msg("\nI'm sorry I can't see any such door"); return; }
            if (doorToChange.locked == locked) { msg("\nThe door is already " + (locked ? "locked" : "unlocked")); return; }
            if (commandParameters.currentGame.player.inventory.Contains(doorToChange.keyObject))
            {
                doorToChange.locked = locked;
                msg("\nYou " + (locked ? "lock" : "unlock") + " the door");
            }
        }
        

    }
}
