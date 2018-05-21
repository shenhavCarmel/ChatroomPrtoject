using System;
using NUnit.Framework;
using ChatRoom;


namespace ChatRoom.UnitTest
{

    [TestFixture]
    public class FirstWindowTests
    {

        MileStone2.LogicLayer.ChatRoom x;


        [SetUp]
        public void SetupTests()
        {
            x = new MileStone2.LogicLayer.ChatRoom(new MileStone2.PresentationLayer.ActionListener());
        }

        // checked
        [Test]
        public void Register_NullNickname_False()
        {
            bool ans = x.Register(null, "23");
            Assert.IsFalse(ans);
        }

        // checked 
        [Test]
        public void Register_UserAlreadyExists_False()
        {
            if (x.getRegisteredUsers() != null)
            {
                MileStone2.LogicLayer.User us = x.getRegisteredUsers()[0];
                bool ans = x.Register(us.GetNickname(), us.GetGroupId());
                Assert.IsFalse(ans);
            }
            Assert.Pass();
        }

        // checked
        [Test]
        public void CheckIfCanExit_exit_True()
        {
            bool ans = x.CanExit();
            Assert.IsTrue(ans);
        }

        // checked
        [Test]
        public void login_UserIsRegistred_true()
        {
            if (x.getRegisteredUsers() != null)
            {
                MileStone2.LogicLayer.User us = x.getRegisteredUsers()[1];
                bool result = x.Login(us.GetNickname(), us.GetGroupId());
                Assert.IsTrue(result);
            }
            else
            {
                Assert.Fail();
            }
        }

    }

}