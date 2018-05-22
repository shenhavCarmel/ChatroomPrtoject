using MileStone2.ConsistentLayer;
using MileStone2.PresentationLayer;
using MileStoneClient.CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone2.LogicLayer
{
    public class ChatRoom
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // fields
        private User _loggedInUser;
        private const String _URL = "http://ise172.ise.bgu.ac.il:80";
        private List<Message> _messages;
        private List<Message> _currDisplayedMsgs;
        public static List<User> _registeredUsers;
        private MessageHandler _msgHandler;
        private UserHandler _userHandler;
        private Sorter _sorter;
        private Filter _filter;
        public ActionListener _chatBinder;

        // constructor
        public ChatRoom(ActionListener binder)
        {
            _msgHandler = new MessageHandler();
            _userHandler = new UserHandler();

            // set the object observer
            _chatBinder = binder;

            // set sorter and filter
            _sorter = new Sorter(_chatBinder.SelectedModeAscending, _chatBinder.SelectedTypeSorterIndex);
            _filter = new Filter(_chatBinder.SelectedTypeFilterIndex);

            // load users' and messages' data from files
            _messages = _msgHandler.GetMessagesList();
            _currDisplayedMsgs = new List<Message>();
            _currDisplayedMsgs.AddRange(_messages);

            _registeredUsers = _userHandler.GetUsersList();

            _loggedInUser = null;
        }

        public List<User> GetRegisteredUsers()
        {
            return _registeredUsers;
        }
        public List<Message> GetMessagesInChat()
        {
            return Sort();
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
            // check if arguments are valid
            if ((nickname == null || nickname.Equals("")) | (groupId == null || groupId.Equals("")))
            {
                return false;
            }
            else
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
        }

        public Filter GetFilter()
        {
            return this._filter;
        }
        public Sorter GetSorter()
        {
            return _sorter;
        }

        public Boolean CheckIfUserExists(User user)
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
                    int groupID;
                    Message msg = new Message(currMsg);
                    try
                    {
                        groupID = int.Parse(msg.GetGroupID());

                        // check if the list of messages already contains message with the same guid
                        if (!(_messages.Exists(e => e.GetId().Equals(currMsg.Id))))
                        {
                            _messages.Add(msg);
                        }

                        // check if the users from the messages are in the registered users list
                        if ((_registeredUsers == null) || (!(_registeredUsers.Exists(e => e.GetNickname().Equals(msg.GetUserName()) &
                                                            !(_registeredUsers.Exists(x => x.GetGroupId().Equals(msg.GetGroupID())))))))
                        {
                            // add the existing users
                            User newUser = new User(msg.GetUserName(), msg.GetGroupID());
                            _userHandler.SaveUser(newUser);
                            _registeredUsers.Add(newUser);
                        }
                    }
                    catch
                    {

                    }

                }

                // add to presistent
                _msgHandler.SaveMessageList(_messages);

                List<Message> selectedMsgs = new List<Message>();
                try
                {
                    // filter & sort the relevant messages
                    selectedMsgs.AddRange(_filter.runFilter(_messages, _registeredUsers,
                                                        _chatBinder.FilterGroupId, _chatBinder.FilterNickname));
                    _currDisplayedMsgs = _sorter.runSort(selectedMsgs);
                }
                catch (Exception e)
                {
                    throw e;
                }

                // logger
                log.Info("Chatroom retrieved 10 messages from server");

                return true;
            }
            catch
            {
                // logger
                log.Error("Retrieval messages from server failed");

                return false;
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

        public List<Message> Sort()
        {
            // sort the relevant messages according to settings
            _sorter.setSortingType(_chatBinder.SelectedTypeSorterIndex);
            _sorter.setAscending(_chatBinder.SorterMode[0]);

            // logger
            log.Info("Messages in chat room where sorted by user preferences");
            return _sorter.runSort(_currDisplayedMsgs);
        }

        public void FilterMsgs()
        {
            // set filter type
            _filter.setFilteringType(_chatBinder.SelectedTypeFilterIndex);
            try
            {
                // run the filter
                _currDisplayedMsgs = _filter.runFilter(_messages, _registeredUsers,
                                         _chatBinder.FilterGroupId, _chatBinder.FilterNickname);

                // logger
                log.Info("Filtering process succeed");
            }
            catch (Exception e)
            {
                // logger
                log.Error("Filtering process failed- " + e.Message);
                throw e;
            }
        }

        public class Sorter
        {
            private Boolean _ascending;
            private const int TIME_STAMP = 0;
            private const int NICKNAME = 1;
            private const int USER_TIMESTAMP = 2;
            private int _sortingType;

            public Sorter(Boolean isAscending, int sortType)
            {
                _ascending = isAscending;
                _sortingType = sortType;
            }

            public void setAscending(Boolean isAscending)
            {
                _ascending = isAscending;
            }

            public void setSortingType(int sortingType)
            {
                _sortingType = sortingType;
            }

            public List<Message> runSort(List<Message> listToSort)
            {
                List<Message> msgs = new List<Message>();

                // execute sort according to settings (type and mode)
                switch (_sortingType)
                {
                    case TIME_STAMP:
                        if (_ascending)
                        {
                            msgs = listToSort.OrderBy(x => x.GetDate()).ToList();
                        }
                        else
                        {
                            msgs = listToSort.OrderByDescending(x => x.GetDate()).ToList();
                        }
                        break;

                    case NICKNAME:
                        if (_ascending)
                        {
                            msgs = listToSort.OrderBy(x => x.GetUserName()).ToList();
                        }
                        else
                        {
                            msgs = listToSort.OrderByDescending(x => x.GetUserName()).ToList();
                        }
                        break;

                    case USER_TIMESTAMP:
                        if (_ascending)
                        {
                            msgs = listToSort.OrderBy(x => Int32.Parse(x.GetGroupID())).
                                ThenBy(x => x.GetUserName()).ThenBy(x => x.GetDate()).ToList();

                        }
                        else
                        {
                            msgs = listToSort.OrderByDescending(x => x.GetGroupID()).
                                ThenByDescending(x => x.GetUserName()).ThenByDescending(x => x.GetDate()).ToList();
                        }
                        break;

                    default:
                        break;
                }
                return msgs;
            }
        }
        public class Filter
        {
            private const int GROUP = 0;
            private const int USER = 1;
            private const int NONE = 2;
            private int _filteringType;

            public Filter(int filteringType)
            {
                _filteringType = filteringType;
            }

            public void setFilteringType(int filteringType)
            {
                _filteringType = filteringType;
            }

            public List<Message> runFilter(List<Message> listToFilter, List<User> registeredUsers,
                                            String userGroupId, String userNickname)
            {
                List<Message> msg = new List<Message>();

                // execute filter according to settings (type)
                switch (_filteringType)
                {
                    case GROUP:
                        try
                        {
                            msg = DisplayMsgByGroupId(userGroupId, registeredUsers, listToFilter);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        break;

                    case USER:
                        try
                        {
                            msg = DisplayMsgByUser(userNickname, userGroupId, listToFilter, registeredUsers);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        break;

                    case NONE:
                        msg = listToFilter;
                        break;

                    default:
                        break;
                }
                return msg;
            }

            public List<Message> DisplayMsgByGroupId(String groupId, List<User> registeredUsers, List<Message> messages)
            {

                if (!groupId.Equals(""))
                {

                    int group;

                    // check if the group id inserted is legal
                    if (int.TryParse(groupId, out group))
                    {

                        if (registeredUsers.Any((user) => user.GetGroupId() == groupId))
                        {
                            List<Message> msgFromGroup = new List<Message>();

                            foreach (Message currMsg in messages)
                            {
                                if (currMsg.GetGroupID().Equals(groupId))
                                    msgFromGroup.Add(currMsg);
                            }

                            return msgFromGroup;
                        }
                        else
                            throw new ArgumentException("Group ID is illegal - group isn't registered");
                    }
                    else
                        throw new ArgumentException("Group ID is illegal - only numbers are allowed");
                }
                return messages;
            }

            private List<Message> DisplayMsgByUser(string nickname, string groupId, List<Message> messages, List<User> registeredUsers)
            {
                if (!nickname.Equals("") && !groupId.Equals(""))
                {
                    int groupIn;
                    if (int.TryParse(groupId, out groupIn))
                    {
                        User sender = new User(nickname, groupId);

                        if (CheckIfUserExists(sender, registeredUsers))
                        {

                            // logger
                            log.Info("User displaying messages sent by a specific user");

                            // collect messages from a specific user
                            IEnumerable<Message> results =
                                 messages.Where(currMsg => (currMsg.GetGroupID()).Equals(groupId) &&
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
                    else
                        throw new ArgumentException("Illegal groupID - only numbers allowed");
                }
                else
                    throw new ArgumentException("Don't forget to enter all user details");
            }

            private Boolean CheckIfUserExists(User user, List<User> registeredUsers)
            {
                // go over users list and check if the requested user exists
                foreach (User currUser in registeredUsers)
                {
                    if ((currUser.GetNickname()).Equals(user.GetNickname()) &&
                        (currUser.GetGroupId()).Equals(user.GetGroupId()))
                        return true;
                }
                return false;
            }

        }
    }
}

