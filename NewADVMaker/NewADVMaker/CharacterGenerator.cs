using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtensionMethods;

namespace NewADVMaker
{
    public class CharacterGenerator
    {
        #region Public Properties
        public List<Character> gameCharacters { get; set; }
        public Character playerCharacter { get; set; }
        #endregion
        #region Constructors
        public CharacterGenerator()
        {
        }
        #endregion
        #region Variables
        Characteristics charactristics = new Characteristics();
        #endregion
        #region Public Methods
        public Character NewRandomCharacter(List<Character> gameCharacters,Character playerCharacter,Gender gender = Gender.None)
        {
            Character newCharacter = new Character();

            bool nameFree = false;

            Console.WriteLine("*************************");

            do
            {
                newCharacter = GenerateRandomCharacter(gameCharacters, playerCharacter,gender);

                foreach (Character gameCharacter in gameCharacters)
                {
                    nameFree = gameCharacter.FullNameString != newCharacter.FullNameString;
                    Console.WriteLine(nameFree + " " + gameCharacter.FullNameString + " - " + newCharacter.FullNameString);
                    if (!nameFree) { break; }
                }
            }
            while (nameFree == false);

            Console.WriteLine("*************************");

            return newCharacter;
        }
        public static Character NewCharacter(Gender gender,FirstName firstname,Surname surname)
        {
            Character toReturn = new Character();
            toReturn.Gender = gender;
            toReturn.Firstname = firstname.ToString();
            toReturn.Surname = surname;
            toReturn.CharacterName = toReturn.Firstname;
            toReturn.alias = toReturn.Firstname;
            toReturn.Relationship = RelationShipAll.friend;
            toReturn.FamilyRelationship = toReturn.Relationship;
            toReturn.IsBloodRelated = false;
            toReturn.Age = 21;
            toReturn.BodyType = BodyType.average;
            toReturn.AssType = AssType.round;
            toReturn.SexualPreference = SexualPreference.bi;
            toReturn.Sexuality = Sexuality.easy;
            toReturn.isAnalVirgin = true;

            toReturn.HasCock = gender != Gender.Female;
            toReturn.CockLength = 6;
            toReturn.CockThickness = 2;
            toReturn.CockDescription = "cock";

            toReturn.HasPussy = (gender != Gender.Male && gender != Gender.Shemale);
            toReturn.PussyType = PussyType.tight;
            toReturn.SetPronouns();

            toReturn.HasBreasts = (gender != Gender.Male);
            toReturn.BreastCupSize = CupSize.D;
            toReturn.BreastSize = 36;


            if(toReturn.HasCock) {toReturn.bodyParts.Add(new BodyParts.cock());}
            if (toReturn.HasPussy) { toReturn.bodyParts.Add(new BodyParts.pussy()); }
            if (toReturn.HasBreasts) { toReturn.bodyParts.Add(new BodyParts.tits()); }

            toReturn.bodyParts.Add(new BodyParts.ass());
            toReturn.bodyParts.Add(new BodyParts.nipples());

            return toReturn;
        }

        #endregion
        #region Private Functions
        private Character GenerateRandomCharacter(List<Character> gameCharacters, Character playerCharacter)
        {
            return GenerateRandomCharacter(gameCharacters, playerCharacter, Gender.None);
        }
        private Character GenerateRandomCharacter(List<Character> gameCharacters, Character playerCharacter,Gender gender)
        {
            this.gameCharacters = gameCharacters;
            this.playerCharacter = playerCharacter;

            Character toReturn = new Character();
            var randomGenerator = new Random();

            if (gender == Gender.None)
            {
                toReturn.Gender = (Gender)randomGenerator.Next(1, Enum.GetNames(typeof(Gender)).Length);
            }
            else
            {
                toReturn.Gender = gender;
            }
            toReturn.Firstname = toReturn.Gender == NewADVMaker.Gender.Male ? NewRandomMaleName() : NewRandomFemaleName();
            toReturn.CharacterName = toReturn.Firstname;
            toReturn.alias = toReturn.Firstname;
            toReturn.Relationship = NewRandomRelationShip(toReturn.Gender);
            toReturn.FamilyRelationship = toReturn.Relationship;
            toReturn.IsBloodRelated = IsBloodRelated(toReturn.Relationship);
            toReturn.Surname = NewRandomSurname(toReturn.Relationship);
            toReturn.Age = NewRandomAge(toReturn.Relationship);
            toReturn.BodyType = NewRandomBodyType(toReturn.Age);
            toReturn.AssType = NewAssType(toReturn.BodyType);
            toReturn.SexualPreference = NewRandomSexualPreference(toReturn.Gender);
            toReturn.Sexuality = NewRandomSexuality(toReturn.Relationship);
            toReturn.isAnalVirgin = randomGenerator.Next(2) == 1;
            toReturn.AssholeType = NewAssholeType();
            toReturn.isAnalVirgin = toReturn.AssholeType == AssholeType.virgin;
            toReturn.isPlayerCharacter = false;
            toReturn.Description = "";
            toReturn.ObjectName = toReturn.Firstname.ToLower();
            toReturn.bodyParts.Add(new BodyParts.ass());
            toReturn.bodyParts.Add(new BodyParts.nipples());

            toReturn.HasPussy = toReturn.Gender != Gender.Male && toReturn.Gender != Gender.Shemale;
            if (toReturn.HasPussy)
            {
                toReturn.bodyParts.Add(new BodyParts.pussy());
                toReturn.PussyType = NewRandomPussyType(toReturn.Relationship, toReturn.Age, toReturn.Sexuality);
            }

            toReturn.HasCock = toReturn.Gender != Gender.Female;
            if (toReturn.HasCock)
            {
                toReturn.bodyParts.Add(new BodyParts.cock());

                int maxLength = 14;
                int maxThickness = 7;

                if (toReturn.Age < 13)
                {
                    maxLength = 6;
                    maxThickness = 2;
                }

                toReturn.CockLength = randomGenerator.Next(4, maxLength);
                toReturn.CockThickness = randomGenerator.Next(2, maxThickness);
                toReturn.CockDescription = "cock";

            }

            if (toReturn.Gender != Gender.Male && toReturn.Age > 13)
            {
                toReturn.HasBreasts = true;
                toReturn.BreastSize = randomGenerator.Next(14, 25) * 2;
                toReturn.BreastCupSize = ((CupSize)randomGenerator.Next(Enum.GetNames(typeof(CupSize)).Length));
            }

            if (toReturn.Relationship == RelationShipAll.wife || toReturn.Relationship == RelationShipAll.husband)
            {
                toReturn.MarriageDate = DateTime.Now;
            }
            if (toReturn.Relationship == RelationShipAll.fiance)
            {
                toReturn.EnagagementDate = DateTime.Now;
            }

            return toReturn;
        }
        private string NewRandomFemaleName()
        {
            var randomGenerator = new Random();
            string toReturn = ((FirstNameFemale)randomGenerator.Next(Enum.GetNames(typeof(FirstNameFemale)).Length)).ToString();
            return toReturn;
        }
        private string NewRandomMaleName()
        {
            var randomGenerator = new Random();
            string toReturn = ((FirstNameMale)randomGenerator.Next(Enum.GetNames(typeof(FirstNameMale)).Length)).ToString();
            return toReturn;
        }
        private Surname NewRandomSurname(RelationShipAll relationShip)
        {
            if (HasSameSurname(relationShip))
            {
                return playerCharacter.Surname;
            }
            else
            {
                var randomGenerator = new Random();
                return  ((Surname)randomGenerator.Next(Enum.GetNames(typeof(Surname)).Length));
            }
        }
        private RelationShipAll NewRandomRelationShip(Gender gender)
        {
            RelationShipAll toReturn = RelationShipAll.stranger;

            var randomGenerator = new Random();
            RelationshipFemale femaleRelation = ((RelationshipFemale)randomGenerator.Next(Enum.GetNames(typeof(RelationshipFemale)).Length));
            RelationshipMale maleRelation = ((RelationshipMale)randomGenerator.Next(Enum.GetNames(typeof(RelationshipMale)).Length));

            if (gender == Gender.Male)
            {
                toReturn = Extensions.ConvertTo<RelationShipAll>(maleRelation);
            }
            else
            {
                toReturn = Extensions.ConvertTo<RelationShipAll>(femaleRelation);
            }

            if (toReturn == RelationShipAll.dad && playerCharacter.HasDad)
            {
                return NewRandomRelationShip(gender);
            }

            if (toReturn == RelationShipAll.mum && playerCharacter.HasMum)
            {
                return NewRandomRelationShip(gender);
            }

            if (toReturn == RelationShipAll.dad) { playerCharacter.HasDad = true; }
            if (toReturn == RelationShipAll.mum) { playerCharacter.HasMum = true; }

            return toReturn;
        }
        private int NewRandomAge(RelationShipAll relationShip)
        {
            var randomGenerator = new Random();
            int playerAge = playerCharacter.Age;
            int newAgeMin = 8;
            int newAgeMax = 70;

            if (relationShip == RelationShipAll.mum || relationShip == RelationShipAll.dad)
            {
                newAgeMin = playerAge + 16;
                newAgeMax = playerAge + 30;
            }

            if (relationShip == RelationShipAll.son || relationShip == RelationShipAll.daughter)
            {
                newAgeMin = 5;
                newAgeMax = Math.Min(16,playerAge - 16);
            }

            if (relationShip == RelationShipAll.grandad || relationShip == RelationShipAll.granny)
            {
                newAgeMin = playerAge+ 30;
                newAgeMax = playerAge + 40;
            }
            if (relationShip == RelationShipAll.husband || relationShip == RelationShipAll.wife || relationShip == RelationShipAll.fiance)
            {
                newAgeMin = 16;
            }

            return randomGenerator.Next(newAgeMin, newAgeMax);
        }
        private BodyType NewRandomBodyType(int age)
        {
            var randomGenerator = new Random();
            BodyType toReturn = ((BodyType)randomGenerator.Next(Enum.GetNames(typeof(BodyType)).Length));

            if (toReturn == BodyType.young && age > 16)
            {
                while (toReturn == BodyType.young)
                {
                    toReturn = ((BodyType)randomGenerator.Next(Enum.GetNames(typeof(BodyType)).Length));
                }
            }
            else if (age < 16)
            {
                toReturn = BodyType.young;
            }

            return toReturn;
        }
        private SexualPreference NewRandomSexualPreference(Gender gender)
        {
            var randomGenerator = new Random();
            SexualPreference sexualPref = (SexualPreference)randomGenerator.Next(Enum.GetNames(typeof(SexualPreference)).Length);
            if (sexualPref == SexualPreference.gay && gender == Gender.Female)
            {
                sexualPref = SexualPreference.lesbian;
            }
            if (sexualPref == SexualPreference.lesbian && gender == Gender.Male)
            {
                sexualPref = SexualPreference.gay;
            }

            if (gender == Gender.Herm) { sexualPref = SexualPreference.bi; }

            return sexualPref;
        }
        private PussyType NewRandomPussyType(RelationShipAll relationShip, int age,Sexuality sexuality)
        {
            if (sexuality == Sexuality.virgin) { return PussyType.virgin; }
            if (age < 10) { return PussyType.virgin; }

            var randomGenerator = new Random();
            PussyType toReturn = ((PussyType)randomGenerator.Next(Enum.GetNames(typeof(PussyType)).Length));
            if ((toReturn == PussyType.virgin && ImpossibleVirgin(relationShip)) || sexuality==Sexuality.slut || sexuality==Sexuality.easy)
            {
                while (toReturn == PussyType.virgin)
                {
                    toReturn = ((PussyType)randomGenerator.Next(Enum.GetNames(typeof(PussyType)).Length));
                }
            }

            return toReturn;
        }
        private Sexuality NewRandomSexuality(RelationShipAll relationShip)
        {
            var randomGenerator = new Random();
            Sexuality toReturn = ((Sexuality)randomGenerator.Next(Enum.GetNames(typeof(Sexuality)).Length));
            if (toReturn == Sexuality.virgin && ImpossibleVirgin(relationShip))
            {
                while (toReturn == Sexuality.virgin && ImpossibleVirgin(relationShip))
                {
                    toReturn = ((Sexuality)randomGenerator.Next(Enum.GetNames(typeof(Sexuality)).Length));
                }
            }

            return toReturn;
        }
        private AssholeType NewAssholeType()
        {
            AssholeType toReturn = AssholeType.tight;
            var randomGenerator = new Random();
            toReturn = ((AssholeType)randomGenerator.Next(Enum.GetNames(typeof(AssholeType)).Length));

            return toReturn;
        }
        private AssType NewAssType(BodyType bodytype)
        {
            return charactristics.GetAssType(bodytype);
        }
        #endregion
        #region Functions
        private bool IsBloodRelated(RelationShipAll relationShip)
        {
            bool toReturn = false;
            foreach (BloodRelation relationship in Enum.GetValues(typeof(BloodRelation)))
            {
                if (relationship.ToString() == relationShip.ToString())
                {
                    toReturn = true;
                }
                
            }
            return toReturn;
        }
        private bool HasSameSurname(RelationShipAll relationShip)
        {
            bool toReturn = false;
            foreach (SameSurname relationship in Enum.GetValues(typeof(SameSurname)))
            {
                if (relationship.ToString() == relationShip.ToString())
                {
                    toReturn = true;
                }

            }
            return toReturn;
        }
        private bool ImpossibleVirgin(RelationShipAll relationShip)
        {
            bool toReturn = false;
            foreach (NonVirgin nonVirgin in Enum.GetValues(typeof(NonVirgin)))
            {
                if (nonVirgin.ToString() == relationShip.ToString())
                {
                    toReturn = true;
                }
                
            }
            return toReturn;
        }
        #endregion
    }
}
