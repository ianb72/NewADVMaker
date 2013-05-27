using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NewADVMaker.Clothing;

namespace NewADVMaker
{
    public class Character : GameObject
    {
        #region Variables
        bool _isCreampied;
        Dictionary<RelationShipAll, RelationShipAll> relationshipLUT = new Dictionary<RelationShipAll, RelationShipAll>();
        #endregion
        #region Properties
        public string CharacterName { get; set; }
        public string Firstname { get; set; }
        public Surname Surname
        {
            get; 
            set; 
        }
        public string FullNameString
        {
            get { return GetFullNameString(); }
        }
        public int Age { get; set; }
        public Gender Gender { get; set;}
        public RelationShipAll Relationship { get; set; }
        public RelationShipAll FamilyRelationship { get; set; }
        public string RelationshipString
        {
            get { return GetRelationShipString(); }
        }
        
        public DateTime EnagagementDate { get; set; }
        public DateTime MarriageDate { get; set; }

        public BodyType BodyType { get; set; }
        public AssType AssType { get; set; }
        public SexualPreference SexualPreference { get; set; }
        public Sexuality Sexuality { get; set; }

        public bool HasBreasts { get; set; }
        public CupSize BreastCupSize { get; set; }
        public int BreastSize { get; set; }

        public bool HasCock { get; set; }
        public int CockLength { get; set; }
        public int CockThickness { get; set; }
        public string CockDescription { get; set; }

        public bool HasPussy { get; set; }
        public PussyType PussyType { get; set; }
        public string PussyTypeString
        {
            get { return GetPussyTypeString(); }
        }
        public AssholeType AssholeType { get; set; }
       
        public bool IsBloodRelated { get; set; }

        public bool isAnalVirgin { get; set; }
        public bool isCreampied
        {
            get { return _isCreampied; }
            set { _isCreampied = value; GetPussyTypeString(); }
        }
        public Character lastFuck { get; set; }
        public bool isCrotchCovered
        {
            get { return GetCrotchCovered(); }
        }

        public bool hasBeenFucked { get; set; }
        public bool hasBeenKissed { get; set; }
        public bool hasBeenFingered { get; set; }
        public bool hasBeenLicked { get; set; }
        public bool hasBeenSucked { get; set; }
        public bool hasSucked { get; set; }

        public int fuckCount { get; set; }
        public int kissCount { get; set; }
        public int fingerCount { get; set; }
        public int lickCount { get; set; }
        public int beenSuckedCount { get; set; }
        public int suckCount { get; set; }

        public bool HasMum { get; set; }
        public bool HasDad { get; set; }

        public string alias { get; set; }
        public string possPronoun { get; set; }
        public string objPronoun { get; set; }
        public string article { get; set; }
        public string genderPronoun { get; set; }
        public int Arousal { get; set; }

        public Dictionary<string,ClothingBase> clothing { get; set; }
        public List<Character> charactersKissed { get; set; }

        public GameObject location { get; set; }
        public bool isNotOnFloor { get; set; }
        public List<GameObject> inventory { get; set; }

        public List<GameObject> bodyParts { get; set; }

        public object mainGameForm { get; set; }

        #endregion
        #region Constructors
        public Character(bool isPlayerCharacter = false,string alias="")
        {
            Init();
        }
        public Character(Gender gender)
        {
            this.Gender = gender;
            this.alternateNames = new List<string>();
            Init();
            SetGenderDefaults();
        }
        #endregion
        #region Public Methods
        public void SetProperty(string propertyName, object propertyValue)
        {
            PropertyInfo propInfo= this.GetType().GetProperty(propertyName);

            if (propInfo.PropertyType.BaseType == typeof(System.Enum))
            {
                propertyValue = Convert.ToInt32(propertyValue);
                Type EnumType = propInfo.PropertyType;
                if(EnumType == typeof(Gender)) { propertyValue = (Gender)propertyValue; }
                if (EnumType == typeof(Surname)) { propertyValue = (Surname)propertyValue; }
                if (EnumType == typeof(RelationShipAll)) { propertyValue = (RelationShipAll)propertyValue; }
                if (EnumType == typeof(BodyType)) { propertyValue = (BodyType)propertyValue; }
                if (EnumType == typeof(AssType)) { propertyValue = (AssType)propertyValue; }
                if (EnumType == typeof(SexualPreference)) { propertyValue = (SexualPreference)propertyValue; }
                if (EnumType == typeof(Sexuality)) { propertyValue = (Sexuality)propertyValue; }
                if (EnumType == typeof(CupSize)) { propertyValue = (CupSize)propertyValue; }
                if (EnumType == typeof(PussyType)) { propertyValue = (PussyType)propertyValue; }
                if (EnumType == typeof(AssholeType)) { propertyValue = (AssholeType)propertyValue; }
            }

            propInfo.SetValue(this, Convert.ChangeType(propertyValue, propInfo.PropertyType),null);

        }
        public void Wear(string objectName,GameObject clothingObject)
        {
            if (clothingObject.GetType().BaseType == typeof(Outfits.OutfitBase))
            {

                Outfits.OutfitBase outfitObject = (Outfits.OutfitBase)clothingObject;
                foreach (KeyValuePair<string, ClothingBase> kvp in outfitObject.clothing)
                {
                    this.Wear(kvp.Key, kvp.Value);
                }
            }

            try
            {

                if (clothingObject.ObjectName.Contains(this.Firstname))
                {
                    clothingObject.ObjectName = clothingObject.ObjectName.Remove(0, this.Firstname.Length + 2);
                }

                this.clothing.Add(objectName, (ClothingBase) clothingObject);
                clothingObject.Wear(this);
            }
            catch
            {
                throw new Exception("unable to put on");
            }
        }
        public void Remove(string objectName)
        {
            ClothingBase clothingObject = (ClothingBase) this.clothing[objectName];
            this.clothing.Remove(objectName);

            clothingObject.Remove(this);
        }
        public void SetPronouns()
        {
            if (isPlayerCharacter)
            {
                genderPronoun = "you";
                article = "you";
                possPronoun = "your";
                objPronoun = "you";
            }
            else if (this.Gender == NewADVMaker.Gender.Male)
            {
                genderPronoun = "he";
                article = "his";
                possPronoun = "his";
                objPronoun = "him";
            }
            else
            {
                genderPronoun = "she";
                article = "her";
                possPronoun = "her";
                objPronoun = "her";
            }
        }
        public void WriteMessage(NewADVMaker.msgParams messageParams)
        {
            Type type = mainGameForm.GetType();
            MethodInfo mi = type.GetMethod("msg");
            object[] parameters = new object[] { messageParams };

            mi.Invoke(mainGameForm, parameters);
        }
        #endregion
        #region Private Methods
        private string GetFullNameString()
        {
            return string.Format("{0} {1}", this.Firstname, this.Surname);
        }
        private void Init()
        {
            this.isPlayerCharacter = isPlayerCharacter;
            this.alias = "";
            this.charactersKissed = new List<Character>();
            this.inventory = new List<GameObject>();
            this.bodyParts = new List<GameObject>();
            this.clothing = new Dictionary<string, ClothingBase>();
            this.alternateNames = new List<string>();
            if (isPlayerCharacter) { SetPronouns(); }

        }
        private void InitRelationshipLUT()
        {

        }
        private void SetGenderDefaults()
        {
            this.bodyParts.Add(new BodyParts.ass());
            this.bodyParts.Add(new BodyParts.nipples());

            switch (this.Gender)
            {
                case NewADVMaker.Gender.Female:
                    this.HasPussy = true;
                    this.HasBreasts = true;
                    this.bodyParts.Add(new BodyParts.pussy());
                    this.bodyParts.Add(new BodyParts.tits());
                    break;
                case NewADVMaker.Gender.Male:
                    this.HasCock = true;
                    this.bodyParts.Add(new BodyParts.cock());
                    break;
                case NewADVMaker.Gender.Herm:
                    this.HasPussy = true;
                    this.HasBreasts = true;
                    this.HasCock = true;
                    this.bodyParts.Add(new BodyParts.pussy());
                    this.bodyParts.Add(new BodyParts.tits());
                    this.bodyParts.Add(new BodyParts.cock());
                    break;
                case NewADVMaker.Gender.Shemale:
                    this.HasBreasts = true;
                    this.HasCock = true;
                    this.bodyParts.Add(new BodyParts.tits());
                    this.bodyParts.Add(new BodyParts.cock());
                    break;
            }
        }
        #endregion
        #region Functions
        private String GetPussyTypeString()
        {
            if (_isCreampied)
            {
                return "cum filled";
            }

            switch (this.PussyType)
            {
                case PussyType.wellFucked:
                    return "well fucked";
            }


            return this.PussyType.ToString();
        }
        public string TheirRelationship(Character secondChar)
        {


            return "";
        }
        public override ClothingBase getClothingObject(string objectName)
        {
            ClothingBase tempObject;
            this.clothing.TryGetValue(objectName,out tempObject);
            return tempObject;
        }
        public string GetCurrentVisibleClothingString()
        {
            Console.WriteLine("Getting clothing");

            StringBuilder toReturn = new StringBuilder();

            foreach (KeyValuePair<string,ClothingBase> kvp in clothing)
            {
                Clothing.ClothingBase currentGarment =(ClothingBase) kvp.Value;

                foreach (ClothingPosition clothingPos in currentGarment.clothingPosition)
                {
                    ClothingBase garment = GetTopMostGarment(clothingPos);
                    if(!toReturn.ToString().Contains(garment.ObjectName))
                    {
                        toReturn.Append(garment.Description + " (" +  garment.ObjectName + ")\n");
                    }
                }
               
            }
            
            return toReturn.ToString();
        }
        public ClothingBase GetTopMostGarment(ClothingPosition clothingPos)
        {
            for (var layer = 10; layer != 0; layer--)
            {
                foreach (KeyValuePair<string, ClothingBase> kvp in clothing)
                {
                    ClothingBase garment = kvp.Value;
                    foreach (ClothingPosition clothPos in garment.clothingPosition)
                    {
                        if (clothPos == clothingPos && garment.layer == layer)
                        {
                            return garment;
                        }
                    }
                }
            }

            return null;
        }
        public string GetRelationShipString()
        {
            Dictionary<RelationShipAll,string> relationshipStringLUT = new Dictionary<RelationShipAll,string>();

            relationshipStringLUT.Add(RelationShipAll.bestfriend,"best friend");
            
            string toReturn = "";
            if(relationshipStringLUT.TryGetValue(this.Relationship,out toReturn))
            {
                return toReturn;
            }
            else
            {
                return this.Relationship.ToString();
            }
        }
        private bool GetCrotchCovered()
        {
            bool toReturn = false;

            foreach (KeyValuePair<string,ClothingBase> kvp in clothing)
            {
                if (kvp.Value.clothingPosition.Contains(ClothingPosition.crotch))
                {
                    toReturn = true;
                }
            }

            return toReturn;
        }
        #endregion
        #region Enum String Translators
        
        #endregion
    }
}
