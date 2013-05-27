using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NewADVMaker.Commands
{
    public class AdultCommandList : Interfaces.IcommandList
    {
        public MainGameForm.MessageHandler messageHandler { get; set; }


        public AdultCommandList()
        {
        }
        public void quicky(CommandParameters commandParameters)
        {
            Character firstChar = (Character)commandParameters.firstObject;
            Character secondChar = (Character)commandParameters.secondObject;

            msg(new msgParams("Test fuck"));

        }
        public void msg(msgParams messageParams)
        {
            messageHandler.Invoke(messageParams);
        }
    }
}