using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using NewADVMaker.Rooms;
using NewADVMaker.Characters;
using NewADVMaker.Commands;
using NewADVMaker.Objects;
using NewADVMaker.Clothing;
using NewADVMaker.Interfaces;

namespace NewADVMaker
{
    public partial class MainGameForm : Form
    {
        #region Delegates and Events
        delegate void CommandHandler(string commandString);
        public delegate void MessageHandler(msgParams messageParams);
        delegate void TurnCountChangedEventHandler();
        event TurnCountChangedEventHandler TurnChangedEvent;

        #endregion
        #region Declarations and Variables
        CharacterGenerator charGenerator = new CharacterGenerator();
        List<Character> gameCharacters = new List<Character>();
        Dictionary<string, IcommandList> commandLists = new Dictionary<string, IcommandList>();
        Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
        Dictionary<string, GameObject> directions = new Dictionary<string, GameObject>();
        WordReplacement wordReplacement = new WordReplacement();
        frmRequestor requestorForm = new frmRequestor();

        string _promptText = "Your wish";
        string _prompt = "?";

        string lastCommandString;

        int turnCount = 0;

        bool msgBold = false;
        bool msgItalic = false;
        Color msgColour = Color.Black;
        Color defaultmsgColour = Color.Black;

        CommandHandler activeCommandHandler;
        
        //Game objects and characters
        Character Char1 = new Character();
        Character Char2 = new Character();
        Character Char3 = new Character();

        player player = new player();

        Character mother;
        Character daughter;
        Character boy;

        //Rooms
        MasterBedroom masterBedroom = new MasterBedroom();
        Kitchen kitchen = new Kitchen();
        LivingRoom livingRoom = new LivingRoom();
        RoomBase stairs = new RoomBase();
        RoomBase girlsRoom = new RoomBase();
        RoomBase boysRoom = new RoomBase();
        RoomBase bathroom = new RoomBase();
        RoomBase parentsRoom = new RoomBase();
        RoomBase landing = new RoomBase();
        RoomBase hall = new RoomBase();
        RoomBase outofHouse = new RoomBase();

        RoomBase currentRoom = new RoomBase();
        #endregion
        #region Constructors
        public MainGameForm()
        {
            InitializeComponent();
            Init();
        }
        #endregion
        #region Private Methods
        private void Init()
        {
            activeCommandHandler = new CommandHandler(ProcessCommand);
            TurnChangedEvent += MainGameForm_TurnChangedEvent;
            AddRooms();
            AddCharacters();
            AddDirections();
            AddObjects();
            AddCommandLists();
            EnterRoom(livingRoom);
            DisplayPrompt();
        }

        void MainGameForm_TurnChangedEvent()
        {
            turnCount++;
            lblTurnCounter.Text = Convert.ToString(turnCount);
            //Mother
            if (turnCount == 6)
            {
                if (mother.currentRoom != player.currentRoom)
                {
                    msg("\nThe mother calls to you saying that they are leaving, and will be back quite late");
                    msg("\nYou hear the front door shut as the mother and father leave for the party");
                }
                else if (mother.currentRoom == player.currentRoom && (bool)mother.customVariable["fucked"])
                {
                    msg("Quickly checking her hubby isn't around, Claire turns to and gives you a long hard kiss, and says 'I'll be back for more later baby'\n");
                    msg("\nYou watch Claire's sexy ass wiggle away, as her and the father leave for the party");
                }
                else if (mother.currentRoom == player.currentRoom && !(bool)mother.customVariable["fucked"])
                {
                    msg("The mother gives you a quick peck on the cheeck, tells you they will probably be back quite late");
                    msg("\nYou hear the front door shut as the mother and father leave for the party");
                }

                MoveToRoom(outofHouse, mother);
            }
        }
        private void AddCommandLists()
        {
            commandLists.Add("adult",new AdultCommandList());

            foreach (KeyValuePair<string,IcommandList> commandListKVP in commandLists)
            {
                commandListKVP.Value.messageHandler = new MessageHandler(this.msg);
            }
        }
        private void AddCharacters()
        {
            player.currentRoom = livingRoom;
            currentRoom = livingRoom;

            player.mainGameForm = this;

            gameCharacters.Add(player);

            //Mother
            mother = new Character();
            mother =  NewADVMaker.CharacterGenerator.NewCharacter(Gender.Female, FirstName.Claire, Surname.Williams);
            mother.Relationship = RelationShipAll.neighbor;
            mother.Firstname = "Claire";
            mother.ObjectName = "mother";
            mother.DisplayName = "the mother";
            mother.Description = "Your sexy neighbor Claire. \nSlightly plump just how you like, with a perfect pair of boobs, and a lovely round ass.";
            mother.alternateNames.Add("jane");
            mother.alternateNames.Add("mother");
            mother.alternateNames.Add("mom");
            mother.alternateNames.Add("mum");
            mother.customVariable.Add("bum touched",false);
            mother.customVariable.Add("tits touched",false);
            mother.customVariable.Add("kissed",false);
            mother.customVariable.Add("pussy touched",false);
            mother.customVariable.Add("fucked", false);

            AddToRoom(livingRoom, mother);
            //end mother

            //Daughter
            daughter = new Character();
            daughter = NewADVMaker.CharacterGenerator.NewCharacter(Gender.Female, FirstName.Lucy, Surname.Williams);
            daughter.Relationship = RelationShipAll.neighbor;
            daughter.Firstname = "lucy";
            daughter.ObjectName = "girl";
            daughter.DisplayName = "a young girl";
            daughter.Description = "She is bending over the desk examining her homework. <cr> Her small breasts are" +
                                   "straining against the thin fabric of her white blouse and as you look" +
                                   "closely you can just see her panties peeking out from under her short skirt.";
            daughter.alternateNames.Add("lucy");
            daughter.alternateNames.Add("girl");
            daughter.alternateNames.Add("teen");
            daughter.alternateNames.Add("daughter");
            daughter.BodyType = BodyType.young;
            daughter.BreastSize = 28;
            daughter.BreastCupSize = CupSize.A;
            daughter.Age = 16;
            daughter.Sexuality = Sexuality.virgin;
            daughter.PussyType = PussyType.virgin;
            daughter.SexualPreference = SexualPreference.straight;
            daughter.isAnalVirgin = true;
            daughter.clothing = new Outfits.girlsSchoolUniform().clothing;
            AddToRoom(girlsRoom, daughter);
            //End daughter

            //Young boy
            boy = new Character();
            boy = NewADVMaker.CharacterGenerator.NewCharacter(Gender.Male, FirstName.Mark, Surname.Williams);
            boy.Relationship = RelationShipAll.neighbor;
            boy.Firstname = "mark";
            boy.ObjectName = "boy";
            boy.DisplayName = "a young boy";
            boy.Description = "the son";
            boy.alternateNames.Add("mark");
            boy.alternateNames.Add("son");
            boy.alternateNames.Add("boy");
            boy.CockLength = 6;
            boy.CockThickness = 2;
            boy.bodyParts[0].Description = "a pretty little teen boys cock";
            boy.Age = 14;
            AddToRoom(boysRoom, boy);
            //End young boy
            gameObjects.Add("player", player);
            gameObjects.Add("mother", mother);
            gameObjects.Add("girl", daughter);
            gameObjects.Add("boy", boy);

            gameCharacters.Add(mother);
            gameCharacters.Add(daughter);
            gameCharacters.Add(boy);

            gameObjects.Add("char1", Char1);
            gameObjects.Add("char2", Char2);
            gameObjects.Add("char3", Char3);

            gameObjects.Add("object1", new GameObject());
            gameObjects.Add("object2", new GameObject());
        }
        private void AddRooms()
        {
            stairs.ObjectName = "stairs";
            stairs.Description = "the stairs";

            livingRoom.ObjectName = "living room";
            livingRoom.Description = "the living room";

            girlsRoom.ObjectName = "girls room";
            girlsRoom.Description = "a girls bedroom";

            boysRoom.ObjectName = "boys room";
            boysRoom.Description = "a boys room";

            bathroom.ObjectName = "bathroom";
            bathroom.Description = "the bathrom";

            parentsRoom.ObjectName = "parents room";
            parentsRoom.Description = "the parents bedroom";

            landing.ObjectName = "landing";
            landing.Description = "the landing";

            hall.ObjectName = "hall";
            hall.Description = "the hall";

            livingRoom.exits.Add("west", stairs);
            livingRoom.exits.Add("north", hall);

            hall.exits.Add("south", livingRoom);
            
            stairs.exits.Add("east", livingRoom);
            stairs.exits.Add("up", landing);

            landing.exits.Add("down", stairs);
            landing.exits.Add("east", bathroom);
            landing.exits.Add("west", boysRoom);
            landing.exits.Add("north", girlsRoom);
            landing.exits.Add("south", parentsRoom);

            bathroom.exits.Add("west", landing);
            boysRoom.exits.Add("east", landing);
            girlsRoom.exits.Add("south", landing);
            parentsRoom.exits.Add("north", landing);



            gameObjects.Add("currentroom", currentRoom);
        }
        private void AddDirections()
        {
            directions.Add("north",new GameObject("north"));
            directions.Add("south", new GameObject("south"));
            directions.Add("east", new GameObject("east"));
            directions.Add("west", new GameObject("west"));
            directions.Add("up", new GameObject("up"));
            directions.Add("down", new GameObject("down"));
        }
        private void AddObjects()
        {
            Strapon strapon = new Strapon();
            Bed bedroomBed = new Bed();
            Panties panties = new Panties("frilly pink panties");
            Jeans bluejeans = new Jeans();
            Container wardrobe = new NewADVMaker.Container();
            Dildo dildo = new Dildo();
        }
        private void msg(string message)
        {
            msg(message, false, false, Color.Black);
        }
        private void msg(string message,bool bold,bool italic, Color color)
        {
            int stringIndex = 0;
            StringBuilder outputString = new StringBuilder();

            while (stringIndex < message.Length)
            {
                string readChar = message.Substring(stringIndex, 1);

                if (readChar == "[")
                {
                    #region Get Properties Command
                    try
                    {
                        StringBuilder currentCommand = new StringBuilder();
                        while (readChar != "]" && stringIndex < message.Length)
                        {
                            readChar = message.Substring(stringIndex, 1);
                            currentCommand.Append(readChar);
                            stringIndex++;
                        }

                        outputString.Append(ParseStringCommand(currentCommand.ToString()));
                        stringIndex--;
                    }
                    catch (Exception ex)
                    {
                        outputString.Append("*");
                    }
                    #endregion
                }
                else if (readChar == "<")
                {
                    try
                    {
                        StringBuilder currentCommand = new StringBuilder();
                        while (readChar != ">" && stringIndex < message.Length)
                        {
                            readChar = message.Substring(stringIndex, 1);
                            currentCommand.Append(readChar);
                            stringIndex++;
                        }

                        string[] splitCurrentCommand = currentCommand.ToString().Split(',');
                        GameObject referencedObject = new GameObject();
                        string requestedObject = "";


                        switch (splitCurrentCommand.Length)
                        {
                            case 1:
                                break;
                            case 2:
                                 requestedObject = splitCurrentCommand[1].Substring(0, splitCurrentCommand[1].Length - 1);
                                 referencedObject = gameObjects[requestedObject];
                                 break;
                            case 3:
                                 requestedObject = splitCurrentCommand[2].Substring(0, splitCurrentCommand[2].Length - 1);
                                 referencedObject = gameObjects[requestedObject];
                                 break;
                        }

                        string requestedCommand = splitCurrentCommand[0].Substring(1, splitCurrentCommand[0].Length - 1).TrimEnd('>');

                        switch (requestedCommand)
                        {
                            case "replace":
                                outputString.Append(wordReplacement.ReplaceActionWord(splitCurrentCommand[1],referencedObject));
                                break;
                            case "orifice":
                                outputString.Append(wordReplacement.ReplaceOrifice(referencedObject));
                                break;
                            case "perspReplace":
                                outputString.Append(wordReplacement.PerspReplace(splitCurrentCommand[1], gameObjects["char1"], gameObjects["char2"]));
                                break;
                            case "italicson":
                                msgItalic = true;
                                return;
                            case "italicsoff":
                                msgItalic = false;
                                return;
                            case "boldon":
                                msgBold = true;
                                return;
                            case "boldoff":
                                msgBold = false;
                                return;
                            case "cr":
                                outputString.AppendLine();
                                break;
                            default:
                                break;
                        }

                        stringIndex--;
                    }
                    catch (Exception ex)
                    {
                        outputString.Append("*");
                    }
                }
                else
                {
                    outputString.Append(readChar);
                }
                stringIndex++;
            }

            StringBuilder finalOutputString = new StringBuilder();
           
            finalOutputString.Append(outputString);

            var startIndex = tbOutput.TextLength;
            tbOutput.AppendText(finalOutputString.ToString());
            //tbOutput.AppendText("\n");
            var endIndex = tbOutput.TextLength;
            tbOutput.Select(startIndex, endIndex - startIndex);
            Color newColour = Color.Black;

            if (color != defaultmsgColour)
            {
                newColour = color;
            }
            if (msgColour != defaultmsgColour)
            {
                newColour = msgColour;
            }

            tbOutput.SelectionColor = newColour;

            if (bold || msgBold) {tbOutput.SelectionFont = new Font(tbOutput.Font,FontStyle.Bold); }
            if (italic || msgItalic) { tbOutput.SelectionFont = new Font(tbOutput.Font, FontStyle.Italic); }

            tbOutput.SelectionStart = tbOutput.Text.Length;
            tbOutput.ScrollToCaret();
        }
        public void msg(msgParams messageParams)
        {
            msg(messageParams.MessageText, messageParams.Bold, messageParams.Italic, messageParams.TextColour);
        }
        private string ParseStringCommand(string commandToParse)
        {
            string toReturn = "";

            commandToParse = commandToParse.Substring(1, commandToParse.Length - 2);

            string[] splitCommand = commandToParse.Split('.');
            string objectName = splitCommand[0];
            string propertyName = splitCommand[1];
            string modifier = "";

            GameObject referencedObject = new GameObject();
            referencedObject = gameObjects[objectName];

            try
            {
                if (referencedObject == null) { throw new Exception("Error"); }
                toReturn = referencedObject.GetType().GetProperty(propertyName).GetValue(referencedObject, null).ToString();
            }
            catch
            {
                return "Error";
            }

            if (splitCommand.Length == 3)
            {
                modifier = splitCommand[2];
                switch (modifier)
                {
                    case "uf":
                        toReturn = toReturn.Substring(0, 1).ToUpper() + toReturn.Substring(1, toReturn.Length - 1);
                        break;
                    default:
                        break;
                }
            }

            return toReturn;
        }
        private string TextFormatCommand(string commandToParse)
        {
            return "";
        }
        private void DisplayPrompt()
        {
            msg("\n\n");
            msg("[currentroom.ObjectName.uf]",true,false,Color.Navy);
            msg("\n");
            msg(_promptText, true, false, Color.Blue);
            msg("\n");
            msg(_prompt);
            msg("", false, false, Color.Brown);

            tbOutput.Select();
        }
        private void ProcessCommandOld(string commandString)
        {
            if (commandString=="" || commandString==null) { commandString = lastCommandString; }
            if (commandString == "" || commandString == null) { return; }
            lastCommandString = commandString;
            string[] splitCommand = commandString.Split(' ');
            string[] commandParts = FormatString(splitCommand);
            bool firstWordisVerb = !isCharacter(splitCommand[0]);
            bool secondWordisVerb = false;
            string verb = splitCommand[0];
            GameObject firstObject = new GameObject();
            GameObject secondObject = new GameObject();
            GameObject thirdObject = new GameObject();
            CommandParameters commandParameters = new CommandParameters();

            object[] NoParameters = new object[] { };

            switch (commandParts.Length)
            {
                #region 1 parameter
                case 1:
                    ExecuteCommand(verb, commandParameters);
                    return;
                #endregion
                #region 2 parameters
                case 2:
                    bool secondWordisCharacter = isCharacter(commandParts[1]);
                    firstObject =  secondWordisCharacter ? findCharacter(commandParts[1]) : findGameObject(commandParts[1]);

                    if (firstObject == null)
                    {
                        firstObject = findObject(firstObject, commandParts[1]);
                    }
                   
                    if (firstObject == null)
                    {
                        verb = commandParts[0] + "_" + commandParts[1];
                    }

                    commandParameters = new CommandParameters(player, firstObject);
                    ExecuteCommand(verb, commandParameters);
                    return;
                #endregion
                #region 3 parameters
                case 3:
                    secondWordisVerb = !isCharacter(commandParts[1]);
                    if (firstWordisVerb && secondWordisVerb)
                    {
                        verb = commandParts[0] + "_" + commandParts[1];
                        firstObject = findCharacter(commandParts[2]);
                        if (firstObject == null) { firstObject = findObject(firstObject, commandParts[2]); }
                        commandParameters = new CommandParameters(firstObject);
                        ExecuteCommand(verb, commandParameters );
                        return;
                    }
                    else
                    {
                        verb = commandParts[1];
                        firstObject = findCharacter(commandParts[0]);
                        bool thirdWordisCharacter = isCharacter(commandParts[2]);
                        secondObject = thirdWordisCharacter ? findCharacter(commandParts[2]) : findGameObject(commandParts[2]);

                        //Is char wearing object?
                        if (secondObject == null)
                        {
                            secondObject = firstObject.getClothingObject(commandParts[2]);
                        }
                        
                        if(secondObject == null)
                        {
                            secondObject = findObject(secondObject, commandParts[2]);
                        }
                        //Unknown object
                        if (secondObject == null)
                        {
                            msg(new msgParams("I'm sorry I don't know what a "+commandParts[2]+" is!"));
                            return;
                        }
                        commandParameters = new CommandParameters(firstObject, secondObject);
                        ExecuteCommand(verb, commandParameters);
                        return;
                    }
                #endregion
                #region 4 parameters
                case 4:
                    return;
                #endregion
                #region 5 parameters

                #endregion
            }

            ExecuteCommand(verb, commandParameters);
        }
        private void ProcessCommand(string commandString)
        {
            GameObject firstObject = null;
            GameObject secondObject = null;
            GameObject thirdObject = null;
            CommandParameters commandParameters = new CommandParameters();

            try
            {
                string[] splitCommandString = commandString.Split(' ');
                if (commandString == "" || commandString == null) { commandString = lastCommandString; }
                if (commandString == "" || commandString == null) { return; }
                lastCommandString = commandString;
                string[] splitCommand = commandString.Split(' ');
                string[] commandParts = FormatString(splitCommand);
                commandParts = concanateCommand(commandParts);

                int verbIndex = GetVerbIndex(commandParts);

                if (verbIndex == -1)
                {
                    msg("\nCommand not found\n");
                    return;
                }

                string verb = commandParts[verbIndex];

                switch (verbIndex)
                {
                    case 0:
                        firstObject = player;

                        if (commandParts.Length == 2)
                        {
                            secondObject = findObject(secondObject, commandParts[1],(Character)player);
                        }
                        if (commandParts.Length == 3)
                        {
                            secondObject = findObject(secondObject, commandParts[1]);
                            thirdObject = findObject(thirdObject, commandParts[2], (Character)firstObject, (Character)secondObject);
                        }
                        break;
                    case 1:
                        if (commandParts.Length == 3)
                        {
                            firstObject = findObject(firstObject, commandParts[0]);
                            secondObject = findObject(secondObject, commandParts[2], (Character)firstObject);
                        }
                        if (commandParts.Length == 4)
                        {
                            firstObject = findObject(firstObject, commandParts[0]);
                            secondObject = findObject(secondObject, commandParts[2]);
                            thirdObject = findObject(thirdObject, commandParts[3], (Character)firstObject,(Character)secondObject);
                        }
                        break;
                }

                commandParameters = new CommandParameters(firstObject, secondObject, thirdObject);
                //commandParameters.objectContainingCommand = commandLists["adult"];
                commandParameters.objectContainingCommand = this;
                commandParameters.mainGameObject = this;
                ExecuteCommand(verb, commandParameters);
            }
            catch (Exception ex)
            {
                msg("\nCommand error\n");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        private void ExecuteCommand(string verb, CommandParameters commandParameters)
        {
            Console.WriteLine("Executing command " + verb);
            if(commandParameters.firstObject!=null) {Console.Write(commandParameters.firstObject.ObjectName + " ");}
            if (commandParameters.secondObject != null) { Console.Write(commandParameters.secondObject.ObjectName + " "); }
            if (commandParameters.thirdObject != null) { Console.Write(commandParameters.thirdObject.ObjectName + " "); }
            Console.WriteLine();

            object objectContainingMethod = commandParameters.objectContainingCommand;
            Type type = objectContainingMethod.GetType();
            MethodInfo mi = type.GetMethod(verb);

            object[] parameters = new object[] { commandParameters };

            if (mi != null)
            {
                mi.Invoke(objectContainingMethod, parameters);
                TurnChangedEvent();
            }
            else
            {
                msg("Unknown command " + verb);
            }
        }
        private void MoveToRoom(RoomBase room, Character objectToAdd)
        {
            if (objectToAdd.GetType().BaseType == typeof(Character) || objectToAdd.GetType() == typeof(Character))
            {
                RoomBase originalRoom = objectToAdd.currentRoom;
                originalRoom.peoplePresent.Remove((Character) objectToAdd);
                
                objectToAdd.currentRoom = room;
                room.peoplePresent.Add((Character)objectToAdd);
            }
        }
        private void AddToRoom(RoomBase room,string objectName, GameObject objectToAdd)
        {
            if (objectToAdd.GetType().BaseType == typeof(Character) || objectToAdd.GetType() == typeof(Character))
            {
                objectToAdd.currentRoom = room;
                room.peoplePresent.Add((Character)objectToAdd);
            }
            else
            {
                room.contents.Add(objectName,objectToAdd);
            }
        }
        private void AddToRoom(RoomBase room, GameObject objectToAdd)
        {
            AddToRoom(room, null, objectToAdd);
        }
        private void EnterRoom(RoomBase roomToEnter)
        {
            currentRoom = roomToEnter;
            player.currentRoom = currentRoom;
            gameObjects["currentroom"] = currentRoom;
            look(new CommandParameters());
        }
        private void DisplayMessages(List<msgParams> messages)
        {
            foreach (msgParams messageParams in messages)
            {
                msg(messageParams);
            }
        }
        private void GetRoomOccupants()
        {
            if (currentRoom.peoplePresent.Count != 0)
            {
                foreach (Character chr in currentRoom.peoplePresent)
                {
                    gameObjects["char1"] = chr;
                    msg("[char1.DisplayName.uf] is here", false, false, Color.DarkCyan);
                    if (chr.isNotOnFloor) { msg(" is " + chr.location.occupiedString); }
                    msg("\n");
                }
            }
            else
            {
                msg("There is no one in the room\n", true, false, Color.Black);
            }
        }
        private void GetRoomContents()
        {
            if (currentRoom.contents.Count != 0)
            {
                foreach (KeyValuePair<string, GameObject> kvp in currentRoom.contents)
                {
                    msg(kvp.Value.ObjectName + "\n");
                }
            }
        }
        private void GenerateCommandParams(string[] commandStringArray)
        {

        }
        #endregion
        #region Control Event Handlers
        private void btnRandomChar_Click(object sender, EventArgs e)
        {
            NewRandom();
            DisplayPrompt();
        }
        private void tbOutput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                string commandEntered = tbOutput.Lines[tbOutput.Lines.Length - 2];
                string commandString = tbOutput.Lines[tbOutput.Lines.Length - 2].Replace(_prompt, "");

                activeCommandHandler.Invoke(commandString);
                DisplayPrompt();
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = false;
            }
        }
        private void tbOutput_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                string lastChar = tbOutput.Text.Substring(tbOutput.TextLength - 1, 1);
                if (lastChar == _prompt)
                {
                    tbOutput.AppendText(_prompt);
                }
            }
        }
        #endregion
        #region Generic Command Methods
        public void info(CommandParameters commandParameters)
        {
            Character selectedChar = (Character)commandParameters.firstObject;
            gameObjects["char1"] = selectedChar;
            string propertyString = "Not Defined";
            string propertyNameString = "";

            msg("\n");
            msg("*************************************\n");
            msg("******* Character Info **************\n");
            msg("*************************************\n");
            foreach (PropertyInfo pi in selectedChar.GetType().GetProperties())
            {
                string propertyName = pi.Name;
                Type propertyType = pi.GetType();
                try
                {
                    propertyNameString = propertyName.PadRight(30) + ":        ";
                    
                    if (pi.Name == "clothing")
                    {

                    }
                    else if (pi.Name == "currentRoom")
                    {
                        }
                    else
                    {
                        propertyString = pi.GetValue(selectedChar, null).ToString() + "\n";
                    }
                }
                catch
                {
                }

                msg(propertyNameString);
                Console.WriteLine("{0},{1}",pi.Name,pi.GetValue(selectedChar,null));
                msg(propertyString,true,false,Color.Black);
            }
            msg("*************************************\n");
            msg("\n");
        }
        public void set(Character selectedCharacter, string propertyName, string propertyValueString)
        {
            try
            {
                selectedCharacter.SetProperty(propertyName, propertyValueString);
            }
            catch (Exception ex)
            {
                msg(string.Format("Error setting property - {0}",ex.Message), true, false, Color.Red);
            }
            
        }
        public void NewRandom(Gender gender = Gender.None)
        {
            msg("Generating new random character\n\n");
            Character newRandomCharacter = charGenerator.NewRandomCharacter(gameCharacters, player,gender);
            newRandomCharacter.SetPronouns();
            gameCharacters.Add(newRandomCharacter);
            AddToRoom(currentRoom, (Character) newRandomCharacter);
            //lookat(newRandomCharacter);
            gameObjects["char1"] = newRandomCharacter;
            msg("Generated new char - [char1.Firstname] - [char1.Relationship] - [char1.Gender] - [char1.Age]", true, false, Color.BlueViolet);
            //DisplayPrompt();
        }
        public void get_commands(CommandParameters commandParameters)
        {
            Type type = typeof(MainGameForm);
            MethodInfo[] commandMethods = type.GetMethods();
            string executingAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().ToString().Split(',')[0] + ".exe";

            foreach (MethodInfo mi in commandMethods)
            {
                if (mi.Module.Name == executingAssemblyName)
                {
                    msg(mi.Name + "\n");
                }
            }
        }
        #endregion
        #region Commands
        //public void you(CommandParameters commandParameters)
        //{
        //    look_at(new CommandParameters(player));
        //}
        public void quicky(CommandParameters commandParameters)
        {
            Character firstChar = (Character)commandParameters.firstObject;
            Character secondChar = (Character)commandParameters.secondObject;

            gameObjects["char1"] = firstChar;
            gameObjects["char2"] = secondChar;

            if (!CharacterInRoom(secondChar)) { return; }

            if (!secondChar.HasPussy) { assfuck(commandParameters); return; }

            msg("Start fucking [char2.Firstname]\n", true, true,Color.Green);
            msg("Your [char2.Relationship] lays down on [char2.possPronoun] back, legs spread\n");
            msg("[char2.Firstname] reaches down between [char2.possPronoun] legs, spreading [char2.possPronoun] [char2.PussyTypeString] pussy open\n");

        }
        public void look_at(CommandParameters commandParameters)
        {
            GameObject requestedObject;

            if (commandParameters.thirdObject != null)
            {
                requestedObject = commandParameters.thirdObject;
            }
            else
            {
                requestedObject = commandParameters.secondObject;
            }

            if (requestedObject.Description != "" && requestedObject.Description != null )
            {
                msg(requestedObject.Description);
            }
            else
            {
                gameObjects["char1"] = requestedObject;
                DisplayMessages(new LookAt(commandParameters).messages[0]);
            }
        }
        public void assfuck(CommandParameters commandParameters)
        {
            msg("Ass fuck");
        }
        public void marry(CommandParameters commandParameters)
        {
            Character secondChar = (Character) commandParameters.secondObject;

            gameObjects["char1"] = player;
            gameObjects["char2"] = secondChar;

            if (secondChar.Relationship != RelationShipAll.fiance)
            {
                msg("Maybe you should get engaged first !!", false, true, Color.Blue); return;
            }

            msg("The bells ring, the rice flies, you are now married to [char2.Firstname]", true, false, Color.Magenta);
            secondChar.Relationship = secondChar.Gender == Gender.Female ? RelationShipAll.wife : RelationShipAll.husband;
        }
        public void proposeto(CommandParameters commandParameters)
        {
            Character secondChar = (Character)commandParameters.secondObject;

            gameObjects["char1"] = player;
            gameObjects["char2"] = secondChar;

            if (secondChar.Relationship == RelationShipAll.husband || secondChar.Relationship == RelationShipAll.wife)
            {
                msg("You're already married to [char2.Article]"); return;
            }

            if (secondChar.Relationship == RelationShipAll.fiance)
            {
                msg("You're already engaged to [char2.Article]"); return;
            }

            Random acceptanceChance = new Random();
            int startIndex = 1;
            int endIndex = 10;

            switch (secondChar.Relationship)
            {
                case RelationShipAll.stranger:
                    endIndex = 50;
                    break;
                case RelationShipAll.bestfriend:
                    endIndex = 3;
                    break;
                default:
                    break;
            }

            if (acceptanceChance.Next(startIndex, endIndex) == 1)
            {
                msg("To your surprise [char2.Firstname] says yes",true,false,Color.Green);
                secondChar.Relationship = RelationShipAll.fiance;
                secondChar.EnagagementDate = DateTime.Now;
            }
            else
            {
                msg("Apologising [char2.Firstname] says [char2.genderPronoun] isn't ready for that yet",true,false,Color.Red);
            }
        }
        public void lick(CommandParameters commandParameters)
        {
            gameObjects["char1"] = commandParameters.firstObject;
            gameObjects["char2"] = commandParameters.secondObject;

            Character targetChar = (Character)commandParameters.secondObject;

            if (targetChar.isCrotchCovered)
            {
                ClothingBase coveringObject = targetChar.GetTopMostGarment(ClothingPosition.crotch);
                msg("[char2.genderPronoun.uf] would need to take off [char2.possPronoun] " + coveringObject.ObjectName + " first");
                return;
            }

            List<msgParams> messages = new List<msgParams>();
            Random rand = new Random();
            CommandBase command = new Commands.Lick(commandParameters);
            int messageIndex = rand.Next(0,command.messageCount+1);

            DisplayMessages(command.messages[1]);
        }
        public void listchars(CommandParameters commandParameters)
        {
            foreach (Character chr in gameCharacters)
            {
                string isPlayer = chr.isPlayerCharacter ? " (Player)" : "";
                msg(chr.Firstname + " " + chr.Surname +  isPlayer+ "-" + chr.Gender + " : "+ chr.currentRoom.ObjectName + "\n");
            }
        }
        public void fuck(CommandParameters commandParameters)
        {
            Character firstChar = (Character)commandParameters.firstObject;
            Character secondChar = (Character)commandParameters.secondObject;

            gameObjects["char1"] = commandParameters.firstObject;
            gameObjects["char2"] = commandParameters.secondObject;

            #region  Check can perform fuck
            if (!CharacterInRoom(secondChar)) { return; }
            //if (!firstChar.HasCock)
            //{
            //    msg("[char1.Firstname] doesn't have the equipment for that!\n\n");
            //    return;
            //}
            //if (secondChar.isCrotchCovered)
            //{
            //    msg("[char2.Firstname.uf] would need to remove some clothing before you can fuck [char2.objPronoun]\n\n");
            //    return;
            //}
            #endregion
            #region Young girl
            if (firstChar.GetType() == typeof(player) && secondChar.ObjectName == "girl" && commandParameters.thirdObject == null)
            {
                msg( "You walk across to the girl and pin her to the desk.\n"+
                     "Sliding your hand up her skirt you push her panties to one side and slip "+
                     "a finger into her tight pussy. \n"+
                     "You undo you flies and extract your erect cock. \n "+
                     "She cries out as you place your cock between her legs and slide it into "+
                     "her young cunt.\n"+
                     "It's so tight you can only get the tip of your cock in her, you push hard "+
                     "until it slides in half way.\n "+
                     "Reaching round you squeeze her small breasts through her thin blouse and bra. \n "+
                     "You can't control yourself any longer so you grab her shoulders and force "+
                     "your cock all the way up her tight cunt, she screams as you pump your rigid "+
                     "cock in and out. \n"+
                     "You put one hand over her mouth to stifle her cries and reach inside her "+
                     "blouse with the other.\n "+
                     "Pushing her bra to one side you cup one small breast in your hand and "+
                     "squeeze the nipple between your fingers. \n"+
                     "With a final thrust you spunk your load into her virgin pussy, spurt after "+
                     "spurt pumps into her little cunt filling it with cum.\n");
                msg("\n\n");
                msg("You withdraw your limp cock and walk out of the room.");
                EnterRoom(landing);
            }
            else if (firstChar.GetType() == typeof(player) && secondChar.ObjectName == "girl" && commandParameters.thirdObject.GetType() == typeof(BodyParts.ass))
            {
                msg("'Oh would you?' she squeals. \n\n" +
                     "She gets on all fours on the bed and waits.\n\n" +
                     "You climb behind her and part her buttocks with your hands.\n" +
                     "She groans as you lube up a finger and slide it up her ass.\n" +
                     "Quickly lubing your prick you place it against her young bottom and push hard. \n" +
                     "It slowly slides in and you let out a groan of pleasure. \n" +
                     "She pulls away but with one hand on her shoulder and one hand on her " +
                     "buttock you force your cock all the way up her tight asshole.\n\n" +
                     "You fuck her ass, ramming your cock in with every stroke.\n\n" +
                     "With a cry you spunk up her tight young asshole.\n\n" +
                     "You leave her collapsed in the bed with your cum leaking out of her asshole" +
                     "and walk out of the room.\n");
            }
            #endregion
            if (firstChar.GetType() == typeof(player) && secondChar.ObjectName == "mother")
            {
                mother.customVariable["fucked"] = true;
            }

        }
        public void random(CommandParameters commandParameters)
        {
            NewRandom();
        }
        public void random_male(CommandParameters commandParameters)
        {
            NewRandom(Gender.Male);
        }
        public void random_female(CommandParameters commandParameters)
        {
            NewRandom(Gender.Female);
        }
        public void random_shemale(CommandParameters commandParameters)
        {
            NewRandom(Gender.Shemale);
        }
        public void random_herm(CommandParameters commandParameters)
        {
            NewRandom(Gender.Herm);
        }
        public void rub(CommandParameters commandParameters)
        {
            gameObjects["char1"] = (Character)commandParameters.firstObject;
            gameObjects["char2"] = (Character)commandParameters.secondObject;

            

        }
        public void look(CommandParameters commandParameters)
        {
            msg("\n");
            msg("You are in [currentroom.Description]\n",false,false,Color.Green);
            
            exits(new CommandParameters());

            GetRoomOccupants();
            GetRoomContents();
        }
        public void go(CommandParameters commandParameters)
        {
            string direction = commandParameters.secondObject.Description;
            RoomBase destinationRoom;
            bool isValidExit = currentRoom.exits.TryGetValue(direction, out destinationRoom);

            if (isValidExit)
            {
                EnterRoom(destinationRoom);
            }
            else
            {
                msg("You can't go that way !\n\n");
            }
        }
        public void exits(CommandParameters commandParameters)
        {
            foreach(KeyValuePair<string,RoomBase> kvp in currentRoom.exits)
            {
                msg("\nYou can go " + kvp.Key + " ");
            }
            msg("\n\n");
        }
        public void call(CommandParameters commandParameters)
        {
            Character requestedChar = (Character)commandParameters.secondObject;
            gameObjects["char2"] = requestedChar;

            MoveToRoom(currentRoom,(Character) requestedChar);

            msg("\n[char2.Firstname.uf] is now in the room with you");
        }
        public void wear(CommandParameters commandParameters)
        {
            GameObject requestedObject = new GameObject();

            try
            {
                requestedObject = commandParameters.secondObject;
                Character requestedChar = (Character)commandParameters.firstObject;
                string objectName = requestedObject.ObjectName;
                currentRoom.contents.Remove(objectName);
                requestedChar.Wear(objectName, requestedObject);
                if(isInventoryObject(requestedChar,objectName))
                {
                    requestedChar.inventory.Remove(requestedObject);
                }
                gameObjects["char1"] = requestedChar;
                msg(new msgParams("\n[char1.Firstname.uf] is now wearing the " + requestedObject.Description + "\n"));
            }
            catch (Exception ex)
            {
                msg(new msgParams("\n[char1.Firstname.uf] can't put on the " + requestedObject.Description + "\n"));
            }
        }
        public void remove(CommandParameters commandParameters)
        {
            GameObject requestedObject = commandParameters.secondObject;
            Character requestedChar = (Character)commandParameters.firstObject;
            string objectName = requestedObject.ObjectName;
            requestedChar.Remove(objectName);
            
            if (!objectName.Contains("'s"))
            {
                objectName = requestedChar.Firstname + "'s " + objectName;
                requestedObject.ObjectName = objectName;
            }
            
            currentRoom.contents.Add(objectName,requestedObject);
            gameObjects["char1"] = requestedChar;
            msg(new msgParams("\n[char1.Firstname.uf] takes off the " + requestedObject.Description + "\n"));
        }
        public void get_on(CommandParameters commandParameters)
        {
            Character requestedChar = (Character) commandParameters.firstObject;
            GameObject requestedObject = commandParameters.secondObject;
            requestedChar.isNotOnFloor = true;
            requestedChar.location = requestedObject;
        }
        public void suck(CommandParameters commandParameters)
        {
            gameObjects["char1"] = (Character)commandParameters.firstObject;
            gameObjects["char2"] = (Character)commandParameters.secondObject;

            List<msgParams> messages = new List<msgParams>();
            Random rand = new Random();
            CommandBase command = new Commands.Suck(commandParameters);
            int messageIndex = rand.Next(0, command.messageCount + 1);

            DisplayMessages(command.messages[messageIndex]);
        }
        public void use(CommandParameters commandParameters)
        {
        }
        public void kiss(CommandParameters commandParameters)
        {
            Character firstChar = (Character) commandParameters.firstObject;
            Character secondChar = (Character)commandParameters.secondObject;
            GameObject thirdObject = commandParameters.thirdObject;
        }
        public void examine(CommandParameters commandParameters)
        {

        }
        public void clothing(CommandParameters commandParameters)
        {
            Character selectedChar = (Character) commandParameters.secondObject;
            gameObjects["char1"] = selectedChar;

            msg("[char1.Firstname.uf] is wearing\n",true,false,Color.Black);
            msg(selectedChar.GetCurrentVisibleClothingString());

        }
        public void open(CommandParameters commandParameters)
        {
            gameObjects["object1"] = commandParameters.secondObject;

            if (commandParameters.secondObject.GetType() != typeof(Container))
            {
                msg("\nYou can't open [object1.ObjectName]\n");
                return;
            }

            msg("\nYou open the [object1.ObjectName.uf]\n");
            msg("\nIt contains\n");

            Container containerToOpen = (Container)commandParameters.secondObject;
            foreach (GameObject contentItem in containerToOpen.contents)
            {
                msg(contentItem.ObjectName + "\n");
                currentRoom.contents.Add(contentItem.ObjectName, contentItem);
            }
        }
        public void close(CommandParameters commandParameters)
        {
            gameObjects["object1"] = commandParameters.secondObject;

            if (commandParameters.secondObject.GetType() != typeof(Container))
            {
                msg("\nYou can't close [object1.ObjectName]\n");
                return;
            }

            Container containerToClose = (Container) commandParameters.secondObject;
            Dictionary<string, GameObject> currentRoomContents = new Dictionary<string, GameObject>(currentRoom.contents);
            
            foreach (KeyValuePair<string, GameObject> kvp in currentRoomContents)
            {
                if (kvp.Value.parentContainer == containerToClose)
                {
                    containerToClose.contents.Add(kvp.Value);
                    currentRoom.contents.Remove(kvp.Value.ObjectName);
                }
            }
        }
        public void i(CommandParameters commandParameters)
        {
            inventory(commandParameters);
        }
        public void inventory(CommandParameters commandParameters)
        {
            Character requestedChar = (Character)commandParameters.secondObject != null ? (Character)commandParameters.secondObject : player;

            gameObjects["char1"] = requestedChar;

            msg("\n[char1.alias.uf] <replace,are,char1> carrying - ");
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
            Character firstCharacter = (Character)commandParameters.firstObject;

            firstCharacter.inventory.Add(commandParameters.secondObject);
            currentRoom.contents.Remove(commandParameters.secondObject.ObjectName);

            msg("\nYou take the " + commandParameters.secondObject.ObjectName + "\n");
        }
        public void put_away(CommandParameters commandParameters)
        {
            currentRoom.contents.Remove(commandParameters.secondObject.ObjectName);
            commandParameters.secondObject.parentContainer.contents.Add(commandParameters.secondObject);
        }
        public void drop(CommandParameters commandParameters)
        {
            GameObject objectToDrop = commandParameters.secondObject;
            Character firstCharacter = (Character)commandParameters.firstObject;
            firstCharacter.inventory.Remove(objectToDrop);
            currentRoom.contents.Add(objectToDrop.ObjectName, objectToDrop);
        }
        public void give(CommandParameters commandParameters)
        {
            GameObject objectToGive = (GameObject)commandParameters.thirdObject;
            Character firstCharacter = (Character)commandParameters.firstObject;
            Character secondCharacter = (Character)commandParameters.secondObject;

            gameObjects["char1"] = firstCharacter;
            gameObjects["char2"] = secondCharacter;
            gameObjects["object1"] = objectToGive;

            firstCharacter.inventory.Remove(objectToGive);
            secondCharacter.inventory.Add(objectToGive);

            msg("[char1.genderPronoun.uf] <replace,give,char1> the [object1.ObjectName] to [char2.Firstname]");

            #region Dildo to slut
            if (secondCharacter.HasPussy && secondCharacter.Sexuality == Sexuality.slut && objectToGive.objectType==ObjectType.Dildo)
            {
                msg("\n");

                msg("As you hand the vibrator to [char2.Firstname.uf], she grabs it, fondles it and then " +
                   "slams it deep inside her cunt moaning tremendously,she ignores you completely " +
                   "as she continues masturbating with the [object1.Description]. You wonder how in the " +
                   "world she can take that whole thing inside her! Suddenly she lets out a long " +
                   "yowling sound and shudders violently, after a few minutes, she comes down " +
                   "from her orgasm, she says  'Thank you, I needed that!' ");
            }
            #endregion

        }
        public void threesome(CommandParameters commandParamters)
        {
            msg(commandParamters.firstObject.ObjectName + "\n");
            msg(commandParamters.secondObject.ObjectName + "\n");
            msg(commandParamters.thirdObject.ObjectName + "\n");
        }
        public void squeeze(CommandParameters commandParameters)
        {
            //Mother
            if (player.currentRoom == livingRoom && 
                commandParameters.secondObject == mother &&
                commandParameters.thirdObject.GetType() == typeof(BodyParts.ass))
            {
                msg("You walk over and stand behind her. Gently you squeeze her buttocks with both " +
                    "hands. You can feel the outline of her panties through the thin material of " +
                    "her dress. 'OHHH' she exclaims, 'you've got nice hands'.");
                mother.customVariable["bum touched"] = true;
            }

            if (player.currentRoom == livingRoom && commandParameters.secondObject == mother &&
                commandParameters.thirdObject.GetType() == typeof(BodyParts.tits) &&
                (bool)mother.customVariable["bum touched"] == true)
            {
                msg("You reach round and cup her breasts in your hands. You bounce them up and down " +
                    "and squeeze them gently feeling her nipples harden underneath yourfingers.  " +
                    "She reaches behind her and feels your crotch. 'You ARE horny' she says" +
                    "massaging your cock through your trousers.");
            }
            //End mother

        }
        #endregion
        #region Command Aliases
        #endregion
        #region Other Sex Commands
        public void TakeVirginity()
        {

        }
        #endregion
        #region Functions
        private GameObject findObject(GameObject gameObject, string objectName, Character Character1,Character Character2)
        {
            //Is object character
            if (gameObject == null)
            {
                gameObject = findCharacter(objectName);
            }
            //Is possesive form of character
            if (gameObject == null)
            {
                char[] possesiveChars = new char[] { (char)32, (char)115 };
                gameObject = findCharacter(objectName.TrimStart(possesiveChars));
            }
            //Is character body part
            if (gameObject == null && Character1 != null)
            {
                gameObject = findCharBodypart(Character1, objectName);
            }
            if (gameObject == null && Character2 != null)
            {
                gameObject = findCharBodypart(Character2, objectName);
            }
            //Is char wearing object?
            if (gameObject == null && Character1 != null)
            {
                gameObject = Character1.getClothingObject(objectName);
            }
            //Second char reference to themsevles
            if(gameObject == null)
            {
                gameObject = isSelfReference(Character1, objectName);
            }
            //Is object in room
            if (gameObject == null)
            {
                gameObject = findInRoom(objectName);
            }
            //Is direction command?
            if (gameObject == null)
            {
                gameObject = isDirectionalCommand(objectName);
            }
            //Is player alias
            if (gameObject == null)
            {
                gameObject = isPlayerAlias(objectName) ? player : null;
            }
            //Is inventory object
            if (gameObject == null && Character1 != null)
            {
                gameObject = findInventoryObject(Character1, objectName);
            }

            return gameObject;
        }
        public GameObject findObject(GameObject gameObject, string objectName)
        {
            return findObject(gameObject, objectName, null,null);
        }
        public GameObject findObject(GameObject gameObject, string objectName, Character Character1)
        {
            return findObject(gameObject, objectName, Character1,null);
        }
        private Character findCharacter(string characterName)
        {
            int charCount = 0;
            int charIndex =0;
            int totalCount = 0;

            if (characterName.Contains("'s"))
            {
                characterName= characterName.TrimEnd(new char[] { (char)39, (char)115 });
            }

            foreach (Character chr in gameCharacters)
            {
                if (chr.CharacterName.ToLower() == characterName.ToLower())
                {
                    charCount++;
                    charIndex = totalCount;
                }
                else
                {
                    foreach (string altName in chr.alternateNames)
                    {
                        if(altName.ToLower() == characterName.ToLower())
                        {
                            charCount++;
                            charIndex = totalCount;
                        }
                    }
                }

                totalCount++;
            }



            if (charCount == 1)
            {
                return gameCharacters[charIndex];
            }
            else if (charCount != 0)
            {
                return requestWhichCharacter(characterName);
            }
            else
            {
                return null;
            }
        }
        private Character requestWhichCharacter(string characterName)
        {
            requestorForm.ClearList();

            foreach (Character chr in gameCharacters)
            {
                if (chr.CharacterName.ToLower() == characterName.ToLower())
                {
                    requestorForm.AddToList(chr.CharacterName + " " + chr.Surname + " " + chr.Age);
                }
            }

            DialogResult dr = requestorForm.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.Cancel)
            {
                return null;
            }
            else
            {
                string[] splitselectedString = requestorForm.selectedString.Split(' ');
                string selectedName = splitselectedString[0] + " " + splitselectedString[1];
                return findCharacterFullName(selectedName);
            }
        }
        private Character findCharacterFullName(string fullName)
        {

            foreach (Character chr in gameCharacters)
            {
                if (chr.FullNameString.ToLower() ==  fullName.ToLower())
                {
                    return chr;
                }
            }

            return null;
        }
        private GameObject findGameObject(string objectName)
        {
            foreach (KeyValuePair<string,GameObject> kvp in currentRoom.contents)
            {
                if (kvp.Key == objectName) { return kvp.Value; }
                foreach (string altName in kvp.Value.alternateNames)
                {
                    if (altName == objectName) { return kvp.Value; }
                }
            }

            return null;
        }
        private GameObject findInventoryObject(Character parentCharacter,string objectName)
        {
            foreach (GameObject invObject in parentCharacter.inventory)
            {
                if (invObject.ObjectName == objectName) { return invObject; }
            }

            return null;
        }
        private GameObject findInRoom(string objectName)
        {
            foreach (KeyValuePair<string, GameObject> kvp in currentRoom.contents)
            {
                if (kvp.Key == objectName)
                {
                    return kvp.Value;
                }
            }

            return null;
        }
        private GameObject findCharBodypart(Character parentCharacter, string objectName)
        {
            foreach (GameObject bodyPart in parentCharacter.bodyParts)
            {
                if (bodyPart.ObjectName == objectName) { return bodyPart; }
                foreach (string altName in bodyPart.alternateNames)
                {
                    if (altName == objectName) { return bodyPart; }
                }
            }

            return null;
        }
        private GameObject isSelfReference(GameObject firstChar, string objectName)
        {
            if (objectName.Contains("self")) { return firstChar; }
            return null;
        }
        private bool isCharacter(string wordToCheck)
        {
            return findCharacter(wordToCheck) != null;
        }
        private bool isGameObject(string wordToCheck)
        {
            return findGameObject(wordToCheck) != null;
        }
        private bool isPlayerAlias(string wordToCheck)
        {
            List<string> playerAlias = new List<string>();
            playerAlias.Add("you");
            playerAlias.Add("me");
            return playerAlias.Contains(wordToCheck);
        }
        private bool isInventoryObject(Character parentCharacter, string objectName)
        {
            return findInventoryObject(parentCharacter, objectName) != null;
        }
        private GameObject isDirectionalCommand(string wordToCheck)
        {
            GameObject tempObject;
            directions.TryGetValue(wordToCheck,out tempObject);
            return tempObject;
        }
        private string[] FormatString(string[] sourceStringArray)
        {
            int stringIndex = 0;
            ArrayList newArray = new ArrayList();

            #region Remove Quotes
            while (stringIndex < sourceStringArray.Length)
            {
                string stringElement = sourceStringArray[stringIndex];
                StringBuilder stringToAdd = new StringBuilder();

                if (stringElement.Contains('"'))
                {
                    int quotedStringStart = stringIndex;
                    stringToAdd.Append(sourceStringArray[quotedStringStart].TrimStart('"'));

                    do
                    {
                        stringElement = sourceStringArray[stringIndex+1];
                        stringToAdd.Append(' ');
                        stringToAdd.Append(stringElement.TrimEnd('"'));
                        stringIndex++;
                    }
                    while (!stringElement.Contains('"'));
                    
                    stringIndex++;
                    newArray.Add(stringToAdd.ToString());
                }
                else if(ExtensionMethods.Extensions.isAdverb(stringElement))
                {
                    stringIndex++;
                }
                else
                {
                    stringToAdd.Append(stringElement);
                    stringIndex++;
                    newArray.Add(stringToAdd.ToString());
                }
            #endregion
            }

            string[] toReturn = (String[]) newArray.ToArray(typeof(string));

            return toReturn;
        }
        private string[] concanateCommand(string[] sourceStringArray)
        {
            GameObject nullObject = null;

            if (sourceStringArray.Length > 1)
            {
                string verb = "";

                bool isFirstwordVerb = findObject(null, sourceStringArray[0]) == null;
                bool isSecondwordVerb = findObject(null, sourceStringArray[1],player) == null;
                if (isFirstwordVerb && isSecondwordVerb)
                {
                    verb = sourceStringArray[0] + "_" + sourceStringArray[1];
                    string[] newStringArray = new string[sourceStringArray.Length - 1];
                    newStringArray[0] = verb;
                    Array.Copy(sourceStringArray, 2, newStringArray, 1, sourceStringArray.Length - 2);
                    sourceStringArray = newStringArray;
                }
            }

            return sourceStringArray;
        }
        private int GetVerbIndex(string[] sourceStringArray)
        {
            
            Type type = typeof(MainGameForm);
            MethodInfo[] commandMethods = type.GetMethods();
            string executingAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().ToString().Split(',')[0] + ".exe";

            foreach (MethodInfo mi in commandMethods)
            {
                if (mi.Module.Name == executingAssemblyName)
                {
                    for (var sourceIndex = 0; sourceIndex < sourceStringArray.Length; sourceIndex++)
                    {
                        if (sourceStringArray[sourceIndex] == mi.Name)
                        {
                            return sourceIndex;
                        }
                    }
                }
            }

            return -1;
        }
        private bool CharacterInRoom(Character character)
        {
            bool toReturn = currentRoom.peoplePresent.Contains(character);
            
            if (!toReturn) { msg(new msgParams("\n[char2.Firstname.uf] is not in the " + currentRoom.ObjectName + "\n\n")); }

            return toReturn;
        }
        #endregion
    }
}
