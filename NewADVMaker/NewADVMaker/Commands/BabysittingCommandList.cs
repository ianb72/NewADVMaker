using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class BabysittingCommandList : Interfaces.IcommandList
    {
        public MainGameForm.MessageHandler messageHandler { get; set; }
        public Games.GameBase currentGame { get; set; }

        private Commands.StandardCommandList standardCommandList;
        private Commands.AdultCommandList adultCommandList;
        private MessageList messages;

        private bool rubbedMothersBum = false;
        private bool rubbedMothersTits = false;
        private bool parentsHaveLeft = true;
        private bool ministeratdoor = false;

        public BabysittingCommandList(Games.GameBase currentGame, MainGameForm.MessageHandler messageHandler)
        {
            this.messages = new MessageList(@".\MessageLists\BS.msgLst");

            this.currentGame = currentGame;
            this.messageHandler = messageHandler;
            this.standardCommandList = new StandardCommandList(currentGame, messageHandler);
            this.adultCommandList = new AdultCommandList(currentGame, messageHandler);
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
        #region Commands
        public void intro(CommandParameters commandParameters)
        {
            msg(messages.messages[1]);
        }
        public void look_at(CommandParameters commandParameters)
        {
            if (commandParameters.commandObjects[1].GetType() == typeof(Characters.mother))
            {
                msg(messages.messages[208]);
                return;
            }

            msg(commandParameters.commandObjects[1].Description);
        }
        public void rub(CommandParameters commandParameters)
        {
            if (commandParameters.commandObjects[1].GetType() == typeof(Characters.mother))
            {
                if (commandParameters.commandObjects[2].GetType() == typeof(BodyParts.ass))
                {
                    msg(messages.messages[207]);
                    rubbedMothersBum = true;
                }
                if (commandParameters.commandObjects[2].GetType() == typeof(BodyParts.tits))
                {
                    if (rubbedMothersBum)
                    {
                        msg(messages.messages[206]);
                        rubbedMothersTits = true;
                    }
                    else
                    {
                        msg(messages.messages[205]);
                    }
                }
            }
        }
        public void fuck(CommandParameters commandParameters)
        {
            if (commandParameters.commandObjects[1].GetType() == typeof(Characters.mother))
            {
                if (!rubbedMothersBum || !rubbedMothersTits)
                {
                    msg(messages.messages[204]);
                }
                else
                {
                    msg(messages.messages[203]);
                    msg(messages.messages[202]);
                    currentGame.RemoveFromRoom(commandParameters.currentRoom, commandParameters.commandObjects[1]);
                    parentsHaveLeft = true;
                }
            }
        }
        public void open(CommandParameters commandParameters)
        {
            if (commandParameters.currentRoom.ObjectName == "hallway")
            {
                if (parentsHaveLeft)
                {
                    msg(messages.messages[248]);
                    ministeratdoor = true;
                }
                else
                {
                    msg(messages.messages[249]);
                }
            }
            else
            {
                standardCommandList.open(commandParameters);
            }

        }
        public void yes(CommandParameters commandParameters)
        {
            if (ministeratdoor)
            {
                msg(messages.messages[247]);
                Character daughter = (Character) currentGame.gameObjects["daughter"];
                Rooms.RoomBase livingRoom = (Rooms.RoomBase) currentGame.gameObjects["living room"];
                currentGame.AddToRoom(livingRoom,daughter);
            }
        }
        public void no(CommandParameters commandParameters)
        {
            if (ministeratdoor)
            {
                msg(messages.messages[246]);
            }
        }
        
        #endregion

    }
}
