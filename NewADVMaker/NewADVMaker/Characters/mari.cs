using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Characters
{
    public class mari : Character
    {
        public mari()
            :base(Gender.Female)
        {
            this.CharacterName = "mari";
            this.Firstname = "mari";
            this.alias = "mari";
            this.ObjectName = "mari";
            this.Surname = Surname.Butterworth;
            this.Relationship = RelationShipAll.wife;
            this.BreastSize = 38;
            this.BreastCupSize = CupSize.D;
            this.Sexuality = Sexuality.loving;
            this.Age = 43;
            this.BodyType = BodyType.chubby;
            this.PussyType = PussyType.tight;
            this.AssType = NewADVMaker.AssType.chubby;
            this.Description = "";
            this.SetPronouns();
        }
    }
}
