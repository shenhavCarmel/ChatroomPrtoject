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

    class UserHandler
    {
        private String _fileName;
        private Stream _stream;
        private List<User> _usersList;


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
                if (_stream.Length != 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _usersList = (List<User>)bf.Deserialize(_stream);
                    _stream.Close();
                    File.Delete(_fileName);
                    _stream=File.Create(_fileName);
                    _stream.Close();
                    _stream = File.OpenRead(_fileName);
                    bf.Serialize(_stream, _usersList);
                    _stream.Close();
                }
                else _stream.Close();
            }
        }
        public List<User> GetUsersList()
        {
            return _usersList;
         } 
        
        public void SaveUser(User user)
        {
            _usersList.Add(user);
            File.Delete(_fileName);
            _stream=File.Create(_fileName);
            _stream.Close();
            _stream = File.OpenWrite(_fileName);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(_stream, _usersList);
            _stream.Close();
        }

    }
}
