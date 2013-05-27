using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker
{
    public class msgParams
    {
        public string MessageText { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public System.Drawing.Color TextColour { get; set; }
        public Character Char1 { get; set; }
        public Character Char2 { get; set; }
        public Character Char3 { get; set; }
        public GameObject Object1 { get; set; }
        public GameObject Object2 { get; set; }

        public msgParams(string messageText, bool bold, bool italic, System.Drawing.Color colour)
        {
            this.MessageText = messageText;
            this.Bold = bold;
            this.Italic = italic;
            this.TextColour = colour;
        }
        public msgParams(string messageText)
            :this(messageText,false,false,System.Drawing.Color.Black)
        {
        }

    }
}
