using MileStone2.LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MileStone2.ConsistentLayer
{
    //the UserHandler class trancfers User's information to files
    class UserHandler
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // fields
        private String _fileName;
        private Stream _stream;
        private List<User> _usersList;

        // constructor
        public UserHandler()
        {

            this._usersList = new List<User>();
            this._fileName = "userFile.bin";
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
                    _usersList = (List<User>)bf.Deserialize(_stream);
                    _stream.Close();
                }
                else _stream.Close();
            }
        }

        public List<User> GetUsersList()
        {
            return _usersList;
        }

        //this method saves a new user's information in files
        public void SaveUser(User user)
        {
            _usersList.Add(user);
            if (File.Exists(_fileName))
                File.Delete(_fileName);
            _stream = File.Create(_fileName);
            _stream.Close();
            _stream = File.OpenWrite(_fileName);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(_stream, _usersList);
            _stream.Close();
            //logger
            log.Info("User written to files");
        }

    }
}
