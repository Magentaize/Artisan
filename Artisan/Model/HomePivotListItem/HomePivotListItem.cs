using Artisan.Toolkit;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Artisan.Interface;

namespace Artisan.Model
{
    public class HomePivotListItem:NotifyPropertyObject,IHomePivotListItem
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { UpdateProperty(ref _id, value);}
        }

        private string _creatTime;
        public string CreatTime
        {
            get { return _creatTime; }
            set { UpdateProperty(ref _creatTime, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { UpdateProperty(ref _text, value);}
        }

        private string _pics;
        public string Pics
        {
            get { return _pics; }
            set { UpdateProperty(ref _pics, value); }
        }

        private UserInfoGeo _geo;
        public IGeo Geo
        {
            get { return _geo; }
            set { UpdateProperty(ref _geo, (UserInfoGeo)value);}
        }

        private HomePivotListItemUser _user;

        public IHomePivotListItemUser User
        {
            get { return _user; }
            set { UpdateProperty(ref _user, (HomePivotListItemUser)value);}
        }
    }
}