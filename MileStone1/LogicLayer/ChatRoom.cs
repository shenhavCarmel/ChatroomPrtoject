using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStoneClient.CommunicationLayer;
using MileStone1.ConsistentLayer;

namespace MileStone1.LogicLayer
{
    // class is defined by singleton design pattern
    class ChatRoom
    {
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
            //_messages = _msgHandler.GetMessagesList();
            _messages = new List<Message>();
            //_registeredUsers = _msgHandler.GetUsersList();
            _registeredUsers = new List<User>();

            _loggedInUser = null;
        }

        public static ChatRoom Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ChatRoom();
                }

                return _instance;
            }
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

                return true;
            }
            else
                return false;
        }

        public Boolean Register(String nickname, string groupId)
        {
            User userToRegister = new User(nickname, groupId);

            if (!CheckIfUserExists(userToRegister))
            {
                _registeredUsers.Add(userToRegister);

                // register him to the chat room --> add to files
                //_userHandler.SaveUser(userToRegister);

                return true;
            }
            else
                return false;
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

        public void RetrieveMsg()
        {
            List<IMessage> retrievedMsg = Communication.Instance.GetTenMessages(_URL);

            // sort the new messages
            IMessage[] arrRetrievedMsg = retrievedMsg.ToArray();
            Array.Sort(arrRetrievedMsg, delegate (IMessage msg1, IMessage msg2) {
                return msg1.Date.CompareTo(msg2.Date);
            });

            // add new messages from the server to the list of messages
            foreach (IMessage currMsg in arrRetrievedMsg)
            {
                // check if the list of messages already contains message with the same guid
                if(!_messages.Exists(e => e.GetId().Equals(currMsg.Id)))
                {
                    _messages.Add(new Message(currMsg));
                }          
            }

            // add to presistent
            //_msgHandler.SaveMessageList(_messages);
        }

        public List<Message> DisplayLastMsg()
        {
            const int _DISPLAYED_MSG_AMOUNT = 20;

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
                // collect messages from a specific user
                IEnumerable<Message> results =
                     _messages.Where(currMsg => (currMsg.GetGroupID()).Equals(sender.GetGroupId()) &&
                                                (currMsg.GetUserName()).Equals(sender.GetNickname()));
                return (List<Message>)results;
            }
            else
                throw new ArgumentException("Error - user isn't registered to chatroom");

        }

        public string SendMessage(String body)
        {
            try
            {
                // send message content to server through user, save the returned object message
                Message newSentMessage = _loggedInUser.SendMessage(body, _URL);

                _messages.Add(newSentMessage);

                // save to persistent 
                _msgHandler.SaveMessage(newSentMessage);

                return "Message sent successfully";
            }
            catch (Exception excep)
            {
                return excep.Message;
            }
        }

        public void Logout()
        {
            // change the state of the user
            _loggedInUser.Logout();

            // disconnect him from the chatroom
            _loggedInUser = null;
        }

        // check if the end-user can exit (if he loged out first)
        public Boolean CanExit()
        {
            return _loggedInUser == null;
        }
    }
}
