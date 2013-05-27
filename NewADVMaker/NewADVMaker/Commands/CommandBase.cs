using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class CommandBase
    {
        public List<msgParams>[] messages { get; set; }
        public int messageCount { get; set; }
        public CommandParameters commandParameters { get; set; }

        public CommandBase()
        {
            messages = new List<msgParams>[1000];
            AddMessages();
        }
        public CommandBase(CommandParameters commandParameters)
        {
            messages = new List<msgParams>[1000];
            this.commandParameters = commandParameters;
            AddMessages();

        }
        private void AddMessages()
        {
        }
    }
}
