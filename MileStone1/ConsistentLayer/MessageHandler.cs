using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MileStone1.LogicLayer;

namespace MileStone1.ConsistentLayer
{
    class MessageHandler
    {
        private String _fileName;
        private Stream _stream;

        public MessageHandler()
        {
            this._fileName = "MessagesFile.data";
            if (!File.Exists(this._fileName))
            {
                this._stream = File.Create(this._fileName);
                _stream.Close();

            }
            else
            {
                this._stream = File.OpenRead(this._fileName);
                _stream.Close();
            }
        }
        public List<Message> GetMessagesList()
        {
            List<Message> messages = new List<Message>();
            if (_stream.Length != 0)
            {
                this._stream = File.OpenRead(this._fileName);
                BinaryFormatter des = new BinaryFormatter();
                if (new FileInfo(this._fileName).Length != 0)
                    messages = (List<Message>)des.Deserialize(_stream);

            }
            return messages;
        }

        internal List<User> GetUsersList()
        {
            throw new NotImplementedException();
        }

        public void SaveMessage(Message msg)
        {
            List<Message> messages = GetMessagesList();
            if (!messages.Contains(msg))
            {
                messages.Add(msg);
                BinaryFormatter bf = new BinaryFormatter();
                //bf.Deserialize(_stream);
                //_stream = File.OpenRead(this._fileName).Serialize(_stream, messages);
                _stream.Close();
            }
        }
        public void SaveMessageList(List<Message> msg)
        {
            List<Message> messagesList = GetMessagesList();
            foreach (Message currMsg in msg)
            {
                if (!messagesList.Contains(currMsg))
                    messagesList.Add(currMsg);
            }
            BinaryFormatter bf = new BinaryFormatter();
            bf.Deserialize(_stream);
            bf.Serialize(_stream, messagesList);
        }
    }
}
