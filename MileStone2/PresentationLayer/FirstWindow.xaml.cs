
using MileStone2.LogicLayer;
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

namespace MileStone2.PresentationLayer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        private ChatRoom chatR;
        private ActionListener _chatBinder;
        public FirstWindow()
        {
            InitializeComponent();
            _chatBinder = new ActionListener();
            this.DataContext = _chatBinder;
            this.chatR = new ChatRoom(_chatBinder);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            String strNickname = _chatBinder.StartNickname;
            String strGroupID = _chatBinder.StartGroupId;

            if (CheckValidGroupId(strGroupID) && this.chatR.Register(strNickname, strGroupID))
            {
                MessageBox.Show("Registerd succesfully");
            }
            else
            {
                MessageBox.Show("Illegal input");
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

            if (chatR.Login(nickName, groupID))
            {

                // switch to the next window
                ChatRoomWindow chatWindow = new ChatRoomWindow(chatR, _chatBinder);
                this.Close();
                chatWindow.ShowDialog();
            }

            else
            {
                if (MessageBox.Show("this user isn't registered", "invalid name", MessageBoxButton.OK
                , MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    _chatBinder.StartNickname = "";
                    _chatBinder.StartGroupId = "";

                }
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}