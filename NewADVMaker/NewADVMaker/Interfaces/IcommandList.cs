using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NewADVMaker.Interfaces
{
    public interface IcommandList
    {
        MainGameForm.MessageHandler messageHandler { get; set; }
        void msg(Commands.CommandParameters commandParameters, msgParams messageParams);
        Games.GameBase currentGame { get; set; }
    }
}
