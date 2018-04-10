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
        private string _startMenuBar = "~ChatRoom~" + "\r\n" + "1- Register" + "\r\n" + "2- Login" +
                                "\r\n" + "3- Exit";
        private string _menuBar = "~ChatRoom~" + "\r\n" + "4- Send message" + "\r\n" + "5- View last messages" +
                            "\r\n" + "6- View messages written by a certain user" + "\r\n" + "7- Retrieve messages from server" + "\r\n" + "8- logout" + "\r\n";
        private Boolean _displayFirstMenu;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public GUI()
        {
            Console.WriteLine(_startMenuBar);
            _displayFirstMenu = false;
            AnalayzeInput(Console.ReadLine());
        }

        public void DisplayMenuBars()
        {
            Console.Clear();
            if (_displayFirstMenu)
            {
                Console.WriteLine(_startMenuBar);
                _displayFirstMenu = false;
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
                    if (!_displayFirstMenu)
                    {
                        SendCase();
                    }
                    else
                    {
                        Console.WriteLine("Illegal Input");
                        _displayFirstMenu = true;
                        DisplayMenuBars();
                    }    
                    break;

                case "5":
                    if (!_displayFirstMenu)
                    {
                        DisplayLastMsgsCase();
                    }
                    else
                    {
                        Console.WriteLine("Illegal Input");
                        _displayFirstMenu = true;
                        DisplayMenuBars();
                    }    
                    break;

                case "6":
                    if (!_displayFirstMenu)
                    {
                        DisplayMsgsFromUser();
                    }
                    else
                    {
                        Console.WriteLine("Illegal Input");
                        _displayFirstMenu = true;
                        DisplayMenuBars();
                    }
                    break;

                case "7":
                    if (!_displayFirstMenu)
                    {
                        RetrieveFromServer();
                    }
                    else
                    {
                        Console.WriteLine("Illegal Input");
                        _displayFirstMenu = true;
                        DisplayMenuBars();
                    }
                    break;

                case "8":
                    if (!_displayFirstMenu)
                    {
                        LogOutCase();
                    }
                    else
                    {
                        Console.WriteLine("Illegal Input");
                        _displayFirstMenu = true;
                        DisplayMenuBars();
                    }
                    break;

                default:
                    Console.WriteLine("Illegal Input");
                    _displayFirstMenu = true;
                    DisplayMenuBars();
                    break;
            }
        }


        private void RegisterCase()
        {
            Console.Clear();
            Console.WriteLine("Register:");
            Console.WriteLine("To return to the menu bar, press 'Y'");
            Console.WriteLine("Please enter your group ID");

            string strGroupID = Console.ReadLine();
            strGroupID = CheckgroupId(strGroupID);


            if (strGroupID.Equals("Y"))
            {
                _displayFirstMenu = true;
                DisplayMenuBars();
            }

            Console.WriteLine("Please enter your nickname");

            string nickname = Console.ReadLine();

            if (ChatRoom.Instance.Register(nickname, strGroupID))
            {
                Console.Clear();
                Console.WriteLine("Registered succesfully");
                Console.WriteLine("Please press enter to continue");
                Console.ReadLine();
                _displayFirstMenu = true;
                DisplayMenuBars();
            }
            else
            {
                Console.WriteLine("Registered failed. This nickname is already taken.");
                while (!LogicLayer.ChatRoom.Instance.Register(nickname, strGroupID) && !nickname.Equals("Y"))
                {
                    log.Info("Registered failed-wrong nickname/groupID");
                    Console.WriteLine("Re-enter your nickname. If you want to return to the menu bar press 'Y':");
                    nickname = Console.ReadLine();
                }
                if (nickname.Equals("Y"))
                {
                    _displayFirstMenu = true;
                    DisplayMenuBars();
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();

            DisplayMenuBars();

        }

        private void LoginCase()
        {
            Boolean isY = false;
            Console.Clear();
            Console.WriteLine("Login:");
            Console.WriteLine("To return to the menu bar, press 'Y'");

            Console.WriteLine("Please enter your groupId:");
            String strGroupID = Console.ReadLine();

            isY = strGroupID.Equals("Y");

            if (!isY)
            {
                Console.WriteLine("Please enter your nickname:");
                String nickname = Console.ReadLine();
                isY = nickname.Equals("Y");

                if (!isY)
                {
                    if (ChatRoom.Instance.Login(nickname, strGroupID))
                    {
                        Console.WriteLine("Logged In successfully");
                        _displayFirstMenu = false;
                        DisplayMenuBars();
                    }
                    else
                    {
                        log.Info("the user trying to login isn't registered.");
                        Console.Clear();
                        Console.WriteLine("Log In failed. This user doesn't exist.");
                        Console.WriteLine("To return to the menu bar, press 'Y'. To try again enter '2'");
                        string input = Console.ReadLine();
                        if (input.Equals(2))
                            LoginCase();
                    }
                }
            }
            if (isY)
            {
                _displayFirstMenu = true;
                DisplayMenuBars();
            }
            
        }

        private void ExitCase()
        {
            Console.WriteLine("If you sure you want to exit, please press 'Y'");
            if (Console.ReadLine().Equals("Y"))
            {
                if (ChatRoom.Instance.CanExit())
                {
                    Console.Clear();
                    Console.WriteLine("Good bye");
 //                   Console.sleep(5000);
                    Environment.Exit(0);
                }
            }
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
            foreach (Message currMsg in ChatRoom.Instance.DisplayLastMsg())
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
                foreach (Message curr in ChatRoom.Instance.DisplayMsgByUser(nickname, groupId))
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
            ChatRoom.Instance.RetrieveMsg();
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
                ChatRoom.Instance.Logout();
                _displayFirstMenu = true;
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
