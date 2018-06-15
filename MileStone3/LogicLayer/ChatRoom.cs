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
            _filter = new Filter(_chatBinder);


            // load users' and messages' data from db
            _lastMsgsInChat = Sort(_queryRunner.excuteMsgQuery(null), true);
            _displayedMsgs = new List<Message>();
            _displayedMsgs.AddRange(_lastMsgsInChat);

            _loggedInUser = null;
        }

        public List<User> GetRegisteredUsers()
        {
            return _queryRunner.executeUserQuery(null);
        }
        
        public List<Message> GetMessagesInChat()
        {
            return _displayedMsgs;
        }

        public Boolean Login(String nickname, String groupId, String password)
        {
            User userToLogin = new User(nickname, groupId, password);

            if (CheckIfUserExists(userToLogin.GetNickname(), userToLogin.GetGroupId()))
            {
                // check if the user's password matches his password in db
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
            return (userToLogin.GetPassword().Equals(ans[0].GetPassword()));

        }
        public Boolean Register(String nickname, String groupId, String password)
        {
            // check if arguments are valid
            if ((nickname == null || nickname.Equals("")) | (groupId == null || groupId.Equals("")) | (password == null || password.Equals("")))
            {
                throw new ArgumentException("register details invalid");
            }
            else
            {

                User userToRegister = new User(nickname, groupId, password);

                if (!CheckIfUserExists(userToRegister.GetNickname(), userToRegister.GetGroupId()))
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

        public Boolean CheckIfUserExists(String nickname, String groupId) 
        {
            // try to find the user in the db
            User userTocheck = new User(nickname, groupId);
            return (_queryRunner.executeUserQuery(userTocheck).Count != 0);   
        }

        public Boolean RetrieveMsg()
        {
            try
            {
                // refresh filter
                _filter.setFilteringType();
                _filter.setGroupId();
                _filter.setNickname();

                // take new messages from server
                List<Message> retrievedMsg = _queryRunner.excuteMsgQuery(null);

                if (_lastMsgsInChat.Count() + retrievedMsg.Count() > 200)
                {
                    // substract the difference
                    int difference = _lastMsgsInChat.Count() + retrievedMsg.Count() - 200;
                    _lastMsgsInChat.RemoveRange(0, difference);
                }

                // add the new messages
                _lastMsgsInChat.AddRange(retrievedMsg);

                // keep lastMsgsInChat list sorted by timeStamp
                _lastMsgsInChat = Sort(_lastMsgsInChat, true);

                // the user wants to filter, use filtered query
                if (_filter.isFiltered() && _filter.isValid())
                {
                    _displayedMsgs = Sort(_queryRunner.excuteMsgQuery(_filter), false);
                }

                // the user isn't filtering, diaplay all last messages
                else
                {
                    _displayedMsgs = Sort(_lastMsgsInChat, false);
                }

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
                // send message and save in DB through user
                _loggedInUser.SendMessage(body, _queryRunner);

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

        public User getLoggedInUser()
        {
            return _loggedInUser;
        }

        public Message getMessagToEdit(String msg)
        {
            foreach (Message currMsg in _displayedMsgs)
            {
                if (currMsg.ToString().Equals(msg))
                    return currMsg;
            }
            return null;
        }

        public void editMsg(Message msg, String newContent)
        {
            _queryRunner.editMsgInDb(msg, newContent);
            _displayedMsgs.Remove(msg);
            _lastMsgsInChat.Remove(msg);
        }


        public List<Message> Sort(List<Message> lst, Boolean byTimeStamp)
        {
            // force sorting by timeStamp
            if (byTimeStamp)
            {
                _sorter.setSortingType(0);
                _sorter.setAscending(true);

            }
            else
            {
                // sort the relevant messages according to settings
                _sorter.setSortingType(_chatBinder.SelectedTypeSorterIndex);
                _sorter.setAscending(_chatBinder.SorterMode[0]);
            }

            // logger
            log.Info("Messages in chat room where sorted by user preferences");
            return _sorter.runSort(lst);
        
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

                msgs = listToSort.Distinct().ToList();

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
            private ActionListener _binder;

            public Filter(ActionListener binder)
            {
                this._binder = binder;
                _filteringType = binder.SelectedTypeFilterIndex;
                _groupId = binder.FilterGroupId;
                _nickname = _binder.FilterNickname;
            }

            public void setFilteringType()
            {
                _filteringType = _binder.SelectedTypeFilterIndex;
            }

            public void setGroupId()
            {
                _groupId = _binder.FilterGroupId;
            }

            public void setNickname()
            {
                _nickname = _binder.FilterNickname;
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

            internal bool isFiltered()
            {
                return _filteringType != NONE;
            }

            public bool checkIfFilterByUser()
            {
                return (this.getFilterType() == USER);
            }

            public bool isValid()
            {
                bool isFilterValid = true;

                switch (this.getFilterType())
                {
                    case GROUP:
                        if (this.getGroupId() == "")
                        {
                            isFilterValid = false;
                        }

                        break;
                    case USER:
                        if (this.getGroupId() == "" || this.getNickname() == "")
                        {
                            isFilterValid = false;
                        }

                        break;
                }

                return isFilterValid;
            }
        }
    }
}

