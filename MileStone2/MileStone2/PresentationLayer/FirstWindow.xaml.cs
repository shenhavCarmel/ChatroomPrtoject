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
        public void MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            string strGroupID = txtGroupId.Text;

            // check if the groupID is ligal
            if (CheckValidGroupId(strGroupID) && strGroupID.Equals("23"))
            {
                string nickname = txtNickname.Text;

                // check if the user alredy exists
                /*
                TODO: if !(ChatRoom.Instance.Register(nickname, strGroupID))
                {
                   //TODO: register the user 
                }
                */
                Popup myPopup = new Popup();
                myPopup.IsOpen = true;



            }
        }



        // check if the user alredy exists


        // until the input isn't valid and new, keep registering



        private Boolean CheckValidGroupId(string strGroupId)
        {
            int groupId;
            return (!int.TryParse(strGroupId, out groupId));
        }

    }
}