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
        private string _startMenuBar = "~Group25 ChatRoom~" + "\r\n" + "1- Register" + "\r\n" + "2- Login" +
                                "\r\n" + "3- Exit";
        private string _menuBar = "~Group25 ChatRoom~" + "\r\n" + "4- Send message" + "\r\n" + "5- View last messages" +
                            "\r\n" + "6- View messages written by a certain user" + "\r\n" + "7- Retrieve messages from server" + "\r\n" + "8- logout" + "\r\n";
        private Boolean DisplayFirstMenu;

        public GUI()
        {
            Console.WriteLine(_startMenuBar);
            DisplayFirstMenu = false;
            AnalayzeInput(Console.ReadLine());
        }

        public void DisplayMenuBars()
        {
            Console.Clear();
            if (DisplayFirstMenu)
            {
                Console.WriteLine(_startMenuBar);
                DisplayFirstMenu = false;
            }
            else
            {
                Console.WriteLine(_menuBar);
            }
            AnalayzeInput(Console.ReadLine());
        }

        private void AnalayzeInput(string action)
        {
            switch (action)
            {
                case "1":
                    RegisterCase();
                    break;

                case "2":
                    LoginCase();
                    break;

                case "3":
                    ExitCase();
                    break;

                case "4":
                    SendCase();
                    break;

                case "5":
                    DisplayLastMsgsCase();
                    break;

                case "6":
                    DisplayMsgsFromUser();
                    break;

                case "7":
                    RetrieveFromServer();
                    break;

                case "8":
                    LogOutCase();
                    break;

                default:
                    Console.WriteLine("Illegal Input");
                    DisplayMenuBars();
                    break;
            }
        }


        private void RegisterCase()
        {
            //go to logic layer and register user
            Console.WriteLine("Register:");
            Console.WriteLine("Please enter your group ID");

            string strGroupID = Console.ReadLine();
            strGroupID = CheckgroupId(strGroupID);

            if (strGroupID.Equals("Y"))
            {
                DisplayFirstMenu = true;
                DisplayMenuBars();
            }

            Console.WriteLine("Enter your nickname");

            string nickname = Console.ReadLine();

            if (LogicLayer.ChatRoom.Instance.Register(nickname, strGroupID))
            {
                Console.WriteLine("Registered succesfully");
                DisplayFirstMenu = false;
                DisplayMenuBars();
            }
            else
            {
                Console.WriteLine("Registered failed. This nickname is already taken.");
                while (!LogicLayer.ChatRoom.Instance.Register(nickname, strGroupID) && !nickname.Equals("Y"))
                {
                    Console.WriteLine("Invalid Input");
                    // TODO: write the error to the logger
                    Console.WriteLine("Re-enter your nickname. If you want to return to the menu bar press 'Y':");
                    nickname = Console.ReadLine();
                }
                if (nickname.Equals("Y"))
                {
                    DisplayFirstMenu = true;
                    DisplayMenuBars();
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            DisplayMenuBars();

        }

        private void LoginCase()
        {
            Console.WriteLine("Login:");
            Console.WriteLine("Please enter your groupId");

            String strGroupID = Console.ReadLine();
            strGroupID = CheckgroupId(strGroupID); 

            if (strGroupID.Equals("Y"))
            {
                DisplayFirstMenu = true;
                DisplayMenuBars();
            }

            Console.WriteLine("Enter your nickname");
            String nickname = Console.ReadLine();

            if (LogicLayer.ChatRoom.Instance.Login(nickname, strGroupID))
            {
                Console.WriteLine("Logged In successfully");
                DisplayFirstMenu = false;
                DisplayMenuBars();
            }
            else
            {
                Console.WriteLine("Log In failed. This nickname doesn't exist.");
                while (!LogicLayer.ChatRoom.Instance.Login(nickname, strGroupID) && !nickname.Equals("Y"))
                {
                    Console.WriteLine("Invalid Input");
                    // TODO: write the error to the logger
                    Console.WriteLine("Re-enter your nickname. If you want to return to the menu bar, press 'Y':");
                    nickname = Console.ReadLine();
                }
                if (nickname.Equals("Y"))
                {
                    DisplayFirstMenu = true;
                    DisplayMenuBars();
                }

            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            DisplayMenuBars();
        }

        private void ExitCase()
        {
            Console.WriteLine("If you sure you want to exit, please press 'Y'");
            if (Console.ReadLine().Equals("Y"))
            {
                if (LogicLayer.ChatRoom.Instance.CanExit())
                {
                    Console.Clear();
                    Console.WriteLine("Good bye");
                    // TODO closing stuff
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            DisplayMenuBars();
        }

        private void SendCase()
        {
            Console.WriteLine("Please enter your message:");
            Console.WriteLine(LogicLayer.ChatRoom.Instance.SendMessage(Console.ReadLine()));
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            DisplayMenuBars();
        }

        private void DisplayLastMsgsCase()
        {
            foreach (Message currMsg in LogicLayer.ChatRoom.Instance.DisplayLastMsg())
            {
                Console.WriteLine(currMsg.toString());
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            DisplayMenuBars();
        }

        private void DisplayMsgsFromUser()
        {
            Console.WriteLine("Please enter user's nickname:");
            String nickname = Console.ReadLine();
            Console.WriteLine("Please enter user's group id:");
            String groupId = Console.ReadLine();
            try
            {
                foreach (Message curr in LogicLayer.ChatRoom.Instance.DisplayMsgByUser(nickname, groupId))
                {
                    Console.WriteLine(curr.toString());
                }
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                DisplayMenuBars();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DisplayMenuBars();
            }
        }

        private void RetrieveFromServer()
        {
            LogicLayer.ChatRoom.Instance.RetrieveMsg();
            Console.WriteLine("Retrieved messages from server successfuly");
            Console.WriteLine("Please press enter to continue");
            Console.ReadLine();
            DisplayMenuBars();
        }

        private void LogOutCase()
        {
            Console.WriteLine("Logout:");
            Console.WriteLine("If you sure you want to logout, please press 'Y':");
            if (Console.ReadLine().Equals("Y"))
            {
                LogicLayer.ChatRoom.Instance.Logout();
                DisplayFirstMenu = true;
                Console.WriteLine("Logged out successfuly");
                Console.WriteLine("Please press enter to continue");
                Console.ReadLine();
            } 
            DisplayMenuBars();
        }

        private string CheckgroupId(string strGroupId)
        {
            int groupId;
            while (!int.TryParse(strGroupId, out groupId) && !strGroupId.Equals("Y"))
            {
                Console.WriteLine("Invalid Input.");
                // TODO: write the error to the logger
                Console.WriteLine("Re-enter your group ID. If you want to return to the menu press 'Y':");
                strGroupId = Console.ReadLine();
            }
            return strGroupId;
        }
    }
}
