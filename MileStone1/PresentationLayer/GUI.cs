using MileStone1.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MileStone1.PresentationLayer
{
   
    class GUI
    {

        // Fields
        private string _startMenuBar = "~ChatRoom~" + "\r\n" + "1- Register" + "\r\n" + "2- Login" +
                                "\r\n" + "3- Exit";
        private string _menuBar = "~ChatRoom~" + "\r\n" + "4- Send message" + "\r\n" + "5- View last messages" +
                            "\r\n" + "6- View messages written by a certain user" + "\r\n" + "7- Retrieve messages from server" + "\r\n" + "8- logout" + "\r\n";
        private Boolean _displayFirstMenu;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // Constructors
        public GUI()
        {
            // logger
            log.Info("Begginig of the log");

            Console.WriteLine(_startMenuBar);
            _displayFirstMenu = true;
            AnalayzeInput(Console.ReadLine());
        }

        public void DisplayMenuBars()
        {
            Console.Clear();
            if (_displayFirstMenu)
            {
                Console.WriteLine(_startMenuBar);
            }
            else
            {
                Console.WriteLine(_menuBar);
            }
            AnalayzeInput(Console.ReadLine());
        }

        // Analayzing input fron user, and implements it accordingly
        private void AnalayzeInput(string action)
        {
            switch (action)
            {

                case "1":
                    if (_displayFirstMenu)
                    {
                        RegisterCase();
                    }
                    else
                    {
                        InvalidCase();
                    }
                    break;

                case "2":
                    if (_displayFirstMenu)
                    {
                        LoginCase();
                    }
                    else
                    {
                        InvalidCase();
                    }
                    break;

                case "3":
                    if (_displayFirstMenu)
                    {
                        ExitCase();
                    }
                    else
                    {
                        InvalidCase();
                    }
                    break;

                case "4":
                    if (!_displayFirstMenu)
                    {
                        SendCase();
                    }
                    else
                    {
                        InvalidCase();
                    }    
                    break;

                case "5":
                    if (!_displayFirstMenu)
                    {
                        DisplayLastMsgsCase();
                    }
                    else
                    {
                        InvalidCase();
                    }    
                    break;

                case "6":
                    if (!_displayFirstMenu)
                    {
                        DisplayMsgsFromUser();
                    }
                    else
                    {
                        InvalidCase();
                    }
                    break;

                case "7":
                    if (!_displayFirstMenu)
                    {
                        RetrieveFromServer();
                    }
                    else
                    {
                        InvalidCase();
                    }
                    break;

                case "8":
                    if (!_displayFirstMenu)
                    {
                        LogOutCase();
                    }
                    else
                    {
                        InvalidCase();
                    }
                    break;

                default:
                    InvalidCase();
                    break;
            }
        }

        // If the user's input is invalid
        private void InvalidCase()
        {
            // logger
            log.Error("The user tried to enter illegal input of the menu");

            Console.Clear();
            Console.WriteLine("Illegal Input");
            Console.WriteLine("Press any kye to continue");
            Console.ReadLine();
            DisplayMenuBars();
        }


        private void RegisterCase()
        {
            Console.Clear();
            Console.WriteLine("Register:");
            Console.WriteLine("To return to the menu bar, press 'Y'");
            Console.WriteLine("Please enter your group ID");

            string strGroupID = Console.ReadLine();

            // check if the groupID already exists
            strGroupID = CheckgroupId(strGroupID);

            // gives an option to stop the regiter stage and return to the menu bar
            if (strGroupID.Equals("Y"))
            {
                _displayFirstMenu = true;
                DisplayMenuBars();
            }

            Console.WriteLine("Please enter your nickname");

            string nickname = Console.ReadLine();

            // check if the user alredy exists
            if (ChatRoom.Instance.Register(nickname, strGroupID))
            {
                Console.Clear();
                Console.WriteLine("Registered succesfully");
                Console.WriteLine("Press any kye to continue");
                Console.ReadLine();
                _displayFirstMenu = true;
                DisplayMenuBars();
            }
            else
            {
                // until the input isn't valid and new, keep registering
                // can exit if press 'Y'
                while (!LogicLayer.ChatRoom.Instance.Register(nickname, strGroupID) && !nickname.Equals("Y"))
                {
                    log.Info("Registered failed-wrong nickname/groupID");
                    Console.WriteLine("Registered failed. This nickname is already taken." + "\r\n" + "Re-enter your nickname. If you want to return to the menu bar press 'Y':");
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

            // check if the user wants to return to the first menu
            if (!isY)
            {
                Console.WriteLine("Please enter your nickname:");
                String nickname = Console.ReadLine();
                isY = nickname.Equals("Y");

                // check if the user wants to return to the first menu
                if (!isY)
                {
                    if (ChatRoom.Instance.Login(nickname, strGroupID))
                    {
                        Console.WriteLine("Logged In successfully");
                        _displayFirstMenu = false;
                        DisplayMenuBars();
                    }

                    // the user doesn't exist
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Log In failed. This user doesn't exist.");
                        Console.WriteLine("To return to the menu bar, press 'Y'. To try again enter '2'");
                        string input = Console.ReadLine();

                        // user wants to try login again 
                        if (input.Equals("2"))
                        {
                            LoginCase();
                        }   
                        else
                        {
                            while (!input.Equals(2))
                            {
                                Console.Clear();
                                Console.WriteLine("To return to the menu bar, press 'Y'. To try again enter '2'");
                                input = Console.ReadLine();
                                if (input.Equals("Y"))
                                {
                                    _displayFirstMenu = true;
                                    DisplayMenuBars();
                                }    
                            } 
                        }
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
                    // logger
                    log.Info("The logged in user asked to Exit the chatRoom");

                    Console.Clear();
                    Console.WriteLine("Good bye");
                   
                    // Suspend exit
                    Timer t = new Timer(Exit, null, 1000, 0);
                    Console.ReadLine();
                }
            }
            else
            {
                Console.Clear();
                DisplayMenuBars();
            }
        }

        private void SendCase()
        {
            Console.WriteLine("Please enter your message:");
            String msgBody = Console.ReadLine();

            // check if the the content is over 150 characters - illegal
            if (msgBody.Length > 150 | msgBody.Length == 0)
            {
                Console.WriteLine("Sending failed, message can't be over 150 characters or empty");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();

                // logger
                log.Error("User tried to send a message over 150 characters");
            }

            // valid message, send
            else
            {
                if (ChatRoom.Instance.SendMessage(msgBody))
                {
                    Console.WriteLine("Message sent successfully");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Message wasn't sent, please try again");
                }
            }
            DisplayMenuBars();
        }

        private void DisplayLastMsgsCase()
        {
            // get last 20 messages that was retrieved from the server
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
                // get last 20 messages that was retrieved from the server from a specific user
                foreach (Message curr in ChatRoom.Instance.DisplayMsgByUser(nickname, groupId))
                {
                    Console.WriteLine(curr.toString());
                }

                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                DisplayMenuBars();
            }

            // the requested user isn't registered
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Please press enter to continue");
                Console.ReadLine();
                DisplayMenuBars();
            }
        }

        private void RetrieveFromServer()
        {
            // retrieve messages from server, check if the process succeded
            if (ChatRoom.Instance.RetrieveMsg())
            {
                Console.WriteLine("Retrieved messages from server successfuly");
            }
            else
            {
                Console.WriteLine("Something went wrong with the process");
            }
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
                Console.WriteLine("Press any kye to continue");
                Console.ReadLine();
            } 
            DisplayMenuBars();
        }

        private string CheckgroupId(string strGroupId)
        {
            int groupId;
            while (!int.TryParse(strGroupId, out groupId) && !strGroupId.Equals("Y"))
            {
                // logger
                log.Error("User tried to enter a non numeric input for groupID value");

                Console.WriteLine("Invalid Input.");
                Console.WriteLine("Re-enter your group ID. If you want to return to the menu press 'Y':");
                strGroupId = Console.ReadLine();
            }
            return strGroupId;
        }

        private void Exit(Object state)
        {
            Environment.Exit(0);
        }
    }
}
