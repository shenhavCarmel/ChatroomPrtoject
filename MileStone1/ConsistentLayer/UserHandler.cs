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



        public UserHandler()
        {
            this._fileName = "userFile.data";
            if (!File.Exists(this._fileName))
            {
                this._stream = File.Create(this._fileName);
                _stream.Close();

            }

        }
        public List<User> GetUsersList()
        {
            List<User> users = new List<User>();
            if (_stream.Length != 0)
            {
                this._stream = File.OpenRead(this._fileName);
                BinaryFormatter bf = new BinaryFormatter();
                users = (List<User>)bf.Deserialize(_stream);
              //  _stream.Close();
          }
            return users;
      
         } 
        
        public void SaveUser(User user)
        {
            List<User> users = GetUsersList();
            users.Add(user);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Deserialize(_stream);
            bf.Serialize(_stream, users);
        }

    }
}
