using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStoneClient.CommunicationLayer;

namespace MileStone1.LogicLayer
{

    class Message
    {
        private string _messageContent;
        private Guid _ID;
        private DateTime _timeStamp;
        private string _groupID;
        private string _userName;


        public Message(string body, User user)
        {
            if (body.Length <= 150)
            {
                this._messageContent = body;
                this._groupID = user.GetGroupId();
                this._userName = user.GetNickname();


            }
            else
            {
                throw new ArgumentException("Message content over 150 characters");
            }
        }


        public String GetMessageContent()
        {
            return this._messageContent;
        }


        public String GetGroupID()
        {
            return this._groupID;
        }


        public String GetUserName()
        {
            return this._userName;
        }

        public DateTime GetDate()
        {
            return this._timeStamp;
        }

        public Guid GetId()
        {
            return this._ID;
        }


        public String toString()
        {
            return ("Sender: " + this._userName + "\r\n" + "Time Stamp: " + this._timeStamp
                + "\r\n" + "GUID: " + this._ID + "\r\n" + "Body: " + this._messageContent);
        }


        public void SetDate(DateTime dateTime)
        {
            this._timeStamp = dateTime;
        }





        public void SetGuid(Guid id)
        {
            this._ID = id;
        }

    }
}
