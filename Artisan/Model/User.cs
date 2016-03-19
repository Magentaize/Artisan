using Artisan.Interface;
using Artisan.Toolkit;

namespace Artisan.Model
{
    public class User:NotifyPropertyObject,IUser
    {
        private string _uid;

        public string Uid
        {
            get { return _uid; }
            set { UpdateProperty(ref _uid, value); }
        }

        private string _nickname;

        public string NickName
        {
            get { return _nickname; }
            set { UpdateProperty(ref _nickname, value); }
        }

        private string _headPic;

        public string HeadPic
        {
            get { return _headPic; }
            set { UpdateProperty(ref _headPic, value); }
        }

        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set { UpdateProperty(ref _gender, value); }
        }

        private UserInfoGeo _geo;

        public IGeo Geo
        {
            get { return _geo; }
            set { UpdateProperty(ref _geo, (UserInfoGeo)value); }
        }

        private string _intro;

        public string Intro
        {
            get { return _intro; }
            set { UpdateProperty(ref _intro, value); }
        }
    }
}