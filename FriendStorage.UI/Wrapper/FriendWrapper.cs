﻿using FriendStorage.Model;
using FriendStorage.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FriendStorage.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Friend>
    {
        public FriendWrapper(Friend model) : base(model)
        {
            InitializeComplexPropertes(model);
            InitializeCollectionPropertes(model);
        }

        private void InitializeCollectionPropertes(Friend model)
        {
            if (model.Emails == null)
            {
                throw new ArgumentException("Emails can't be Null");
            }
            Emails = new ObservableCollection<FriendEmailWrapper>(
                         model.Emails.Select(e => new FriendEmailWrapper(e)));
            RegisterCollection(Emails, model.Emails);

        }

       


        private void InitializeComplexPropertes(Friend model)
        {
            if (model.Address == null)
            {
                throw new ArgumentException("Address can't be Null");
            }
            Address = new AddressWrapper(model.Address);
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int FriendGroupId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public DateTime? BirthDay
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public bool IsDeveloper
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public AddressWrapper Address { get; private set; }

        public ObservableCollection<FriendEmailWrapper> Emails { get; private set; }
        //public ObservableCollection<FriendEmailWrapper> Emails { get; private set; }
    }
}
