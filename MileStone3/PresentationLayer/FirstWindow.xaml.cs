using MileStone3.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MileStone3.PresentationLayer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        private ChatRoom chatR;
        private ActionListener _chatBinder;
        private Hashing _hashing;
        private Boolean _isPasswordValid;
        private string _hashedPassword;

        public FirstWindow()
        {
            InitializeComponent();
            _chatBinder = new ActionListener();
            this.DataContext = _chatBinder;
            this.chatR = new ChatRoom(_chatBinder);
            this._hashing = new Hashing();
            this._isPasswordValid = false;
            this._hashedPassword = "";
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            String strNickname = _chatBinder.StartNickname;
            String strGroupID = _chatBinder.StartGroupId;

            // if user details are valid, register
            if (CheckValidGroupId(strGroupID) && _isPasswordValid)
            {
                try
                {
                    if (this.chatR.Register(strNickname, strGroupID, _hashedPassword))
                    {
                        MessageBox.Show("Registerd succesfully");
                    }
                    else
                    {
                        if (MessageBox.Show("Registered failed - user already registered!",
                                            "Please Login", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                        {
                            _chatBinder.StartGroupId = "";
                            _chatBinder.StartNickname = "";

                        }
                    }
                }
                catch
                {
                    if (MessageBox.Show("Invalid group Id/nickname - group Id should be only numbers, nickname must be at least one character",
                                    "invalid deatils", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                    {
                        _chatBinder.StartGroupId = "";
                        _chatBinder.StartNickname = "";
                    }
                }

            }
            else
            {
                if (MessageBox.Show("Invalid group Id or password- group Id should be only numbers, password is 4-6 numbers or letters",
                                    "invalid deatils", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    _chatBinder.StartGroupId = "";
                }
            }
        }
        private Boolean CheckValidGroupId(string strGroupId)
        {
            int groupId;
            return (int.TryParse(strGroupId, out groupId));
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string nickName = _chatBinder.StartNickname;
            string groupID = _chatBinder.StartGroupId;

            // if the user is valid, login
            if (CheckValidGroupId(groupID) | !(_isPasswordValid))
            {
                try
                {

                    if (chatR.Login(nickName, groupID, _hashedPassword))
                    {

                        // switch to the next window
                        ChatRoomWindow chatWindow = new ChatRoomWindow(chatR, _chatBinder);
                        this.Close();
                        chatWindow.ShowDialog();
                    }

                    // wrong passowrd
                    else
                    {
                        if (MessageBox.Show("wrong password input", "wrong password", MessageBoxButton.OK
                                                                , MessageBoxImage.Error) == MessageBoxResult.OK)
                        {

                        }
                    }
                }

                // the user trying to login isn't registered
                catch
                {
                    if (MessageBox.Show("This user isn't registered", "invalid user", MessageBoxButton.OK
                                                                                    , MessageBoxImage.Error) == MessageBoxResult.OK)
                    {
                    }
                }
            }

            // invalid password or groupId
            else
            {
                if (MessageBox.Show("Invalid group Id or password- group Id should be only numbers, password is 4-6 numbers or letters",
                                    "invalid deatils", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    _chatBinder.StartGroupId = "";
                }
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private Boolean CheckValidPassword(String strPassword)
        {
            if (strPassword == null || strPassword == "" | strPassword.Length < 4 | strPassword.Length > 6)
            {
                return false;
            }
            else
                return strPassword.All(Char.IsLetterOrDigit);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = sender as PasswordBox;
            if (CheckValidPassword(pb.Password))
            {
                _hashedPassword = _hashing.GetHashString(pb.Password + "1337");
                _isPasswordValid = true;
            }
            else
                _isPasswordValid = false;
        }
    }
}