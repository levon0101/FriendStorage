using FriendStorage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendStorage.UI.Wrapper
{
    public class FriendEmailWrapper : ModelWrapper<FriendEmail>
    {
        public FriendEmailWrapper(FriendEmail model) : base(model)
        {

        }

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id)); // prop only get
        public bool IdIsChanged => GetIsChanged(nameof(Id)); // prop only get


        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string EmailOriginalValue => GetOriginalValue<string>(nameof(Email)); // prop only get
        public bool EmailIsChanged => GetIsChanged(nameof(Email)); // prop only get



        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment)); // prop only get
        public bool CommentIsChanged => GetIsChanged(nameof(Comment)); // prop only get


    }
}
