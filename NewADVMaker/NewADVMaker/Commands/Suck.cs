using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class Suck : CommandBase
    {
        public Suck(CommandParameters commandParameters)
            : base(commandParameters)
        {
            AddMessages();
        }
        public void AddMessages()
        {
            Character firstChar = (Character)commandParameters.firstObject;

            messageCount = 0;
            messages[0] = new List<msgParams>();
            messages[0].Add(new msgParams("Getting on [char1.possPronoun] knees, [char1.genderPronoun] slowly <replace,begin,char1> to rub your [char2.RelationshipString]'s cock in [char1.possPronoun] hands. Without a word, [char1.genderPronoun] then <replace,proceed,char1> to take [char2.possPronoun] cock into [char1.possPronoun] mouth and run [char1.possPronoun] rough tongue against the bottom of [char2.possPronoun] shaft, moving [char2.possPronoun] cock into and out of [char1.possPronoun] mouth."));
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("As [char1.genderPronoun] <replace,continue,char1> to suck [char2.objPronoun] off, [char1.genderPronoun] <replace,bring,char1> up one of [char1.possPronoun] free hands and <replace,begin,char1> to gently massage [char2.possPronoun] balls. The feeling of [char2.possPronoun] cock sliding into and out of [char1.possPronoun] mouth with [char1.possPronoun] tongue wrapping around it is unbelievable. [char1.genderPronoun] easily <replace,give,char1> the best blow job [char2.genderPronoun] <replace,have,char2> ever had the pleasure of recieving."));
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("With [char1.possPronoun] expert fellatio skills, [char2.Firstname.uf] soon <replace,feel,char2> the familiar feeling of orgasm welling up inside of [char2.objPronoun]. 'I'm gunna blow,' [char2.genderPronoun] <replace,warn,char2> [char1.objPronoun], but [char1.genderPronoun] just <replace,keep,char1> on sucking [char2.objPronoun]. Finally, when [char2.genderPronoun] can't hold on any longer, [char2.genderPronoun] <replace,release,char2> [char2.objPronoun]self inside [char1.possPronoun] mouth, filling it with cum as [char2.possPronoun] orgasm sweeps over [char2.objPronoun]."));
            if (firstChar.HasCock)
            {
                messages[0].Add(new msgParams("\n\n"));
                messages[0].Add(new msgParams("As [char2.genderPronoun] cums, [char1.alias] <replace,feel,char1> [char1.possPronoun] own orgasm building, after a few second [char1.genderPronoun] <replace,spray,char1> a big load all over the floor."));
            }
            if (!firstChar.isPlayerCharacter)
            {
                messages[0].Add(new msgParams("\n\n"));
                messages[0].Add(new msgParams("With a mouth full of [char2.Firstname]'s cum, your [char1.Relationship] walks over to you and gives a big sloppy french kiss and a mouth full of your [char2.Relationship]'s tasty sperm."));
            }
            
        }
    }
}
