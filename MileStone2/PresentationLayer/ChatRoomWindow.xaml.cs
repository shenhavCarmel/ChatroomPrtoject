using MileStone2.LogicLayer;
using System;
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

        private void UpdateScrollBar(ListBox listBox)
        {
            if (listBox != null)
            {
                var border = (Border)VisualTreeHelper.GetChild(listBox, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // check if a new message was added to the list box
                int numOfMsg = _chatBinder.DisplayedMsgs.Count;

                // retrieve & display
                _chatRoom.RetrieveMsg();
                _chatBinder.DisplayedMsgs.Clear();

                foreach (Message currMsg in _chatRoom.GetMessagesInChat())
                {
                    _chatBinder.DisplayedMsgs.Add(currMsg.ToString());

                }  

                _chatBinder.OnPropertyChanged("DisplayedMsgs");

                // check if a new message was added to the list box
                int numOfNewMsgs = _chatBinder.DisplayedMsgs.Count;
                if (numOfMsg < numOfNewMsgs)
                    UpdateScrollBar(lbDisplayMsgs);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.ToString(), "", MessageBoxButton.OK
                                                   , MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    _chatBinder.FilterGroupId = "";
                    _chatBinder.FilterNickname = "";
                }
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            String msgBody = _chatBinder.MsgBody;

            if (msgBody.Length > 0 && msgBody.Length <= 150)
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

        
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSend_Click(sender, e);
            }
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
           catch (Exception ex)
           {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK
                                                   , MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    _chatBinder.FilterGroupId = "";
                    _chatBinder.FilterNickname = "";
                }
            }   
        }

        private void txtMsgBody_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                lbDisplayMsgs.Focus();
                txtMsgBody.Focus();
                btnSend_Click(sender, e);
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("are you sure you want to log out?", "log out", MessageBoxButton.YesNo
                        , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this._chatRoom.Logout();
                FirstWindow fs = new FirstWindow();
                fs.Show();
                this.Hide();

            }
        }

        private void cbFilterType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            const int GROUP = 0;
            const int USER = 1;
            const int NONE = 2;

            switch (_chatBinder.SelectedTypeFilterIndex)
            {
                case GROUP:
                    _chatBinder.NicknameFilterEnabled = false;
                    _chatBinder.GroupIDFilterEnabled = true;
                    _chatBinder.FilterNickname = "";
                    break;
                case USER:
                    _chatBinder.NicknameFilterEnabled = true;
                    _chatBinder.GroupIDFilterEnabled = true;
                    break;
                case NONE:
                    _chatBinder.NicknameFilterEnabled = false;
                    _chatBinder.GroupIDFilterEnabled = false;
                    _chatBinder.FilterNickname = "";
                    _chatBinder.FilterGroupId = "";
                    break;
                default:
                    break;
                    
            }
        }
    }
}
