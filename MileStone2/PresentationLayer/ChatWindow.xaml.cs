using MileStone2.LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace MileStone2.PresentationLayer
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChatRoomWindow : Window
    {
        private ChatRoom _chatRoom;
        public ActionListener _chatBinder;
        private DispatcherTimer _dispatcherTimer;


        public ChatRoomWindow(ChatRoom chatR, ActionListener chatBinder)
        {
            InitializeComponent();
            _chatRoom = chatR;
            _chatBinder = chatBinder;
            this.DataContext = _chatBinder;

            this._dispatcherTimer = new DispatcherTimer();

            _dispatcherTimer.Tick += dispatcherTimer_Tick;   // initialize in constructor
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 2);
            _dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _chatRoom.RetrieveMsg();
            _chatBinder.DisplayedMsgs.Clear();

            foreach (Message currMsg in _chatRoom.GetMessagesInChat())
            {
                _chatBinder.DisplayedMsgs.Add(currMsg.ToString());

            }

            _chatBinder.OnPropertyChanged("DisplayedMsgs");
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            String msgBody = _chatBinder.MsgBody;

            if (checkMsgLength(msgBody))
            {
                _chatRoom.SendMessage(msgBody);
                _chatBinder.MsgBody = "";
            }
            else
            {
                if (MessageBox.Show("the message must contain less than 150 letters and more than 0", "invalid message", MessageBoxButton.OK
               , MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    _chatBinder.MsgBody = "";
                }
            }
        }

        public Boolean checkMsgLength( String MsgBody)
        {
            if (MsgBody.Length > 0 && MsgBody.Length <= 150)
            {
                return true;
            }
                return false;
        }

        private void txtMsgBody_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                txtDisplayMsgs.Focus();
                txtMsgBody.Focus();
                btnSend_Click(sender, e);
            }
        }
        
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _chatBinder.DisplayedMsgs.Clear();
                _chatRoom.FilterMsgs();
                foreach (Message currMsg in _chatRoom.GetMessagesInChat())
                {
                    _chatBinder.DisplayedMsgs.Add(currMsg.ToString());
                }
            }
            catch
            {
                if (MessageBox.Show("Illegal input", "invalid nickname or group Id", MessageBoxButton.OK
                                                   , MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    _chatBinder.FilterGroupId = "";
                    _chatBinder.FilterNickname = "";
                }
            }
        }
    }
}