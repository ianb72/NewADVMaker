using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class Quicky : CommandBase
    {
        public Quicky(CommandParameters commandParameters)
        {
            //this.messages = new List<msgParams>();
            this.commandParameters = commandParameters;
            AddMessages();
        }
        private void AddMessages()
        {
            Character firstChar = (Character)commandParameters.firstObject;
            Character secondChar = (Character)commandParameters.secondObject;
        }
    }
}
