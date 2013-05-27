using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Characters
{
    public class stacey : Character
    {
        public stacey()
            :base(Gender.Female)
        {
            this.CharacterName = "stacey";
            this.Firstname = "stacey";
            this.alias = "stacey";
            this.ObjectName = "stacey";
            this.Surname = Surname.Duffy;
            this.Gender = Gender.Female;
            this.Relationship = RelationShipAll.bestfriend;
            this.BreastSize = 38;
            this.BreastCupSize = CupSize.DD;
            this.Sexuality = Sexuality.slut;
            this.Age = 22;
            this.BodyType = BodyType.chubby;
            this.PussyType = PussyType.tight;
            this.Description = "";
            this.SetPronouns();
        }
    }
}
