using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone1.PresentationLayer
{
    class GUI
    {
        string _startMenuBar= "~Group25 ChatRoom~" + "\r\n" + "1- Register" + "\r\n" + "2- Login" + 
                                "\r\n" + "3- Exit";
        string _menuBar= "~Group25 ChatRoom~" + "\r\n" + "4- Send Message" + "\r\n" + "5- view" + 
                            "\r\n" + "6- logout" + "\r\n";
        Boolean isStart;

        public GUI()
        {
            Console.WriteLine(_startMenuBar);
            isStart = false;
            analayzeInput(Console.ReadLine());
        }

        public void displayMenuBar()
        {
            Console.Clear();
            if (isStart)
            {
                Console.WriteLine(_startMenuBar);
                isStart = false;
            }
            else
            {
                Console.WriteLine(_menuBar);
            }
            analayzeInput(Console.ReadLine());
        }
        private void analayzeInput(string action)
        {
            switch (action)
            {
                case "1":
                    //go to logic layer and register user
                    Console.WriteLine("register case");
                    displayMenuBar();
                    break;
                case "2":
                    Console.WriteLine("login case");
                    displayMenuBar();
                    break;
                case "3":
                    Console.WriteLine("Are u sure u want to exit?");
                    if (Console.ReadLine() == "Y")
                        // here we need to exit
                    displayMenuBar();
                    break;
                case "4":
                    Console.WriteLine("send case");
                    displayMenuBar();
                    break;
                case "5":
                    Console.WriteLine("send case");
                    displayMenuBar();
                    break;
                case "6":
                    Console.WriteLine("logout case");
                    isStart = true;
                    displayMenuBar();
                    break;
                default:
                    Console.WriteLine("Illegal Input");
                    displayMenuBar();
                    break;
            }

        }

    }
}
