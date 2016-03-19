using Artisan.Toolkit;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Artisan.Interface;

namespace Artisan.Model
{
    public class HomePivotListItem:NotifyPropertyObject,IHomePivotListItem
    {
        private int _tid;

        public int Tid
        {
            get { return _tid; }
            set { UpdateProperty(ref _tid, value);}
        }

        private string _postTime;

        public string PostTime
        {
            get { return _postTime; }
            set { UpdateProperty(ref _postTime, value); }
        }

        private User _user;

        public IUser User
        {
            get { return _user; }
            set { UpdateProperty(ref _user, (User)value);}
        }

        private Work _work;

        public IWork Work
        {
            get { return _work; }
            set { UpdateProperty(ref _work,(Work)value);}
        }

        //private string _text;
        //public string Text
        //{
        //    get { return _text; }
        //    set { UpdateProperty(ref _text, value);}
        //}

        //private string _pics;
        //public string Pics
        //{
        //    get { return _pics; }
        //    set { UpdateProperty(ref _pics, value); }
        //}

        //private UserInfoGeo _geo;
        //public IGeo Geo
        //{
        //    get { return _geo; }
        //    set { UpdateProperty(ref _geo, (UserInfoGeo)value);}
        //}

        //private HomePivotListItemUser _user;

        //public IHomePivotListItemUser User
        //{
        //    get { return _user; }
        //    set { UpdateProperty(ref _user, (HomePivotListItemUser)value);}
        //}
    }
}