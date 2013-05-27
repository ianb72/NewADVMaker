using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class Fuck : CommandBase
    {
        public Fuck(CommandParameters commandParameters)
        {
            //this.messages = new List<msgParams>();
            this.commandParameters = commandParameters;
            AddMessages();
        }
        private void AddMessages()
        {
            Character firstChar = (Character)commandParameters.firstObject;
            Character secondChar = (Character)commandParameters.secondObject;

            #region General Fuck
            messages[0] = new List<msgParams>();
            messages[0].Add(new msgParams("<italicson>"));
            messages[0].Add(new msgParams("[char1.genderPronoun] <replace,are,char1> on [char2.objPronoun] before [char2.genderPronoun] can say or do anything. <perspReplace,your,char2> needs are something that neither can ignore. <perspReplace,your,char2> tongues dance in and out of each others mouths. [char2.genderPronoun] <replace,tear,char2> at [char1.possPronoun] pants with frenzied lust. [char1.genderPronoun] <replace,lift,char1> the hem of [char2.possPronoun] dress to [char2.possPronoun] waist and <replace,sit,char1> [char2.objPronoun] on the edge of the conference table. Within seconds [char1.genderPronoun] <replace,are,char1> plunging [char1.possPronoun] cock into [char2.possPronoun] <orifice,char2>.\n"));
            if (secondChar.HasCock)
            {
                messages[0].Add(new msgParams("\n\n"));
                messages[0].Add(new msgParams("[char2.possPronoun.uf] hard [char2.CockLength] inch cock bouncing up and down as [char1.genderPronoun] <replace,thrust,char1> into [char2.article] <orifice,char2>."));
            }
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("[char1.genderPronoun] <replace,pound,char1> mercilessly in and out of [char2.objPronoun]. Nothing can be heard except for the slapping of [char1.possPronoun] sac against [char2.article] ass. [char2.genderPronoun] <replace,cling,char2> to [char1.objPronoun] and <replace,beg,char2> [char1.objPronoun] to give [char2.objPronoun] release.\n"));
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("'Fuck me! Fuck me harder!' your [char2.Relationship] hisses into [char1.possPronoun] ear.\n"));
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("[char1.genderPronoun] <replace,push,char1> [char2.objPronoun] back onto the table and <replace,lift,char1> [char2.possPronoun] legs onto [char1.possPronoun] shoulders, spreading [char2.objPronoun] out before [char1.objPronoun]. [char1.genderPronoun] <replace,thrust,char1> into to [char2.objPronoun] harder and faster. Neither of <replace,you,char2> care if the noise can be heard outside the room; nothing matters but the mutual need for release."));
            messages[0].Add(new msgParams("\n\n"));
            if (secondChar.HasCock)
            {
                messages[0].Add(new msgParams("As [char1.genderPronoun] <replace,fuck,char1> [char2.objPronoun], [char1.genderPronoun] <replace,rub,char1> and <replace,play,char1> with [char2.possPronoun] hard cock."));
            }
            else
            {
                messages[0].Add(new msgParams("As [char1.genderPronoun] <replace,fuck,char1> [char2.article], [char1.genderPronoun] <replace,pinch,char1> and <replace,play,char1> with [char2.article] clit."));
            }
            messages[0].Add(new msgParams("Just seeing [char1.objPronoun] shoving [char1.possPronoun] hardened cock into [char2.possPronoun] <orifice,char2> pushes [char2.objPronoun] closer to the edge of sexual oblivion. [char2.genderPronoun] <replace,wrap,char2> [char2.possPronoun] legs around [char1.objPronoun] and [char1.genderPronoun] <replace,lift,char1> [char2.objPronoun] up to [char1.possPronoun] chest.\n"));
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("'Yes! Yes! Right fucking there! I'm cumming!' [char2.genderPronoun] screams against [char1.possPronoun] neck.\n"));
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("[char2.article] climax pushes [char1.objPronoun] over the edge. [char1.genderPronoun] <replace,impale,char1> [char2.objPronoun] hard and <replace,shoot,char1> [char1.possPronoun] load deep within [char2.article] <orifice,char2>. The walls of [char2.article] <orifice,char2> milks [char1.objPronoun] for every last drop. "));
            if (secondChar.HasCock)
            {
                messages[0].Add(new msgParams("\n\n"));
                messages[0].Add(new msgParams("As [char2.genderPronoun] comes your [char2.Relationship]'s cock sprays spurt after spurt of come over both of <replace,you,char1>"));
            }
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("You collapse into a chair and <replace,you,char1> cling to each other trying to catch a breath. [char2.genderPronoun] doesn't have the strength to remove [char2.objPronoun]self from [char1.objPronoun]. So [char2.genderPronoun] just sits on top of [char1.objPronoun], with [char1.possPronoun] cock still throbbing within [char2.article] <orifice,char2>.\n"));
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("[char2.genderPronoun] <replace,lift,char2> [char2.article] head and looks into [char1.possPronoun] eyes. No words were needed. [char2.genderPronoun] leans forward and gently places [char2.article] mouth onto [char1.possPronoun] lips.\n"));
            messages[0].Add(new msgParams("\n\n"));
            messages[0].Add(new msgParams("After a while, [char2.genderPronoun] lifts [char2.objPronoun]self off [char1.genderPronoun]. <replace,your,char1> combined juices running down [char2.article] leg\n"));
            messages[0].Add(new msgParams("\n\n"));
            if (secondChar.Sexuality == Sexuality.slut && secondChar.HasPussy)
            {
                messages[0].Add(new msgParams("Reaching down between [char2.article] legs, your [char2.Relationship] slides a couple of fingers into [char2.article] cunt, lifting the fingers to [char2.article] mouth licking the juices off and swallowing them.\n"));
            }
            messages[0].Add(new msgParams("<italicsoff>"));

            secondChar.isCreampied = true;
            secondChar.lastFuck = firstChar;
            #endregion
        }
    }
}
