using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone1.LogicLayer
{
    class User
    {

        private Boolean isLoggedIn;
        private String nickname;
        private int groupId;

        public User (String nickname, int groupId)
        {
            this.isLoggedIn = false;
            this.nickname = nickname;
            this.groupId = groupId;
        }

        public void logout()
        {
            isLoggedIn = false;
        }

        public void login()
        {
            isLoggedIn = true;
        }

        public Message sendMessage(string msg)
        {
            Message message= CommunicationLayer.Communication.send(ChatRoom.URL, this.groupId, msg);
        }
        
        private void saveToPresistent()
        {

        }
    }
}
