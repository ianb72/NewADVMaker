using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker
{
    public class GameObject
    {
        public string Description { get; set; }
        public bool isPlayerCharacter { get; set; }
        public bool isRoom { get; set; }
        public string ObjectName { get; set; }
        public string DisplayName { get; set; }
        public Rooms.RoomBase currentRoom { get; set; }
        public bool isWearable { get; set; }
        public string occupiedString { get; set; }
        public Container parentContainer { get; set; }
        public MainGameForm.MessageHandler messageHandler { get; set; }
        public string giveMessage { get; set; }
        public string takeMessage { get; set; }
        public ObjectType objectType { get; set; }
        public List<string> alternateNames { get; set; }
        public Dictionary<string, object> customVariable { get; set; }

        public GameObject()
        {
            this.objectType = ObjectType.Generic;
            this.alternateNames = new List<string>();
            this.customVariable = new Dictionary<string, object>();
        }

        public GameObject(string description)
        {
            this.Description = description;
            this.ObjectName = description;
            this.objectType = ObjectType.Generic;
            this.alternateNames = new List<string>();
            this.customVariable = new Dictionary<string, object>();
        }

        public virtual void Wear(Character targetCharacter)
        {
        }
        public virtual void Remove(Character targerCharacter)
        {
        }
        public virtual Clothing.ClothingBase getClothingObject(string objectName)
        {
            return null;
        }
        public virtual void Use(Commands.CommandParameters commandParameters)
        {

        }
    }
    public enum ObjectType
    {
        Generic,
        Dildo,
        Bed,
        Sofa,
        Chair
    }
}
