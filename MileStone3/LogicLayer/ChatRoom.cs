using MileStone3.PresentationLayer;
using MileStone3.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone3.LogicLayer
{
    public class ChatRoom
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // fields
        private User _loggedInUser;
        private const String _URL = "http://ise172.ise.bgu.ac.il:80";
        private QueryRunner _queryRunner;
        private List<Message> _lastMsgsInChat;
        private List<Message> _displayedMsgs;
        private Sorter _sorter;
        private Filter _filter;
        public ActionListener _chatBinder;

        // constructor
        public ChatRoom(ActionListener binder)
        {
   
            _queryRunner = new QueryRunner();

            // set the object observer
            _chatBinder = binder;

            // set sorter and filter
            _sorter = new Sorter(_chatBinder.SelectedModeAscending, _chatBinder.SelectedTypeSorterIndex);
            _filter = new Filter(_chatBinder.SelectedTypeFilterIndex, _chatBinder.FilterGroupId, _chatBinder.FilterNickname);

            // load users' and messages' data from db
            _lastMsgsInChat = _queryRunner.excuteMsgQuery(_filter, _sorter);
            _displayedMsgs = new List<Message>();
            _displayedMsgs.AddRange(_lastMsgsInChat);

            _loggedInUser = null;
        }

        public List<User> GetRegisteredUsers()
        {
            // TODO: implement this function
            return _queryRunner.executeUserQuery(null);
        }
        public List<Message> GetMessagesInChat()
        {
            return Sort();
        }

        public Boolean Login(String nickname, string groupId, String password)
        {
            User userToLogin = new User(nickname, groupId, password);

            if (CheckIfUserExists(userToLogin))
            {
                // TODO :  check if the user's password matches his password in db
                if (isUserPasswordCorrect(userToLogin))
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
                    log.Error("A registered user tried to login with the wrong password");
                    return false;
                }
            }
            else
            {

                // logger
                log.Error("A non registered user tried to login");

                throw new ArgumentException("User not registered at all");
            }
        }
        private Boolean isUserPasswordCorrect(User userToLogin)
        {
            List<User> ans = _queryRunner.executeUserQuery(userToLogin);
            return (userToLogin.getPassword().Equals(ans[0].getPassword()));
        }
        public Boolean Register(String nickname, string groupId, string password)
        {
            // check if arguments are valid
            if ((nickname == null || nickname.Equals("")) | (groupId == null || groupId.Equals("")) | (password == null || password.Equals("")))
            {
                return false;
            }
            else
            {

                User userToRegister = new User(nickname, groupId, password);

                if (!CheckIfUserExists(userToRegister))
                {

                    _queryRunner.saveUserToDB(userToRegister);

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
            // try to find the user in the db
            return (_queryRunner.executeUserQuery(user) != null);   
        }

        public Boolean RetrieveMsg()
        {
            try
            {
                List<Message> retrievedMsg = _queryRunner.excuteMsgQuery(_filter, _sorter);
                if (_lastMsgsInChat.Count() + retrievedMsg.Count() > 200)
                {
                    // substract the difference
                    int difference = _lastMsgsInChat.Count() + retrievedMsg.Count() - 200;
                    _lastMsgsInChat.RemoveRange(0, difference);                   
                }

                // add the new messages
                _lastMsgsInChat.AddRange(retrievedMsg);

                // configure the displayed msgs according to sorting
                _displayedMsgs = Sort();

                // logger
                log.Info("Chatroom retrieved new messages from server and updated presentation");

                return true;
            }
            catch
            {
                // logger
                log.Error("Retrieval messages from db failed");

                return false;
            }
        }

        public Boolean SendMessage(String body)
        {
            try
            {
                // send message content to server through user, save the returned object message
                Message newSentMessage = _loggedInUser.SendMessage(body, _URL);

                // TODO: save message in db

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
            return _sorter.runSort(_lastMsgsInChat);
        }

        /*
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
        }*/

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

            public int getSorterType()
            {
                return _sortingType;
            }
            public Boolean isDescending()
            {
                return !_ascending;
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
            private String _groupId;
            private String _nickname;

            public Filter(int filteringType, String GID, String nick)
            {
                _filteringType = filteringType;
                _groupId = GID;
                _nickname = nick;
            }

            public void setFilteringType(int filteringType)
            {
                _filteringType = filteringType;
            }

            public int getFilterType()
            {
                return _filteringType;
            }

            public String getGroupId()
            {
                return _groupId;
            }

            public String getNickname()
            {
                return _nickname;
            }
            
        }
    }
}

