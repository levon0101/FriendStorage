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
    public class ChangeTrackingSimpleProperty
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
        public void ShouldStoreOriginalValue()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.AreEqual(_friend.FirstName, wrapper.FirstNameOriginalValue);

            wrapper.FirstName = "Raf";
            Assert.AreEqual("Levon", wrapper.FirstNameOriginalValue);

        }
        [TestMethod]
        public void ShouldSetIsChanged()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.IsFalse(wrapper.FirstNameIsChanged);
            Assert.IsFalse(wrapper.IsChanged);

            wrapper.FirstName = "OkiDoki";
            Assert.IsTrue(wrapper.FirstNameIsChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.FirstName = "Levon"; // back to original valu (from initialize method)
            Assert.IsFalse(wrapper.FirstNameIsChanged);
            Assert.IsFalse(wrapper.IsChanged);
        }




        [TestMethod]
        public void ShouldRaisePropertyChangedEventForFirstNameChanged()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.FirstNameIsChanged))
                {
                    fired = true;
                }

            };
            wrapper.FirstName = "Yulia";
            Assert.IsTrue(fired);
        }


        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChanged()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsChanged))
                {
                    fired = true;
                }

            };
            wrapper.FirstName = "Yulia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.FirstName = "Yana";
            Assert.AreEqual("Yana", wrapper.FirstName);
            Assert.AreEqual("Levon", wrapper.FirstNameOriginalValue); // same in initialize()
            Assert.IsTrue(wrapper.FirstNameIsChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.AcceptChanges();

            Assert.AreEqual("Yana", wrapper.FirstName);
            Assert.AreEqual("Yana", wrapper.FirstNameOriginalValue); //After AcceptChanges() all save
            Assert.IsFalse(wrapper.FirstNameIsChanged);
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.FirstName = "Yana";
            Assert.AreEqual("Yana", wrapper.FirstName);
            Assert.AreEqual("Levon", wrapper.FirstNameOriginalValue); // same in initialize()
            Assert.IsTrue(wrapper.FirstNameIsChanged);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.AreEqual("Levon", wrapper.FirstName);
            Assert.AreEqual("Levon", wrapper.FirstNameOriginalValue); //After AcceptChanges() all save
            Assert.IsFalse(wrapper.FirstNameIsChanged);
            Assert.IsFalse(wrapper.IsChanged);
        }
    }
} 
