using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Games
{
    public class TestGame : GameBase
    {
        public TestGame()
        {
            Init();
        }
        public override void Init()
        {
            this.commandLists.Add(new Commands.AdultCommandList());
        }
    }
}
