using System;
using NUnit.Framework;
using ChatRoom;
using System.Collections.Generic;

namespace ChatRoom.UnitTest
{

    [TestFixture]
    public class ChatRoomTests
    {

        MileStone2.LogicLayer.ChatRoom x;


        [SetUp]
        public void SetupTests()
        {
            x = new MileStone2.LogicLayer.ChatRoom(new MileStone2.PresentationLayer.ActionListener());
        }
 
        // checked
        [Test]
        public void CheckIfUserExists_RegisteredUser_True()
        {
            if (x.GetRegisteredUsers() != null)
            {
                MileStone2.LogicLayer.User us = x.GetRegisteredUsers()[1];
                bool ans = x.CheckIfUserExists(us);
                Assert.IsTrue(ans);
            }
            Assert.Pass();
        }

        // checked
        [Test]
        public void SortedMsgs_TimeStampSort_True()
        {
            List<MileStone2.LogicLayer.Message> msgs = x.GetMessagesInChat();
            x.GetSorter().setSortingType(0);
            x.Sort();
            int compare;
            Boolean isSorted = true;
            for( int i=0; i < msgs.Count - 1 && isSorted; i++)
            {
                compare = msgs[i].GetDate().CompareTo(msgs[i+1].GetDate());
                if (compare == 1)
                    isSorted = false;  
            }
            Assert.IsTrue(isSorted);
        }

        // checked
        [Test]
        public void Filter_FilterByGroupID_true()
        {
            List<MileStone2.LogicLayer.User> us = x.GetRegisteredUsers();
            x.GetFilter().setFilteringType(0);
            List<MileStone2.LogicLayer.Message> msgs = x.GetFilter().runFilter(x.GetMessagesInChat()
                , us, us[0].GetGroupId(), us[0].GetNickname());
            Boolean isFiltered = true;
            String groupID = msgs[0].GetGroupID();
            foreach(MileStone2.LogicLayer.Message msg in msgs)
            {
                if (!(msg.GetGroupID() == groupID) & isFiltered)
                {
                    isFiltered = false;
                }
            }
            Assert.IsTrue(isFiltered);
        }

        [Test]
        public void Filter_FilterByNickName_true()
        {
            List<MileStone2.LogicLayer.User> us = x.GetRegisteredUsers();
            x.GetFilter().setFilteringType(1);
            List<MileStone2.LogicLayer.Message> msgs = x.GetFilter().runFilter(x.GetMessagesInChat()
                , us, us[0].GetGroupId(), us[0].GetNickname());
            Boolean isFiltered = true;
            String nickName = msgs[0].GetUserName();
            foreach (MileStone2.LogicLayer.Message msg in msgs)
            {
                if (!(msg.GetUserName() == nickName) & isFiltered)
                {
                    isFiltered = false;
                }
            }
            Assert.IsTrue(isFiltered);
        }

    }

}