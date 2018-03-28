using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass]
    public class ChangeNotificationCollectionProperty
    {
        private Friend _friend;
        private FriendEmail _friendEmail;

        [TestInitialize]
        public void Initialize()
        {
            _friendEmail = new FriendEmail { Email = "levon.ecs@gmail.com" };
            _friend = new Friend
            {
                FirstName = "Levon",
                Address = new Address(),
                Emails = new List<FriendEmail>()
                {
                    new FriendEmail {Email = "levon-xxl@mail.ru"},
                    _friendEmail
                }

            };
        }

        [TestMethod]
        public void ShuldIntializeEmailsProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsNotNull(wrapper.Emails);
            CheckIfModelEmailsCollectionIsInSync(wrapper);

        }

        [TestMethod]
        public void ShouldBeInSyncAfterRemovingEmail()
        {

            var wrapper = new FriendWrapper(_friend);
            var emailToRemove = wrapper.Emails.Single(em => em.Model == _friendEmail);
            wrapper.Emails.Remove(emailToRemove);
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        [TestMethod]
        public void ShouldBeInSyncAfterAddingEmail()
        {
            _friend.Emails.Remove(_friendEmail);
            var wrapper = new FriendWrapper(_friend);
            wrapper.Emails.Add(new FriendEmailWrapper(_friendEmail));
            CheckIfModelEmailsCollectionIsInSync(wrapper);
        }

        private void CheckIfModelEmailsCollectionIsInSync(FriendWrapper wrapper)
        {
            Assert.AreEqual(wrapper.Emails.Count, _friend.Emails.Count);
            Assert.IsTrue(_friend.Emails.All(e => wrapper.Emails.Any(we => we.Model == e)));
        }
    }
}
