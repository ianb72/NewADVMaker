using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewADVMaker.Rooms;

namespace NewADVMaker.Games
{
    public class BabySitting : GameBase
    {
        Commands.BabysittingCommandList babySittingCommandList;
        Commands.StandardCommandList standardCommandList;

        RoomBase livingRoom = new RoomBase("living room");
        RoomBase girlsBedroom = new RoomBase("girls bedroom");
        RoomBase boysBedroom = new RoomBase("boys bedroom");
        RoomBase landing = new RoomBase("landing");
        RoomBase parentsBedroom = new RoomBase("parents bedroom");
        RoomBase stairs = new RoomBase("stairs");
        RoomBase bathroom = new RoomBase("bathroom");
        RoomBase hallway = new RoomBase("hallway");
        RoomBase doorstep = new RoomBase("doorstep");

        Character mother = new Characters.mother();
        Character schoolgirl = new Characters.schoolgirl();
        Character daughter = new Characters.daughter();
        Character youngboy = new Characters.youngboy();

        GameObject bedroomkey = new Objects.Key();
        GameObject lube = new GameObject("lube");

        Objects.Door bedroomdoor = new Objects.Door();
        GameObject frontdoor = new GameObject("front door");

        int motherTurnCounter = 0;

        public BabySitting(MainGameForm.MessageHandler messageHandler,MainGameForm mainGameForm)
        {
            this.messageHandler = messageHandler;
            this.mainGameForm = mainGameForm;
            Init();
        }
        public BabySitting()
        {
            Init();
        }

        public override void Init()
        {
            this.commandLists.Add(new Commands.BabysittingCommandList(this, messageHandler));
            this.commandLists.Add(new Commands.AdultCommandList(this, messageHandler));
            this.commandLists.Add(new Commands.StandardCommandList(this, messageHandler));

            babySittingCommandList = new Commands.BabysittingCommandList(this, messageHandler);
            standardCommandList = new Commands.StandardCommandList(this, messageHandler);

            this.GameTitle = "Babysitting V0.1a\n";
            AddRooms();
            AddCharacters();
            babySittingCommandList.intro(null);
        }
        public override void mainGameForm_TurnChangedEvent(TurnChangedEventArgs e)
        {
            
        }
        private void AddRooms()
        {
            bedroomdoor.keyObject = bedroomkey;
            bedroomdoor.locked = true;
            bedroomdoor.closed = true;

            girlsBedroom.exits.Add(new RoomExit("south"), landing);

            landing.exits.Add(new RoomExit("north"), girlsBedroom);
            landing.exits.Add(new RoomExit("south"), parentsBedroom);
            landing.exits.Add(new RoomExit("east"), bathroom);
            landing.exits.Add(new RoomExit("down"), stairs);
            landing.exits.Add(new RoomExit("west", true, bedroomdoor),boysBedroom);

            boysBedroom.exits.Add(new RoomExit("east", true, bedroomdoor), landing);

            stairs.exits.Add(new RoomExit("east"), livingRoom);
            stairs.exits.Add(new RoomExit("up"), landing);

            bathroom.exits.Add(new RoomExit("west"), landing);

            parentsBedroom.exits.Add(new RoomExit("north"), landing);

            livingRoom.exits.Add(new RoomExit("north"), hallway);
            livingRoom.exits.Add(new RoomExit("west"), stairs);

            hallway.exits.Add(new RoomExit("south"),livingRoom);
            hallway.contents.Add(frontdoor.ObjectName, frontdoor);

            livingRoom.contents.Add("key", bedroomkey);
            bathroom.contents.Add("lube", lube);

            AddToGame(livingRoom);
            AddToGame(girlsBedroom);
            AddToGame(boysBedroom);
            AddToGame(landing);
            AddToGame(parentsBedroom);
            AddToGame(stairs);
            AddToGame(bathroom);
            AddToGame(hallway);

            EnterRoom(livingRoom);
        }
        private void AddCharacters()
        {
            AddToGame("player", player);

            AddToGame(schoolgirl.ObjectName, schoolgirl);
            AddToGame(mother.ObjectName, mother);
            AddToGame(daughter.ObjectName, daughter);
            //AddToGame(youngboy.ObjectName, youngboy);


            AddToRoom(livingRoom, mother);
            AddToRoom(girlsBedroom, schoolgirl);
            AddToRoom(boysBedroom, youngboy);
        }
    }
}
