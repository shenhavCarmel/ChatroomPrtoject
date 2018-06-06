using MileStone3.LogicLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MileStone3.LogicLayer.ChatRoom;

namespace MileStone3.DataAccessLayer
{
    class QueryRunner
    {
        //fields

        private const int GROUP = 0;
        private const int USER = 1;
        private const int NONE = 2;

        private String _connectionString;
        private String _sqlQuery; 
        private const String _serverAddress = "ise172.ise.bgu.ac.il,1433\\DB_LAB" ;
        private const String _databaseName = "MS3";
        private const String _userName = "publicUser";
        private const String _password = "isANerd";

        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _dataReader;
        private DateTime _recentTimeStamp;

        //constructor
        public QueryRunner()
        {
            _sqlQuery = null;
            _connectionString = $"Data Source={_serverAddress};Initial Catalog={_databaseName };" +
                $"User ID={_userName};Password={_password}";
            _connection = new SqlConnection(_connectionString);
            
            
        }

        public List<Message> excuteQuery(Filter filter )
        {
            List<Message> ans = new List<Message>();
            try
            {
                _connection.Open();
                int filterType = filter.getFilterType();
                Query qr = null;

                switch (filterType)
                {
                    case GROUP:
                        qr = new Query("Messages msgs, Users users","users.group_Id = " + filter.getGroupId() +
                            " AND msg.User_Id = users.Id");
                        break;

                    case USER:
                        qr = new Query("Messages msgs, Users users" , "users.Id AND users.Group_Id = 4 AND users.Nickname = " 
                            + filter.getNickname());
                        break;

                    case NONE:
                        qr = new Query("Messages", "");
                        break;
                }
                _sqlQuery = qr.ToString();
                _command = new SqlCommand(_sqlQuery , _connection);
                _dataReader = _command.ExecuteReader();
                while (_dataReader.Read())
                {
                    //DateTime dateFacturation = new DateTime();
                    if (!_dataReader.IsDBNull(4))
                    {
                       // Message currMsg = new Message(_dataReader.GetValue(1) , )
                    }
                        
                }
                _dataReader.Close();
                _command.Dispose();
                _connection.Close();

                while (_dataReader.Read())
                {

                }
            }
            catch(Exception ex)
            {
                
            }

            return null;
        }
    }
}
