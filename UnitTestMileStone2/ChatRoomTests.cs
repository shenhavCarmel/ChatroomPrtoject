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
        
        // check if the function "CheckIfUserExists" is correct
        [Test]
        public void CheckIfUserExists_RegisteredUser_True()
        {
            if (x.GetRegisteredUsers() != null & x.GetRegisteredUsers().Count > 0)
            {
                MileStone2.LogicLayer.User us = x.GetRegisteredUsers()[0];
                bool ans = x.CheckIfUserExists(us);
                Assert.IsTrue(ans);
            }
            Assert.Pass();
        }

        // check if the function "Sort" is correct 
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

        // check if the function
        [Test]
        public void Filter_FilterByGroupID_true()
        {
            List<MileStone2.LogicLayer.User> us = x.GetRegisteredUsers();
            x.GetFilter().setFilteringType(0);
            List<MileStone2.LogicLayer.Message> msgs = x.GetFilter().runFilter(x.GetMessagesInChat()
                , us, us[0].GetGroupId(), us[0].GetNickname());
            Boolean isFiltered = true;
            String groupID = us[0].GetGroupId();
            foreach (MileStone2.LogicLayer.Message currMsg in msgs)
            {
                if ((currMsg.GetGroupID() != groupID) & isFiltered)
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
            String nickName = us[0].GetNickname();
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