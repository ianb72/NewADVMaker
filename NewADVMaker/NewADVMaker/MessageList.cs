using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NewADVMaker
{
    public class MessageList
    {
        public string[] messages { get; set; }
        public string Filename {get; set;}

        public MessageList(string Filename)
        {
            this.Filename = Filename;
            this.messages = new string[2048];
            PopulateList();
        }

        private void PopulateList()
        {
            StreamReader sr = new StreamReader(Filename);
            while (!sr.EndOfStream)
            {
                StringBuilder sb = new StringBuilder();
                string lineRead = sr.ReadLine();
                if (lineRead.Contains("MESSAGE"))
                {
                    int messageNumber = Convert.ToInt32(lineRead.Split(' ')[1]);

                    while(!lineRead.Contains("END_MESSAGE"))
                    {
                        lineRead = sr.ReadLine();
                        if(!lineRead.Contains("END_MESSAGE")) { sb.AppendLine(lineRead);}

                    }

                    messages[messageNumber] = sb.ToString();
                }

            }
        }
    }
}
