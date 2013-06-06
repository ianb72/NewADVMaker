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
        public Games.GameBase currentGame { get; set; }

        public AdultCommandList(Games.GameBase currentGame, MainGameForm.MessageHandler messageHandler)
        {
            this.currentGame = currentGame;
            this.messageHandler = messageHandler;
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
        public void give(CommandParameters commandParameters)
        {
            GameObject objectToGive = (GameObject)commandParameters.commandObjects[1];
            Character firstCharacter = (Character)commandParameters.commandObjects[0];
            Character secondCharacter = (Character)commandParameters.commandObjects[2];

            commandParameters.gameObjects["char1"] = firstCharacter;
            commandParameters.gameObjects["char2"] = secondCharacter;
            commandParameters.gameObjects["object1"] = objectToGive;

            //Dildo
            if (secondCharacter.HasPussy && secondCharacter.Sexuality == Sexuality.slut && objectToGive.objectType == ObjectType.Dildo)
            {
                msg("\n");

                msg("As you hand the vibrator to [char2.Firstname.uf], she grabs it, fondles it and then " +
                   "slams it deep inside her cunt moaning tremendously,she ignores you completely " +
                   "as she continues masturbating with the [object1.Description]. You wonder how in the " +
                   "world she can take that whole thing inside her! Suddenly she lets out a long " +
                   "yowling sound and shudders violently, after a few minutes, she comes down " +
                   "from her orgasm, she says  'Thank you, I needed that!' ");
            }
            //End Dildo
        }
    }
}