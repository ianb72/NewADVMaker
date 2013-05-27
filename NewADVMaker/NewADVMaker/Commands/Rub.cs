using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class Rub : CommandBase
    {
         public Rub(CommandParameters commandParameters)
            : base(commandParameters)
        {
            AddMessages();
        }
         public void AddMessages()
         {
             messages[0] = new List<msgParams>();
             Character firstChar = (Character)commandParameters.firstObject;
             Character secondChar = (Character)commandParameters.secondObject;

             if (firstChar == secondChar && firstChar.HasPussy)
             {
                 messages[0].Add(new msgParams("'God, you make me so hot,' [char1.Firstname.uf] says as she slowly begins to rub her own pussy in front of you. 'Look how wet I am,' she adds while she spreads her lips to give you a good view of her cunt."));
                 messages[0].Add(new msgParams("\n\n"));
                 messages[0].Add(new msgParams("As your [char1.Relationship] continues to pleasure herself, you can't help but keep your eyes glued to the incredable sexy sight as you watch her fingers work their way into own pussy."));
                 messages[0].Add(new msgParams("\n\n"));
                 messages[0].Add(new msgParams("Shit, I'm gunna cum,' she yells out as she finally brings herself to climax in front of you."));
             }

             if (firstChar != secondChar && secondChar.HasPussy)
             {
                 messages[0].Add(new msgParams("Running [char1.possPronoun] hand down your [char2.Relationship]'s [char2.BodyType] body, [char1.alias] fingers eventually reach the moist warmth of her sex. [char1.genderPronoun] gently <replace,begin,char1> to rub the outer lips of her pink slit coating [char1.possPronoun] fingers in her slick pussy juices"));
                 messages[0].Add(new msgParams("\n\n"));
                 messages[0].Add(new msgParams("As [char1.genderPronoun] <replace,continue,char1> working her pussy over with [char1.possPronoun] nimble fingers, [char1.genderPronoun] gradually <replace,start,char1> working her over rougher and rougher. Every once in a while [char1.genderPronoun] slide [char1.possPronoun] fingers up a bit and roll the hood of her clit."));
                 messages[0].Add(new msgParams("\n\n"));
                 messages[0].Add(new msgParams("With [char1.possPronoun] fingers working their magic, it's not long before [char2.Firstname.uf] is brought to the edge. 'Oh yes! Right there, don't stop!' she lets out as [char1.genderPronoun] <replace,rub,char1> her clit. [char1.genderPronoun] quickly <replace,notice,char1> [char2.Firstname.uf]'s body begin to quiver and then tighten up suddenly as she cums."));
             }
         }
    }
}
