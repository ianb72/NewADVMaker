using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker
{
    public class CommandSearchParams
    {
        public Interfaces.IcommandList commandList { get; set; }
        public int verbPosition { get; set; }
        public string commandVerb { get; set; }

        public CommandSearchParams(Interfaces.IcommandList commandList, int verbPosition,string commandVerb)
        {
            this.commandList = commandList;
            this.verbPosition = verbPosition;
            this.commandVerb = commandVerb;
        }
    }
}
