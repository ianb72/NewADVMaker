using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewADVMaker.Games
{
    public class TestGame : GameBase
    {
        Rooms.RoomBase livingRoom = new Rooms.LivingRoom();
        Rooms.RoomBase kitchen = new Rooms.Kitchen();
        Rooms.RoomBase bedroom = new Rooms.Bedroom();

        Character mari = new Characters.mari();
        Character stacey = new Characters.stacey();

        GameObject dildo = new Objects.Dildo();
        GameObject key = new Objects.Key();

        public TestGame(MainGameForm.MessageHandler messageHandler)
            :base(messageHandler,null)
        {
            Init();
        }
        public TestGame(MainGameForm.MessageHandler messageHandler,MainGameForm mainGameForm)
            : base(messageHandler, mainGameForm)
        {
            Init();
        }
        public override void Init()
        {
            this.commandLists.Add(new Commands.TestGameCommandList(this,messageHandler));
            this.commandLists.Add(new Commands.AdultCommandList(this,messageHandler));
            this.commandLists.Add(new Commands.StandardCommandList(this,messageHandler));

            this.GameTitle = "Test Game V0.1a\n";
            AddRooms();
            AddCharacters();
        }
        private void AddRooms()
        {
            Objects.Door bedroomDoor = new Objects.Door();
            bedroomDoor.closed = true;
            bedroomDoor.locked = true;
            key.ObjectName = "bedroom key";
            key.alternateNames.Add("key");
            bedroomDoor.keyObject = key;

            livingRoom.exits.Add(new Rooms.RoomExit("north"), kitchen);
            livingRoom.exits.Add(new Rooms.RoomExit("east",true,bedroomDoor), bedroom);
            kitchen.exits.Add(new Rooms.RoomExit("south"), livingRoom);
            bedroom.exits.Add(new Rooms.RoomExit("west",true,bedroomDoor),livingRoom);

            AddToGame("livingroom", livingRoom);
            AddToGame("kitchen", kitchen);
            AddToGame("bedroom", bedroom);


            dildo.ObjectName = "black dildo";
            dildo.alternateNames.Add("dildo");
            AddToRoom(livingRoom, dildo);
            //AddToRoom(livingRoom, "key", key);

            EnterRoom(livingRoom);
        }
        private void AddCharacters()
        {
            AddToGame("mari", mari);
            AddToGame("stacey",stacey);
            AddToGame("player", player);
            AddToRoom(livingRoom, mari);
            AddToRoom(livingRoom,stacey);

            player.inventory.Add(key);
        }
    }
}
