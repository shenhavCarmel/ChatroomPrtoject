using MileStoneClient.CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone2.LogicLayer
{
    [Serializable]
    public class User
    {
        // Fields
        private Boolean _isLoggedIn;
        private String _nickname;
        private string _groupId;

        // Constructors
        public User(String nickname, string groupId)
        {
            this._isLoggedIn = false;
            this._nickname = nickname;
            this._groupId = groupId;
        }


        public void Logout()
        {
            _isLoggedIn = false;
        }

        public void Login()
        {
            _isLoggedIn = true;
        }

        public Message SendMessage(string body, String URL)
        {
            try
            {
                IMessage sentMsg = Communication.Instance.Send(URL, _groupId, _nickname, body);

                // return the IMessage converted to message
                return new Message(sentMsg);
            }
            catch
            {
                throw new Exception();
            }

        }

        public String GetNickname()
        {
            return this._nickname;
        }

        public void SetNickname(String newNickname)
        {
            this._nickname = newNickname;
        }

        public string GetGroupId()
        {
            return this._groupId;
        }

        public void SetGroupId(string newGroupId)
        {
            this._groupId = newGroupId;
        }
    }
}
