using MileStone1.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone1.PresentationLayer
{
    class GUI
    {
        private string _startMenuBar= "~Group25 ChatRoom~" + "\r\n" + "1- Register" + "\r\n" + "2- Login" + 
                                "\r\n" + "3- Exit";
        private string _menuBar = "~Group25 ChatRoom~" + "\r\n" + "4- Send Message" + "\r\n" + "5- view" + 
                            "\r\n" + "6- logout" + "\r\n";
        private Boolean isStart;

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
                    Console.WriteLine("register:");
                    Console.WriteLine("Enter your group ID");
              
                    string strGroupID = Console.ReadLine();

                    int groupId = checkgroupId(strGroupID);

                    if (groupId.Equals(-1))
                    {
                        isStart = true;
                        displayMenuBar();
                    }

                    Console.WriteLine("Enter your nickname");

                    string nickname = Console.ReadLine();

                    if (ChatRoom.Instance.register(nickname, groupId))
                    {
                        Console.WriteLine("Registered succesfully");
                        isStart = false;
                        displayMenuBar();
                    }
                    else
                    {
                        Console.WriteLine("Registered failed. This nickname is already taken.");
                        while (!ChatRoom.Instance.register(nickname, groupId) && !nickname.Equals("Y"))
                        {
                            Console.WriteLine("Invalid Input.");
                            // TODO: write the error to the logger
                            Console.WriteLine("Re-enter your nickname. If you want to return to the menu bar press 'Y':");
                            nickname = Console.ReadLine();
                        }
                        if (nickname.Equals("Y"))
                        {
                            isStart = true;
                            displayMenuBar();
                        }

                    }
                    displayMenuBar();
                    break;

                case "2":
                    Console.WriteLine("login case");
                    Console.WriteLine("Enter your groupId");

                    strGroupID = Console.ReadLine();
                    groupId = checkgroupId(strGroupID);

                    if (groupId.Equals(-1))
                    {
                        isStart = true;
                        displayMenuBar();
                    }

                    Console.WriteLine("Enter your nickname");
                    nickname= Console.ReadLine();

                    if (ChatRoom.Instance.login(nickname, groupId))
                    {
                        Console.WriteLine("Logged In seccessfully");
                        isStart = false;
                        displayMenuBar();
                    }
                    else
                    {
                        Console.WriteLine("Log In failed. This nickname isn't exist.");
                        while (!ChatRoom.Instance.login(nickname, groupId) && !nickname.Equals("Y"))
                        {
                            Console.WriteLine("Invalid Input.");
                            // TODO: write the error to the logger
                            Console.WriteLine("Re-enter your nickname. If you want to return to the menu bar press 'Y':");
                            nickname = Console.ReadLine();
                        }
                        if (nickname.Equals("Y"))
                        {
                            isStart = true;
                            displayMenuBar();
                        }

                    }

                    //find user by nickname 
                    User us.loggedIn();
                    displayMenuBar();
                    break;

                case "3":
                    Console.WriteLine("Are u sure u want to exit?");
                    if (Console.ReadLine() == "Y")
                    {
                        // find user 
                        user.loggedOut();
                        //Exit chatroom
                    }
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
                    //find user
                    user.loggedOut();
                    isStart = true;
                    displayMenuBar();
                    break;
                default:
                    Console.WriteLine("Illegal Input");
                    displayMenuBar();
                    break;
            }

        }

        private int checkgroupId(string strGroupId)
        {
            int groupId;
            while (!int.TryParse(strGroupId, out groupId) && !strGroupId.Equals("Y"))
            {
                Console.WriteLine("Invalid Input.");
                // TODO: write the error to the logger
                Console.WriteLine("Re-enter your group ID. If you want to return to the menu for yes press 'Y':");
                strGroupId = Console.ReadLine();
            }
            if (strGroupId.Equals("Y"))
            {
                return -1;
            }
            return groupId;
        }
}
