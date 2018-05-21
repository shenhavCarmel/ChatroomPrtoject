using MileStone2.ConsistentLayer;
using MileStoneClient.CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone2.LogicLayer
{
    class ChatRoom
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // fields
        private static ChatRoom _instance = null;
        private User _loggedInUser;
        private const String _URL = "http://ise172.ise.bgu.ac.il:80";
        private List<Message> _messages;
        private List<User> _registeredUsers;
        private MessageHandler _msgHandler;
        private UserHandler _userHandler;

        // constructor
        private ChatRoom()
        {
            _msgHandler = new MessageHandler();
            _userHandler = new UserHandler();

            // load users' and messages' data from files
            _messages = _msgHandler.GetMessagesList().ToList();
            MsgSorter(this._messages);

            _registeredUsers = _userHandler.GetUsersList().ToList();

            _loggedInUser = null;
        }


        public Boolean Login(String nickname, string groupId)
        {
            User userToLogin = new User(nickname, groupId);

            if (CheckIfUserExists(userToLogin))
            {
                // change state of user
                userToLogin.Login();

                // connect him to the chat room
                this._loggedInUser = userToLogin;

                // logger
                log.Info("User logged in to the chatroom");
                return true;
            }
            else
            {
                // logger
                log.Error("A non registered user tried to login");

                return false;
            }

        }

        public Boolean Register(String nickname, string groupId)
        {
            User userToRegister = new User(nickname, groupId);

            if (!CheckIfUserExists(userToRegister))
            {
                _registeredUsers.Add(userToRegister);

                // register him to the chat room --> add to files
                _userHandler.SaveUser(userToRegister);

                // logger
                log.Info("A new user registered");
                return true;
            }
            else
            {
                // logger
                log.Error("An existing user tried to register again");

                return false;
            }

        }

        private Boolean CheckIfUserExists(User user)
        {
            // go over users list and check if the requested user exists
            foreach (User currUser in _registeredUsers)
            {
                if ((currUser.GetNickname()).Equals(user.GetNickname()) &&
                    (currUser.GetGroupId()).Equals(user.GetGroupId()))
                    return true;
            }
            return false;
        }

        public Boolean RetrieveMsg()
        {
            try
            {
                List<IMessage> retrievedMsg = Communication.Instance.GetTenMessages(_URL);

                // add new messages from the server to the list of messages
                foreach (IMessage currMsg in retrievedMsg)
                {
                    // check if the list of messages already contains message with the same guid
                    if (!_messages.Exists(e => e.GetId().Equals(currMsg.Id)))
                    {
                        _messages.Add(new Message(currMsg));
                    }
                }

                // sort the messages list 
                MsgSorter(this._messages);

                // add to presistent
                _msgHandler.SaveMessageList(_messages);

                // logger
                log.Info("Chatroom retrieved 10 messages from server");

                return true;
            }
            catch
            {
                // logger
                log.Error("Retrieval messages from sercer failed");

                return false;
            }

        }

        private void MsgSorter(List<Message> msgList)
        {
            // sort the new messages
            Message[] arrMsg = msgList.ToArray();
            Array.Sort(arrMsg, delegate (Message msg1, Message msg2) {
                return msg1.GetDate().CompareTo(msg2.GetDate());
            });

            // logger
            log.Info("Quick Messages List was sorted");

            msgList = arrMsg.ToList();
        }

        public List<Message> DisplayLastMsg()
        {
            const int _DISPLAYED_MSG_AMOUNT = 20;

            // logger
            log.Info("User displaying 20 last messages");

            // check the amount of messages
            if (_messages.Count() < _DISPLAYED_MSG_AMOUNT)
            {
                return _messages;
            }
            else
            {
                int startIndex = _messages.Count() - _DISPLAYED_MSG_AMOUNT;
                return _messages.GetRange(startIndex, _DISPLAYED_MSG_AMOUNT);
            }
        }

        public List<Message> DisplayMsgByUser(string nickname, string groupId)
        {
            User sender = new User(nickname, groupId);

            if (CheckIfUserExists(sender))
            {
                // logger
                log.Info("User displaying messages sent by a specific user");

                // collect messages from a specific user
                IEnumerable<Message> results =
                     _messages.Where(currMsg => (currMsg.GetGroupID()).Equals(groupId) &&
                                                (currMsg.GetUserName()).Equals(nickname));

                // into list
                List<Message> msgFromUser = new List<Message>();
                foreach (Message currMsg in results)
                {
                    msgFromUser.Add(currMsg);
                }
                return msgFromUser;
            }
            else
            {
                // logger
                log.Error("The user tried to display messages from a non-registered user");

                throw new ArgumentException("Error - user isn't registered to chatroom");
            }
        }

        public Boolean SendMessage(String body)
        {
            try
            {
                // send message content to server through user, save the returned object message
                Message newSentMessage = _loggedInUser.SendMessage(body, _URL);

                // save to persistent 
                _msgHandler.SaveMessage(newSentMessage);

                // logger
                log.Info("User sent a new message");

                return true;
            }
            catch
            {
                // logger
                log.Error("User tried to send a message, sending process failed");

                return false;
            }
        }

        public void Logout()
        {
            // change the state of the user
            _loggedInUser.Logout();

            // disconnect him from the chatroom
            _loggedInUser = null;

            // logger
            log.Info("User logged out");
        }

        // check if the end-user can exit (if he loged out first)
        public Boolean CanExit()
        {
            return _loggedInUser == null;
        }
    }
}
