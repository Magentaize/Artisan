using Artisan.Interface;
using Artisan.Toolkit;

namespace Artisan.Model
{
    public class HomePivotListItemUser:NotifyPropertyObject, IHomePivotListItemUser
    {
        private int _uid;

        public int Uid
        {
            get { return _uid; }
            set { UpdateProperty(ref _uid, value);}
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { UpdateProperty(ref _name, value);}
        }

        private string _pic;

        public string Pic
        {
            get { return _pic; }
            set { UpdateProperty(ref _pic, value);}
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
            set { UpdateProperty(ref _geo,(UserInfoGeo)value);}
        }

        private string _intro;

        public string Intro
        {
            get { return _intro; }
            set { UpdateProperty(ref _intro, value);}
        }
    }
}