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
    public class ChangeTrackingComplexProperty
    {
        private Friend _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new Friend
            {
                FirstName = "Levon",
                Address = new Address() { City = "Yerevan" },
                Emails = new List<FriendEmail>()

            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendWrapper()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Stepanakert";
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.Address.City = "Yerevan";
            Assert.IsFalse(wrapper.IsChanged);

        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyOfFriendWrapper()
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
            wrapper.Address.City = "Gyumri";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Vanadzor";
            Assert.AreEqual("Yerevan", wrapper.Address.CityOriginalValue);
            //Assert.IsTrue(wrapper.Address.CityIsChanged);
            //Assert.IsTrue(wrapper.Address.IsChanged);

            wrapper.AcceptChanges();

            Assert.AreEqual("Vanadzor", wrapper.Address.City);
            Assert.AreEqual("Vanadzor", wrapper.Address.CityOriginalValue); //After AcceptChanges() all save
            //Assert.IsFalse(wrapper.Address.CityIsChanged);
            //Assert.IsFalse(wrapper.Address.IsChanged);
        }
        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Vanadzor";
            Assert.AreEqual("Yerevan", wrapper.Address.CityOriginalValue);
            //Assert.IsTrue(wrapper.Address.CityIsChanged);
            //Assert.IsTrue(wrapper.Address.IsChanged);

            wrapper.RejectChanges();

            Assert.AreEqual("Yerevan", wrapper.Address.City);
            Assert.AreEqual("Yerevan", wrapper.Address.CityOriginalValue); //After AcceptChanges() all save
            //Assert.IsFalse(wrapper.Address.CityIsChanged);
            //Assert.IsFalse(wrapper.Address.IsChanged);
        }
    }
}
