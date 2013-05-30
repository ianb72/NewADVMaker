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
        public void msg(CommandParameters commandParameters, msgParams messageParams)
        {
            messageParams.commandParameters = commandParameters;
            messageHandler.Invoke(messageParams);
        }
        public void quicky(CommandParameters commandParameters)
        {
            Console.WriteLine("Quicky");
        }
        public void assfuck(CommandParameters commandParameters)
        {
        }
        public void lick(CommandParameters commandParameters)
        {
         
        }
       
        public void fuck(CommandParameters commandParameters)
        {

        }
        public void suck(CommandParameters commandParameters)
        {
          
        }
        public void threesome(CommandParameters commandParamters)
        {
           
        }
        public void squeeze(CommandParameters commandParameters)
        {
          
        }
    }
}