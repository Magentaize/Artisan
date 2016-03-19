using Artisan.Interface;
using Artisan.Toolkit;

namespace Artisan.Model
{
    public class UserInfo:NotifyPropertyObject,IUserInfo
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
            set { UpdateProperty(ref _nickname, value);}
        }

        private string _headPic;

        public string HeadPic
        {
            get { return _headPic; }
            set { UpdateProperty(ref _headPic, value);}
        }

        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set { UpdateProperty(ref _gender, value);}
        }

        private UserInfoGeo _geo;

        public IGeo Geo
        {
            get { return _geo; }
            set { UpdateProperty(ref _geo, (UserInfoGeo)value);}
        }

        private string _intro;

        public string Intro
        {
            get { return _intro; }
            set { UpdateProperty(ref _intro, value);}
        }

        private bool _isFollow;

        public bool IsFollow
        {
            get { return _isFollow; }
            set { UpdateProperty(ref _isFollow, value);}
        }

        private int _works;

        public int Works
        {
            get { return _works; }
            set { UpdateProperty(ref _works, value); }
        }

        private int _article;

        public int Article
        {
            get { return _article; }
            set { UpdateProperty(ref _article, value); }
        }

        private int _follows;

        public int Follows
        {
            get { return _follows; }
            set { UpdateProperty(ref _follows, value); }
        }

        private int _fans;

        public int Fans
        {
            get { return _fans; }
            set { UpdateProperty(ref _fans, value); }
        }
    }

    public class UserInfoGeo : NotifyPropertyObject, IGeo
    {
        private string _city;

        public string City
        {
            get { return _city; }
            set { UpdateProperty(ref _city, value);}
        }

        private string _province;

        public string Province
        {
            get { return _province; }
            set { UpdateProperty(ref _province ,value);}
        }
    }
}