using FriendStorage.Model;
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
            Emails = new ChangeTrackingCollections<FriendEmailWrapper>(
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
            RegisterComplex(Address);
        }


        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id)); // prop only get
        public bool IdIsChanged => GetIsChanged(nameof(Id)); // prop only get


        public int FriendGroupId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public int FriendGroupIdOriginalValue => GetOriginalValue<int>(nameof(FriendGroupId)); // prop only get
        public bool FriendGroupIdIsChanged => GetIsChanged(nameof(FriendGroupId)); // prop only get


        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string FirstNameOriginalValue => GetOriginalValue<string>(nameof(FirstName)); // prop only get
        public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName)); // prop only get


        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastNameOriginalValue => GetOriginalValue<string>(nameof(LastName)); // prop only get
        public bool LastNameIsChanged => GetIsChanged(nameof(LastName)); // prop only get


        public DateTime? BirthDay
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }

        public DateTime? BirthDayOriginalValue => GetOriginalValue<DateTime?>(nameof(BirthDay)); // prop only get
        public bool BirthDayIsChanged => GetIsChanged(nameof(BirthDay)); // prop only get


        public bool IsDeveloper
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool IsDeveloperOriginalValue => GetOriginalValue<bool>(nameof(IsDeveloper)); // prop only get
        public bool IsDeveloperIsChanged => GetIsChanged(nameof(IsDeveloper)); // prop only get


        public AddressWrapper Address { get; private set; }

        public ChangeTrackingCollections<FriendEmailWrapper> Emails { get; private set; }
        //public ObservableCollection<FriendEmailWrapper> Emails { get; private set; }
    }
}
