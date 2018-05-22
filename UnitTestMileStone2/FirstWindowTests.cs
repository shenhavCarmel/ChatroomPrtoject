using System;
using NUnit.Framework;
using ChatRoom;
using MileStone2.LogicLayer;

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

        // check if an user without a nickname can register
        [Test]
        public void Register_NullNickname_False()
        {
            bool ans = x.Register(null, "23");
            Assert.IsFalse(ans);
        }

        // check if a registered user can register again
        [Test]
        public void Register_UserAlreadyExists_False()
        {
            if (x.GetRegisteredUsers() != null & x.GetRegisteredUsers().Count > 0)
            {
                MileStone2.LogicLayer.User us = x.GetRegisteredUsers()[0]; 
                bool ans = x.Register(us.GetNickname(), us.GetGroupId());
                Assert.IsFalse(ans);
            }
            else
                Assert.Inconclusive();
        }

        // check if the function "CanExit" is correct
        [Test]
        public void CheckIfCanExit_exit_True()
        {
            bool ans = x.CanExit();
            Assert.IsTrue(ans);
        }

        // check if the "Login" function is correct
        [Test]
        public void Login_UserIsRegistred_true()
        {
            if (x.GetRegisteredUsers() != null & x.GetRegisteredUsers().Count > 0)
            {
                MileStone2.LogicLayer.User us = x.GetRegisteredUsers()[1];
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