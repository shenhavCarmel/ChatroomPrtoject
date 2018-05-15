using MileStone2.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MileStone2.PresentationLayer
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChatRoomWindow : Window
    {
        private ChatRoom _chatRoom;
        public ChatRoomWindow(ChatRoom chatR)          
        {
            _chatRoom = chatR;
            InitializeComponent();
            txtMsgs.Text = ListToString(_chatRoom.GetMessagesInChat());
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            String msgBody = txtMsgBody.Text;
            if (msgBody.Length > 0 && msgBody.Length <= 150)
            {
                _chatRoom.SendMessage(msgBody);
            }
            else
            {
                if (MessageBox.Show("the message must contain less than 150 letters and more than 0", "invalid message", MessageBoxButton.OK
               , MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    this.txtMsgBody.Clear();
                }
            }

            if (_chatRoom.RetrieveMsg())
            {
               txtMsgs.Text = ListToString(_chatRoom.GetMessagesInChat());
            }

            txtMsgBody.Clear();
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbSortType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEnterUser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private String ListToString(List<Message> lst)
        {
            String str = "";
            foreach (Message currMsg in lst)
            {

                str = str + "\r\n" + currMsg.ToString();
            }

            return str;
        }
    }
}
