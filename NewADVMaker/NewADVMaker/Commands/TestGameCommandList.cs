using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class TestGameCommandList : Interfaces.IcommandList
    {
        public MainGameForm.MessageHandler messageHandler { get; set; }
        public Games.GameBase currentGame { get; set; }

        private Commands.StandardCommandList standardCommandList;
        private Commands.AdultCommandList adultCommandList;

        private MessageList messages;

        public TestGameCommandList(Games.GameBase currentGame, MainGameForm.MessageHandler messageHandler)
        {
            this.messages = new MessageList(@".\messagelists\testgame.msglst");
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

        public void give(CommandParameters commandParameters)
        {
            standardCommandList.give(commandParameters);
            adultCommandList.give(commandParameters);
        }
        public void fuck(CommandParameters commandParameters)
        {
            
        }
        public void kiss(CommandParameters commandParameters)
        {
            if (commandParameters.commandObjects[0] is Characters.stacey)
            {
                msg(messages.messages[100]);
            }
        }
        public void make_love(CommandParameters commandParameters)
        {
            currentGame.customVariable.Add("makinglove",1);
            currentGame.gameObjects["char2"] = commandParameters.commandObjects[1];
            msg(messages.messages[101]);
        }
        public void pussy(CommandParameters commandParameters)
        {
            if (currentGame.customVariable["makinglove"] == 1)
            {
                msg(messages.messages[102]);
                currentGame.customVariable["makinglove"] = 0;
            }
        }
        public void breasts(CommandParameters commandParameters)
        {
            if (currentGame.customVariable["makinglove"] == 1)
            {
                msg(messages.messages[103]);
                currentGame.customVariable["makinglove"] = 0;
            }
        }
    }
}
