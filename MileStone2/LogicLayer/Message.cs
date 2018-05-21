﻿using MileStoneClient.CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone2.LogicLayer
{
    [Serializable]
    public class Message
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _messageContent;
        private Guid _ID;
        private DateTime _timeStamp;
        private string _groupID;
        private string _userName;

        public Message(IMessage msgToCopy)
        {

            this._messageContent = msgToCopy.MessageContent;
            this._groupID = msgToCopy.GroupID;
            this._userName = msgToCopy.UserName;
            this._ID = msgToCopy.Id;
            this._timeStamp = msgToCopy.Date;

            // logger
            log.Info("New message was created");
        }

        public Message(string body, User user)
        {

            this._messageContent = body;
            this._groupID = user.GetGroupId();
            this._userName = user.GetNickname();

            // logger
            log.Info("New message was created");
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


        public String ToString()
        {
            return (this._userName +"(group " + this._groupID + "): " + "\r\n" + this._messageContent
                     + "\r\n" + "-" + this._timeStamp + "-");
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
