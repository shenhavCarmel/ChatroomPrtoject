using MileStone2.LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone2.ConsistentLayer
{
    //the UserHandler class trancfers message's information to files.
    class MessageHandler
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
          (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // fields
        private String _fileName;
        private Stream _stream;
        private List<Message> _messagesList;

        // constructor
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
                BinaryFormatter bf = new BinaryFormatter();
                if (new FileInfo(_fileName).Length != 0)
                {
                    _messagesList = (List<Message>)bf.Deserialize(_stream);
                    _stream.Close();
                }
                else _stream.Close();
            }
        }

        public List<Message> GetMessagesList()
        {
            return _messagesList;
        }

        //this method saves a new message's information in files
        public void SaveMessage(Message msg)
        {
            if (!_messagesList.Contains(msg))
            {
                _messagesList.Add(msg);
                if (File.Exists(_fileName))
                    File.Delete(_fileName);
                _stream = File.Create(_fileName);
                _stream.Close();
                _stream = File.OpenWrite(_fileName);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(_stream, _messagesList);
                _stream.Close();
                //logger
                log.Info("Message written to files");
            }

        }
        //this method saves a new message's information from a messages list in files
        public void SaveMessageList(List<Message> msg)
        {
            foreach (Message currMsg in msg)
            {
                if (!_messagesList.Contains(currMsg))
                    _messagesList.Add(currMsg);
            }
            if (File.Exists(_fileName))
                File.Delete(_fileName);
            _stream = File.Create(_fileName);
            _stream.Close();
            _stream = File.OpenWrite(_fileName);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(_stream, _messagesList);
            _stream.Close();

            //logger
            log.Info("Messages written to files");
        }
    }
}
