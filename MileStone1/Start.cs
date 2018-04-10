using MileStone1.CommunicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MileStone1
{
    class Start
    {
        static void Main(string[] args)
        {
            PresentationLayer.GUI start = new PresentationLayer.GUI();
            string gourpID = "2";
            string nickName = "Ben";
            string messageContent = "BenRh safd";
            string url = "http://ise172.ise.bgu.ac.il";  // url: ip + port


            IMessage msg = Communication.Instance.Send(url, gourpID, nickName, messageContent);

            // return with updated time and guid
            Console.WriteLine("MessageTime:{0} , Guid:{1}\n", msg.Date.ToString(), msg.Id);

            Console.WriteLine(msg + "\n");
            //Cannot create instance of CommunicationMessage
            //IMessage msg2 = new CommunicationMessage(); Error

            List<IMessage> msgList = Communication.Instance.GetTenMessages(url);
            Console.WriteLine("Reuest 10 Last Messages:");
            foreach (IMessage msgItem in msgList)
            {
                Console.WriteLine(msgItem);
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
