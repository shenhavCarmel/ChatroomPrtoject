using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStone1.CommunicationLayer;

namespace MileStone1.LogicLayer
{
    class User
    {

        private Boolean _isLoggedIn;
        private String _nickname;
        private string _groupId;

        public User (String nickname, string groupId)
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

        public IMessage SendMessage(string body, String URL)
        {
            return CommunicationLayer.Communication.Instance.Send(URL, _groupId, _nickname, body);
        }
        
        private void SaveToPresistent()
        {

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
