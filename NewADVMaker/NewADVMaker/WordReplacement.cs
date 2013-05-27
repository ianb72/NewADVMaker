using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker
{
    public class WordReplacement
    {
        Dictionary<string, string> ActionReplacmentLUT = new Dictionary<string, string>();
        Dictionary<string, string> PerspRelacementLUT = new Dictionary<string, string>();

        public WordReplacement()
        {
            ActionReplacmentLUT.Add("are", "is");
            ActionReplacmentLUT.Add("you", "them");
            ActionReplacmentLUT.Add("pinch", "pinches");
            ActionReplacmentLUT.Add("push", "pushes");
            ActionReplacmentLUT.Add("have", "has");

            PerspRelacementLUT.Add("your", "their");
        }
        public string ReplaceActionWord(string wordToReplace,GameObject firstChar)
        {
            if (!firstChar.isPlayerCharacter)
            {
                if (ActionReplacmentLUT.ContainsKey(wordToReplace))
                {
                    return ActionReplacmentLUT[wordToReplace];
                }
                else
                {
                    return wordToReplace + "s";
                }
            }

            return wordToReplace;
        }
        public string ReplaceOrifice(GameObject secondChar)
        {
            Character requestChar = (Character)secondChar;
            return requestChar.Gender == Gender.Female ? "cunt" : "ass";
        }
        public string PerspReplace(string wordToReplace,GameObject firstChar,GameObject secondChar)
        {
            if (!firstChar.isPlayerCharacter && !secondChar.isPlayerCharacter)
            {
                return PerspRelacementLUT[wordToReplace];
            }

            return wordToReplace;
        }
    }
    
}
