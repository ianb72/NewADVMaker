using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class StandardCommandList : Interfaces.IcommandList
    {
        public MainGameForm.MessageHandler messageHandler { get; set; }

        public StandardCommandList()
        {
        }

        public void msg(msgParams messageParams)
        {
            messageHandler.Invoke(messageParams);
        }
    }
}
