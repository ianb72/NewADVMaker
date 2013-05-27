using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class Lick : CommandBase
    {
        public Lick(CommandParameters commandParameters)
            : base(commandParameters)
        {
            AddMessages();
        }
        public void AddMessages()
        {
            Character lastFuck = new Character();
            Character secondChar = (Character) commandParameters.secondObject;
            
            string lastFuckString = "";

            if (secondChar.isCreampied) 
            { 
                lastFuck = secondChar.lastFuck;
                lastFuckString = lastFuck.isPlayerCharacter ? "your" : lastFuck.Firstname + "'s";
            }

            messageCount = 0;
            messages[0] = new List<msgParams>();
            messages[0].Add(new msgParams("[char1.genderPronoun] <replace,kneel,char1> in front of her and <replace,pull,char1> her panties down her legs. "));
            messages[0].Add(new msgParams("She steps out of them without resistance. [char1.genderPronoun] <replace,place,char1> light kisses up her thigh, inching [char1.possPronoun] way toward her throbbing [char2.PussyTypeString] cunt."));
            messages[0].Add(new msgParams("When [char1.possPronoun] lips touch her she leans back and moans with delight. She spread her legs further to allow [char1.objPronoun] better access to her."));

            messageCount++;
            messages[1] = new List<msgParams>();
            messages[1].Add(new msgParams("Getting down on [char1.possPronoun] knees, [char1.genderPronoun] gently begin to lap away at the abundant wetness being secreted by [char2.Firstname.uf]'s horny pussy, running [char1.possPronoun] tongue up and down her pink slit."));
            if (secondChar.isCreampied)
            {
                messages[1].Add(new msgParams("\n\n"));
                messages[1].Add(new msgParams("[char1.genderPronoun.uf] <replace,get,char1> a pleasant surprise when a bog glob of " + lastFuckString +" cum lands in [char1.possPronoun] mouth"));
                messages[1].Add(new msgParams("\n\n"));
                secondChar.isCreampied = false;
            }
            messages[1].Add(new msgParams("With [char2.Firstname.uf] leaning up against a wall for support, [char1.genderPronoun] continue to assault her cunt with [char1.possPronoun] tongue, burying it deep inside her and licking at he slick insides of her young sex."));
            messages[1].Add(new msgParams("With [char1.possPronoun] head between her thighs, [char1.genderPronoun] can feel [char2.Firstname.uf]'s body tighten as she rapidly approaches an orgasm. Finally, in a great release, she cums hard with [char1.possPronoun] face still buried in her pussy."));
        }
    }
}
