using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;

using NewADVMaker.Rooms;
using NewADVMaker.Characters;
using NewADVMaker.Commands;
using NewADVMaker.Objects;
using NewADVMaker.Clothing;
using NewADVMaker.Interfaces;

namespace NewADVMaker.Games
{
    public class GameBase
    {
        public string GameTitle { get; set; }
        public List<Character> gameCharacters = new List<Character>();
        public List<IcommandList> commandLists = new List<IcommandList>();
        public Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
        public Character Char1 = new Character();
        public Character Char2 = new Character();
        public Character Char3 = new Character();
        public Character player = new Character(true);
        public RoomBase currentRoom;
        public CharacterGenerator charGenerator = new CharacterGenerator();
        public MainGameForm mainGameForm { get; set; }
        public int TurnCount { get; set; }
        public Dictionary<string, int> customVariable = new Dictionary<string, int>();

        public MainGameForm.MessageHandler messageHandler;

        public GameBase()
        {
        }
        
        public GameBase(MainGameForm.MessageHandler messageHandler,MainGameForm mainGameForm)
        {
            this.player = new Characters.player();
            this.messageHandler = messageHandler;
            this.mainGameForm = mainGameForm;
            this.mainGameForm.TurnChangedEvent += mainGameForm_TurnChangedEvent;
        }

        public virtual void mainGameForm_TurnChangedEvent(TurnChangedEventArgs e)
        {
        }
        public virtual void Init()
        {
        }
        public void MoveToRoom(RoomBase room, Character objectToAdd)
        {
            if (objectToAdd.GetType().BaseType == typeof(Character) || objectToAdd.GetType() == typeof(Character))
            {
                RoomBase originalRoom = objectToAdd.currentRoom;
                originalRoom.peoplePresent.Remove((Character)objectToAdd);

                objectToAdd.currentRoom = room;
                room.peoplePresent.Add((Character)objectToAdd);
            }
        }
        public void RemoveFromRoom(RoomBase room, GameObject objectToRemove)
        {
            room.peoplePresent.Remove((Character)objectToRemove);
        }
        public void AddToRoom(RoomBase room, GameObject objectToAdd)
        {
            if (objectToAdd.GetType().BaseType == typeof(Character) || objectToAdd.GetType() == typeof(Character))
            {
                objectToAdd.currentRoom = room;
                room.peoplePresent.Add((Character)objectToAdd);
            }
            else
            {
                room.contents.Add(objectToAdd.ObjectName, objectToAdd);
            }
        }
        public void AddToRoom(string roomName, string objectName)
        {
            AddToRoom((RoomBase) gameObjects[roomName], gameObjects[objectName]);
        }
        public void EnterRoom(RoomBase roomToEnter)
        {
            player.currentRoom = roomToEnter;
            SetCurrentRoom(roomToEnter);
        }
        public void GetRoomOccupants()
        {
            
        }
        public void GetRoomContents()
        {
            
        }
        public void NewRandom(Gender gender = Gender.None)
        {
            Character newRandomCharacter = charGenerator.NewRandomCharacter(gameCharacters, player, gender);
            newRandomCharacter.SetPronouns();
            gameCharacters.Add(newRandomCharacter);
            AddToRoom(currentRoom, (Character)newRandomCharacter);
            gameObjects["char1"] = newRandomCharacter;
        }
        public void AddToGame(string label, GameObject objectToAdd)
        {
            gameObjects.Add(label, (GameObject)objectToAdd);
            if (objectToAdd.GetType().BaseType == typeof(Character))
            {
                gameCharacters.Add((Character)objectToAdd);
            }

        }
        public void AddToGame(GameObject objectToAdd)
        {
            AddToGame(objectToAdd.ObjectName, objectToAdd);
        }
        public void SetCurrentRoom(RoomBase room)
        {
            gameObjects["currentRoom"] = room;
            currentRoom = room;
        }
        public GameObject findObject(GameObject gameObject, string objectName, Character Character1, GameObject object2)
        {
            Character Character2 = null;

            if (object2!=null &&  object2.GetType().BaseType == typeof(Character))
            {
                Character2 = (Character)object2;
            }
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
            if (gameObject == null)
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
            //Is Room
            if (gameObject == null)
            {

            }
            return gameObject;
        }
        public GameObject findObject(GameObject gameObject, string objectName)
        {
            return findObject(gameObject, objectName, null, null);
        }
        public GameObject findObject(GameObject gameObject, string objectName, Character Character1)
        {
            return findObject(gameObject, objectName, Character1, null);
        }
        public Character findCharacter(string characterName)
        {
            int charCount = 0;
            int charIndex = 0;
            int totalCount = 0;

            if (characterName.Contains("'s"))
            {
                characterName = characterName.TrimEnd(new char[] { (char)39, (char)115 });
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
                        if (altName.ToLower() == characterName.ToLower())
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
                return null;
            }
            else
            {
                return null;
            }
        }
        public Character findCharacterFullName(string fullName)
        {

            foreach (Character chr in gameCharacters)
            {
                if (chr.FullNameString.ToLower() == fullName.ToLower())
                {
                    return chr;
                }
            }

            return null;
        }
        public GameObject findGameObject(string objectName)
        {
            foreach (KeyValuePair<string, GameObject> kvp in currentRoom.contents)
            {
                if (kvp.Key == objectName) { return kvp.Value; }
                foreach (string altName in kvp.Value.alternateNames)
                {
                    if (altName == objectName) { return kvp.Value; }
                }
            }

            return null;
        }
        public GameObject findInventoryObject(Character parentCharacter, string objectName)
        {
            foreach (GameObject invObject in parentCharacter.inventory)
            {
                if (invObject.ObjectName == objectName) { return invObject; }
            }

            return null;
        }
        public GameObject findInRoom(string objectName)
        {
            foreach (KeyValuePair<string, GameObject> kvp in currentRoom.contents)
            {
                if(kvp.Value.alternateNames.Any(p => p.Contains(objectName)))
                {
                    return (kvp.Value);
                }
            }

            return null;
        }
        public GameObject findCharBodypart(Character parentCharacter, string objectName)
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
        public GameObject isSelfReference(GameObject firstChar, string objectName)
        {
            if (objectName.Contains("self")) { return firstChar; }
            return null;
        }
        public bool isCharacter(string wordToCheck)
        {
            return findCharacter(wordToCheck) != null;
        }
        public bool isGameObject(string wordToCheck)
        {
            return findGameObject(wordToCheck) != null;
        }
        public bool isPlayerAlias(string wordToCheck)
        {
            List<string> playerAlias = new List<string>();
            playerAlias.Add("you");
            playerAlias.Add("me");
            return playerAlias.Contains(wordToCheck);
        }
        public bool isInventoryObject(Character parentCharacter, string objectName)
        {
            return findInventoryObject(parentCharacter, objectName) != null;
        }
        public bool CharacterInRoom(Character character)
        {
            bool toReturn = currentRoom.peoplePresent.Contains(character);
            return toReturn;
        }
        public GameObject isDirectionalCommand(string wordToCheck)
        {
            GameObject tempObject = null;
            
            if (Characteristics.exits.Contains(wordToCheck))
            {
                tempObject = new GameObject();
                tempObject.ObjectName = wordToCheck;
                
            }
            return tempObject;
        }
        public CommandSearchParams findCommand(string[] sourceStringArray)
        {
            foreach (IcommandList commandList in commandLists)
            {
                var stringIndex = 0;
                foreach (string str in sourceStringArray)
                {
                    Type type = commandList.GetType();
                    MethodInfo[] methods = type.GetMethods();

                    //Replace restricted system methods
                    string searchString = "";
                    if (str == "lock")
                    {
                        searchString = "lockCMD";
                    }
                    else
                    {
                        searchString = str;
                    }

                    if (methods.Any(p => p.Name == searchString))
                    {
                        Console.WriteLine("Command {0} found in {1}", searchString, commandList);
                        return new CommandSearchParams(commandList, stringIndex,searchString);
                    }
                    stringIndex++;
                 }
            }

            return null;
        }
        public void msg(CommandParameters commandParameters, msgParams msgParameters)
        {
            msgParameters.commandParameters = commandParameters;
            messageHandler.Invoke(msgParameters);
        }
        public void msg(msgParams msgParameters)
        {
            messageHandler.Invoke(msgParameters);
        }
        public Door findDoorFromDirection(Door door, RoomBase room, string exitString)
        {
            foreach(KeyValuePair<RoomExit,RoomBase> kvp in room.exits)
            {
                if (kvp.Key.direction == exitString)
                {
                    return kvp.Key.door;
                }
            }

            return door;
        }
        public Door findDoorFromDestination(Door door, RoomBase room, string exitString)
        {
            foreach (KeyValuePair<RoomExit, RoomBase> kvp in room.exits)
            {
                if (kvp.Value.ObjectName == exitString) { return kvp.Key.door; }

            }

            return door;
        }
        public Door findDoor(Door door, RoomBase room, string exitString)
        {
            if (door == null)
            {
                door = findDoorFromDirection(door, room, exitString);
            }

            if (door == null)
            {
                door = findDoorFromDestination(door, room, exitString);
            }

            return door;
        }
    }
}
