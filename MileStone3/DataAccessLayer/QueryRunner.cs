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

        private String _connectionString;
        private String _sqlQuery;
        private const String _serverAddress = "ise172.ise.bgu.ac.il,1433\\DB_LAB";
        private const String _databaseName = "MS3";
        private const String _userName = "publicUser";
        private const String _password = "isANerd";

        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _dataReader;
        private DateTime _recentTimeStamp;

        private Boolean _isFirstExecute;

        //constructor
        public QueryRunner()
        {
            _isFirstExecute = true;
            // set sql connection
            _connectionString = $"Data Source={_serverAddress};Initial Catalog={_databaseName};User ID={_userName};Password={_password}";
            _recentTimeStamp = new DateTime();
        }

        public List<Message> excuteMsgQuery(Filter filter, Sorter sorter)
        {
            _connection = new SqlConnection(_connectionString);
            List<Message> newMsgs = new List<Message>();
            try
            {

                _connection.Open();

                // generate a query according to the arguments
                _sqlQuery = generateMsgQuery(filter, sorter).toString();
                _command = new SqlCommand(_sqlQuery, _connection);
                _dataReader = _command.ExecuteReader();

                while (_dataReader.Read())
                {
                    //DateTime dateFacturation = new DateTime();
                    if (!_dataReader.IsDBNull(3))
                    {

                        User newUser = new User(_dataReader.GetValue(6).ToString(), _dataReader.GetValue(5).ToString(), _dataReader.GetValue(7).ToString());
                        Message currMsg = new Message(_dataReader.GetValue(3).ToString(), newUser);
                        newMsgs.Add(currMsg);
                    }

                }
                _dataReader.Close();
                _command.Dispose();
                _connection.Close();

                // update recent time stamp for future executing
                _recentTimeStamp = DateTime.Now;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newMsgs;
        }

        private Query generateMsgQuery(Filter filter, Sorter sorter)
        {
            const int GROUP = 0;
            const int USER = 1;
            const int NONE = 2;

            Query qr = null;

            switch (filter.getFilterType())
            {

                case GROUP:
                    qr = new Query("Messages msgs, Users users", "users.group_Id = " + filter.getGroupId() +
                        " AND msgs.User_Id = users.Id", true);
                    break;

                case USER:
                    qr = new Query("Messages msgs, Users users", "msgs.User_Id = users.Id " +
                        "AND users.Group_Id = " + filter.getGroupId() + " AND users.Nickname = " + filter.getNickname() , true);
                    break;

                case NONE:
                    qr = new Query("Messages msgs, Users users", "msgs.User_Id = users.Id", true);
                    break;     
            }
            if (!_isFirstExecute)
            {
                qr.setWhere(qr.getWhere() + " AND msgs.SendTime > convert(datetime, '" + _recentTimeStamp.ToString("dd/MM/yyyy HH:mm:ss") + "')");
            }
            else
                _isFirstExecute = false;

            return qr;
        }

        private Query generateUserQuery(User user)
        {
            Query qr = null;

            if (user == null)
            {

                // return list of registered users from db
                qr = new Query("Users", "", false);
            }
            else
            {

                // if "user" argument not null return the user details from db
                // (check if the user is registered)
                String where = "Nickname = " + user.GetNickname() + 
                               " AND Group_Id = " + user.GetGroupId();
                qr = new Query("Users", where, false);
            }

            return qr;
        }
        
        public List<User> executeUserQuery(User user)
        {
            _connection = new SqlConnection(_connectionString);

            // create list of users from db
            // if the user argument is not null return his record in db
            List<User> usersFromDB = new List<User>();
            try
            {
                _connection.Open();

                // generate a query according to the arguments
                _sqlQuery = generateUserQuery(user).toString();

                _command = new SqlCommand(_sqlQuery, _connection);
                _dataReader = _command.ExecuteReader();

                while (_dataReader.Read())
                {
                    if (!_dataReader.IsDBNull(0))
                    {
                        User currUser = new User(_dataReader.GetValue(2).ToString(), _dataReader.GetValue(1).ToString(),
                                             _dataReader.GetValue(3).ToString());
                        usersFromDB.Add(currUser);
                    }    
                }

                _dataReader.Close();
                _command.Dispose();
                _connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usersFromDB;
        }

        public void saveUserToDB(User userToSave)
        {
            _connection.Open();
            _command = new SqlCommand(null, _connection);

            _command.CommandText = "INSERT INTO Users([Group_Id], [Nickname],[Password])" +
                       "VALUES (@groupid,@nickname,@password)";

            SqlParameter groupId = new SqlParameter(@"groupid", System.Data.SqlDbType.Text, 20);
            SqlParameter nickname = new SqlParameter(@"nickname", System.Data.SqlDbType.Text, 20);
            SqlParameter password = new SqlParameter(@"password", System.Data.SqlDbType.Text, 20);


            groupId.Value = userToSave.GetGroupId();
            nickname.Value = userToSave.GetNickname();
            password.Value = userToSave.getPassword();

            _command.Parameters.Add(groupId);
            _command.Parameters.Add(nickname);
            _command.Parameters.Add(password);

            int numRowChange = _command.ExecuteNonQuery();
            _command.Dispose();
            _connection.Close();


        }
    }
}