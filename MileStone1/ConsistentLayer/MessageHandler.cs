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
        private List<Message> _messagesList;

        public MessageHandler()
        {
            this._messagesList = new List<Message>();
            this._fileName = "MessageFile.bin";
            if (!File.Exists(this._fileName))
            {
                this._stream = File.Create(this._fileName);
                _stream.Close();

            }
            else
            {
                this._stream = File.OpenRead(this._fileName);
                if (_stream.Length != 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _messagesList = (List<Message>)bf.Deserialize(_stream);
                    _stream.Close();
                    File.Delete(_fileName);
                    _stream = File.Create(_fileName);
                    _stream.Close();
                    _stream = File.OpenRead(_fileName);
                    bf.Serialize(_stream, _messagesList);
                    _stream.Close();
                }
                else _stream.Close();
            }
        }
        public List<Message> GetMessagesList()
        {
            return _messagesList;
        }


        public void SaveMessage(Message msg)
        {
            if (!_messagesList.Contains(msg))
            {
                _messagesList.Add(msg);
                File.Delete(_fileName);
                _stream = File.Create(_fileName);
                _stream.Close();
                _stream = File.OpenWrite(_fileName);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(_stream, _messagesList);
                _stream.Close();
            }
        }
        public void SaveMessageList(List<Message> msg)
        { 
            foreach (Message currMsg in msg)
            {
                if (!_messagesList.Contains(currMsg))
                    _messagesList.Add(currMsg);
            }
            File.Delete(_fileName);
            _stream = File.Create(_fileName);
            _stream.Close();
            _stream = File.OpenWrite(_fileName);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(_stream, _messagesList);
            _stream.Close();
        }
    }
}
