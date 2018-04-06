using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStone1.CommunicationLayer;

namespace MileStone1.LogicLayer
{
    // class is defined by singleton design pattern
    class ChatRoom
    {
        // fields
        private static ChatRoom _instance = null;
        private User _loggedInUser;
        private const String _URL= "http://ise172.ise.bgu.ac.il:80";
        private List<Message> _messages;

        // constructor
        private ChatRoom()
        {
            _messages = new List<Message>();
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

        public Boolean Login(String nickname, int groupId)
        {
            Boolean loginPass = false;
            User userLoggedIn = new User(nickname, groupId);

            // TODO: check if the user exists by groupId and nickname (in files?)- loginPass = true

            if (loginPass)
            {
                // change state of user
                User.Login();

                // connect him to the chat room
                this._loggedInUser = userLoggedIn;
            }
           
            return loginPass;
        }

        public Boolean Register(String nickname, int groupId)
        {
            Boolean registerPass = false;

            // TODO: check if the user doesn't exist by nickname (in files?)- registerPass = true

            if (registerPass)
            {
                // TODO: register him to the chat room --> add to files
                
                return Login(nickname, groupId);
            }

            return registerPass;
        }

        public Message[] RetrieveMsg()
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
                if (!_messages.Contains(currMsg))
                {
                    _messages.Add((Message)currMsg);
                }
            } 

            return (Message[])arrRetrievedMsg;
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

        public List<Message> DisplayMsgByUser(User sender)
        {
            // collect nessages from a specific user
            IEnumerable<Message> results =
                 _messages.Where(currMsg => currMsg.getGroupID == sender.getGroupID() &&
                                            currMsg.getUserName == sender.getUserName());
            return (List<Message>)results;
        }

        public void sendMessage(String msg)
        {
            // TODO: deal with the returned value of the sending nsg to server action
            _loggedInUser.sendMessage(msg);
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
