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
        WordReplacement wordReplacement = new WordReplacement();
        frmRequestor requestorForm = new frmRequestor();

        Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
        Character player;

        string _promptText = "Your wish";
        string _prompt = "?";
        string lastCommandString;

        int turnCount = 0;

        bool msgBold = false;
        bool msgItalic = false;
        Color msgColour = Color.Black;
        Color defaultmsgColour = Color.Black;

        CommandHandler activeCommandHandler;

        RoomBase currentRoom = new RoomBase();
        private Games.GameBase currentGame;

        #endregion
        #region Constructors
        public MainGameForm(Games.GameBase currentGame)
        {
            this.currentGame = currentGame;
            InitializeComponent();
            Init();
        }
        #endregion
        #region Private Methods
        private void Init()
        {
            gameObjects = currentGame.gameObjects;
            player = currentGame.player;

            activeCommandHandler = new CommandHandler(ProcessCommand);
            TurnChangedEvent += MainGameForm_TurnChangedEvent;
            DisplayPrompt();
        }
        void MainGameForm_TurnChangedEvent()
        {
            turnCount++;
            lblTurnCounter.Text = Convert.ToString(turnCount);
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
                    catch (Exception)
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
                    catch (Exception)
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
            currentRoom = currentGame.currentRoom;
            msg("\n\n");
            msg("[currentroom.ObjectName.uf]",true,false,Color.Navy);
            msg("\n");
            msg(_promptText, true, false, Color.Blue);
            msg("\n");
            msg(_prompt);
            msg("", false, false, Color.Brown);

            tbOutput.Select();
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

                CommandSearchParams commandSearchParams = currentGame.findCommand(commandParts);

                if (commandSearchParams == null)
                {
                    msg("\nCommand not found\n");
                    return;
                }


                string verb = commandSearchParams.commandVerb;

                switch (commandSearchParams.verbPosition)
                {
                    case 0:
                        firstObject = player;

                        if (commandParts.Length == 2)
                        {
                            secondObject = currentGame.findObject(secondObject, commandParts[1],(Character)player);
                        }
                        if (commandParts.Length == 3)
                        {
                            secondObject = currentGame.findObject(secondObject, commandParts[1]);
                            thirdObject = currentGame.findObject(thirdObject, commandParts[2], (Character)firstObject, (Character)secondObject);
                        }
                        break;
                    case 1:
                        if (commandParts.Length == 3)
                        {
                            firstObject = currentGame.findObject(firstObject, commandParts[0]);
                            secondObject = currentGame.findObject(secondObject, commandParts[2], (Character)firstObject);
                        }
                        if (commandParts.Length == 4)
                        {
                            firstObject = currentGame.findObject(firstObject, commandParts[0]);
                            secondObject = currentGame.findObject(secondObject, commandParts[2]);
                            thirdObject = currentGame.findObject(thirdObject, commandParts[3], (Character)firstObject,(Character)secondObject);
                        }
                        break;
                }

                commandParameters = new CommandParameters(firstObject, secondObject, thirdObject);
                commandParameters.objectContainingCommand = commandSearchParams.commandList;
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
        private void DisplayMessages(List<msgParams> messages)
        {
            foreach (msgParams messageParams in messages)
            {
                msg(messageParams);
            }
        }
        #endregion
        #region Control Event Handlers
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
        #region Functions
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
            if (sourceStringArray.Length > 1)
            {
                string verb = "";

                bool isFirstwordVerb = currentGame.findObject(null, sourceStringArray[0]) == null;
                bool isSecondwordVerb = currentGame.findObject(null, sourceStringArray[1],player) == null;
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
        
        #endregion
    }
}
