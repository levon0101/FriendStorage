using FriendStorage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendStorage.UI.Wrapper
{
    public class AddressWrapper:ModelWrapper<Address>
    {
        public AddressWrapper(Address model):base(model)
        { 
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id)); // prop only get
        public bool IdIsChanged => GetIsChanged(nameof(Id)); // prop only get


        public string City
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CityOriginalValue => GetOriginalValue<string>(nameof(City)); // prop only get
        public bool CityIsChanged => GetIsChanged(nameof(City)); // prop only get


        public string Street
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string StreetOriginalValue => GetOriginalValue<string>(nameof(Street)); // prop only get
        public bool StreetIsChanged => GetIsChanged(nameof(Street)); // prop only get


        public string StreetNuber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string StreetNuberOriginalValue => GetOriginalValue<string>(nameof(StreetNuber)); // prop only get
        public bool StreetNuberIsChanged => GetIsChanged(nameof(StreetNuber)); // prop only get
         

        public AddressWrapper Address { get; private  set; }
    }
}
