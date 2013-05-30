using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker.Commands
{
    public class StandardCommandList : Interfaces.IcommandList
    {
        public MainGameForm.MessageHandler messageHandler { get; set; }

        public StandardCommandList()
        {
        }

        public void msg(CommandParameters commandParameters, msgParams messageParams)
        {
            messageHandler.Invoke(messageParams);
        }
        public void look_at(CommandParameters commandParameters)
        {
          
        }
        public void marry(CommandParameters commandParameters)
        {
           
        }
        public void proposeto(CommandParameters commandParameters)
        {
          
        }
        public void listchars(CommandParameters commandParameters)
        {
           
        }
        public void random(CommandParameters commandParameters)
        {
        }
        public void random_male(CommandParameters commandParameters)
        {
        }
        public void random_female(CommandParameters commandParameters)
        {
        }
        public void random_shemale(CommandParameters commandParameters)
        {
        }
        public void random_herm(CommandParameters commandParameters)
        {
        }
        public void rub(CommandParameters commandParameters)
        {

        }
        public void look(CommandParameters commandParameters)
        {
           
        }
        public void go(CommandParameters commandParameters)
        {
            
        }
        public void exits(CommandParameters commandParameters)
        {
          
        }
        public void call(CommandParameters commandParameters)
        {
            
        }
        public void wear(CommandParameters commandParameters)
        {
            
        }
        public void remove(CommandParameters commandParameters)
        {
            
        }
        public void get_on(CommandParameters commandParameters)
        {
           
        }
        public void use(CommandParameters commandParameters)
        {
        }
        public void kiss(CommandParameters commandParameters)
        {
            
        }
        public void examine(CommandParameters commandParameters)
        {

        }
        public void clothing(CommandParameters commandParameters)
        {
           

        }
        public void open(CommandParameters commandParameters)
        {
            
        }
        public void close(CommandParameters commandParameters)
        {
           
        }
        public void i(CommandParameters commandParameters)
        {
            inventory(commandParameters);
        }
        public void inventory(CommandParameters commandParameters)
        {
           
        }
        public void take(CommandParameters commandParameters)
        {
            
        }
        public void put_away(CommandParameters commandParameters)
        {
          
        }
        public void drop(CommandParameters commandParameters)
        {
            
        }
        public void give(CommandParameters commandParameters)
        {
           
        }

        }
}
