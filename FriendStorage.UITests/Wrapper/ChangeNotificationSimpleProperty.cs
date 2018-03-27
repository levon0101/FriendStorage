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
    public class ChangeNotificationSimpleProperty
    {

        private Friend _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new Friend
            {
                FirstName = "Levon",
                Address = new Address(),
                Emails = new List<FriendEmail>()

            };
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventOnPropertyChanged()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FirstName")
                {
                    fired = true;
                }

            };
            wrapper.FirstName = "Yulia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldNotRaisePropertyChangedEventIfPropertySetToSameValue()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FirstName")
                {
                    fired = true;
                }

            };
            wrapper.FirstName = "Levon"; // need to be same as in initialize method
            Assert.IsFalse(fired);
        }
    }
}
