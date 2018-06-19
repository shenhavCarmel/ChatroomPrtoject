using MileStone3.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone3.LogicLayer
{
    [Serializable]
    public class User
    {
        // Fields
        private Boolean _isLoggedIn;
        private String _nickname;
        private String _groupId;
        private String _password;

        // Constructors
        public User(String nickname, String groupId, String password)
        {
            this._isLoggedIn = false;
            this._nickname = nickname;
            this._groupId = groupId;
            this._password = password;
            
        }

        public User(String nickname, String groupId)
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

        public void SendMessage(string body, QueryRunner queryRunner)
        {
            Message toSend = new Message(body, this, DateTime.UtcNow, Guid.NewGuid());

            // update DB
            queryRunner.saveMsgToDB(toSend);
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

        public String GetPassword()
        {
            return this._password;
        }
    }
}
