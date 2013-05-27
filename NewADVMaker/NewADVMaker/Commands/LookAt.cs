using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class LookAt : CommandBase
    {
        public LookAt(CommandParameters commandParameters)
            : base(commandParameters)
        {
            AddMessages();
        }
        private void AddMessages()
        {
            Character requestedChar = (Character) commandParameters.secondObject;

            messages[0] = new List<msgParams>();
            messages[0].Add(new msgParams("Your [char1.Age] year old [char1.RelationshipString] has a gorgeous [char1.BodyType] body"));
            if (requestedChar.HasBreasts) { messages[0].Add(new msgParams(" and a pair of fantastic [char1.BreastSize][char1.BreastCupSize] tits")); }
            if (requestedChar.HasCock) { messages[0].Add(new msgParams("\n\n[char1.genderPronoun.uf] is sporting a lovely [char1.CockLength] inch long and [char1.CockThickness] inch thick [char1.CockDescription] that you can't wait to have buried in you!")); }
            messages[0].Add(new msgParams("\n\nWorking your gaze down [char1.possPronoun] body, you adore [char1.possPronoun] [char1.BodyType] belly"));
            if (requestedChar.HasPussy) { messages[0].Add(new msgParams("\n\n[char1.possPronoun.uf] edible looking [char1.PussyTypeString] cunt, really does look good enough to gorge on")); }
            messages[0].Add(new msgParams("\n\nTurning around for you, you drink in the sight of [char1.possPronoun] [char1.AssType] ass, with [char1.possPronoun] inviting looking [char1.AssholeType] asshole winking at you"));
            if (requestedChar.IsBloodRelated) { messages[0].Add(new msgParams("\n\nYou may be related to [char1.objPronoun] but you're sure that won't stop you indulging in a spot of incest!!")); }
                switch (requestedChar.Sexuality)
                {
                    case Sexuality.slut:
                        messages[0].Add(new msgParams("\n\nBeing the slut that [char1.genderPronoun] is, your [char1.Relationship] reaches over and runs [char1.possPronoun] hand up and down your cock"));
                        break;
                    case Sexuality.easy:
                        messages[0].Add(new msgParams("\n\nWhile not a total slapper, you know [char1.Firstname] doesn't need much persuasion to let you do all the things you're thinking of to [char1.objPronoun]"));
                        break;
                }
                switch (requestedChar.SexualPreference)
                {
                    case SexualPreference.bi:
                        messages[0].Add(new msgParams("\n\nAnd to your utter joy you know [char1.genderPronoun] loves getting fucked by a big hard cock and with [char1.possPronoun] face buried in a dripping wet cunt"));
                        break;
                    case SexualPreference.gay:
                        if (requestedChar.isAnalVirgin)
                        {
                            messages[0].Add(new msgParams("\n\n[char1.Firstname] would love you to take [char1.article] anal cherry"));
                        }
                        else
                        {
                            messages[0].Add(new msgParams("\n\n[char1.genderPronoun.uf] likes nothing more than a big cock up [char1.possPronoun] ass"));
                        }
                        break;
                    case SexualPreference.lesbian:
                        messages[0].Add(new msgParams("\n\n[char1.genderPronoun.uf] is happy when [char1.possPronoun] face is buried in a tight wet cunny"));
                        break;
                    case SexualPreference.straight:
                        if (requestedChar.Gender == Gender.Male)
                        {
                            messages[0].Add(new msgParams("\n\nHe loves burying his big hard cock into a girls pussy or ass"));
                        }
                        else if (requestedChar.Gender == Gender.Female)
                        {
                            if (requestedChar.PussyType == PussyType.virgin)
                            {
                                messages[0].Add(new msgParams("\n\nShe can't wait for you to take her tender virginity"));
                            }
                            else
                            {
                                messages[0].Add(new msgParams("\n\nLoves having a big hard cock in her [char1.PussyTypeString] cunt"));
                            }
                            if (requestedChar.AssholeType == AssholeType.virgin)
                            {
                                messages[0].Add(new msgParams("\n\n[char1.Firstname] would love you to take her anal cherry"));
                            }
                            else
                            {
                                messages[0].Add(new msgParams("\n\nLoves having a big hard cock in her [char1.AssholeType] ass"));
                            }
                        }
                        break;
                }

                if (requestedChar.clothing.Count != 0)
                {
                    messages[0].Add(new msgParams("\n\n[char1.genderPronoun.uf] is wearing\n", true, false, System.Drawing.Color.Black));
                    messages[0].Add(new msgParams("\n" + requestedChar.GetCurrentVisibleClothingString() + ".uf", false, false, System.Drawing.Color.Blue));
                }
                else
                {
                    messages[0].Add(new msgParams("\n\n[char1.genderPronoun.uf] is stark naked", true, false, System.Drawing.Color.Black));
                }
        }
        
    }
}
