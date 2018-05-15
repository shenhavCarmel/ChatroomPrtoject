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
        public FirstWindow()
        {
            InitializeComponent();
            this.chatR = new ChatRoom();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string strGroupID = txtGroupId.Text;
            string strNickname = txtNickname.Text;

            if (CheckValidGroupId(strGroupID) && this.chatR.Register(strNickname, strGroupID))
            {
                MessageBox.Show("Registerd succesfully");
            }
            else
            {
                MessageBox.Show("Illegal input. Is this user already registered?");
            }
        }

        private Boolean CheckValidGroupId(string strGroupId)
        {
            int groupId;
            return (int.TryParse(strGroupId, out groupId));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string nickName = txtNickname.Text;
            string groupID = txtGroupId.Text;

            if (chatR.Login(nickName, groupID))
            {

                // switch to the next window
                ChatRoomWindow chatWindow = new ChatRoomWindow(chatR);
                this.Close();
                chatWindow.ShowDialog();
            }

            if (MessageBox.Show("this user is'nt registered", "invalid name", MessageBoxButton.OK
                , MessageBoxImage.Error) == MessageBoxResult.OK)
            {
                this.txtNickname.Clear();
                this.txtGroupId.Clear();

            }
        }
    }
}