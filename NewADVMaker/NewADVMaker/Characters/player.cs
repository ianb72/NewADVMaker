using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Characters
{
    public class player : Character
    {
        public player()
        {
            this.CharacterName = "Player";
            this.isPlayerCharacter = true;
            this.alias = "you";
            this.Firstname = "Ian";
            this.Description = "";
            this.ObjectName = "player";
            this.Surname = Surname.Butterworth;
            this.Age = 40;
            this.HasCock = true;
            this.CockLength = 9;
            this.CockThickness = 4;
            this.HasPussy = true;
            this.PussyType = NewADVMaker.PussyType.tight;

            this.bodyParts.Add(new BodyParts.ass());
            this.bodyParts.Add(new BodyParts.cock());
            this.bodyParts.Add(new BodyParts.nipples());

            this.SetPronouns();
        }
    }
}
