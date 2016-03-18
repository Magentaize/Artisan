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

        private HomePivotListItemGeo _geo;
        public IGeo Geo
        {
            get { return _geo; }
            set { UpdateProperty(ref _geo, (HomePivotListItemGeo)value);}
        }

        private HomePivotListItemUser _user;

        public IHomePivotListItemUser User
        {
            get { return _user; }
            set { UpdateProperty(ref _user, (HomePivotListItemUser)value);}
        }

        private string _authorName;
        public string AuthorName
        {
            get{ return _authorName; }
            set{ UpdateProperty(ref _authorName, value); }
        }


    }

    public class HomePivotListItemGeo : NotifyPropertyObject, IGeo
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
            set { UpdateProperty(ref _province, value);}
        }
    }
}