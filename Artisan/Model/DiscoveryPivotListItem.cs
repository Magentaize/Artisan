using Artisan.Toolkit;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Artisan.Interface;

namespace Artisan.Model
{
    public class DiscoveryPivotListItem:NotifyPropertyObject, IDiscoveryPivotListItem
    {
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
            set { UpdateProperty(ref _work, (Work)value);}
        }
        //private int _id;
        //public int Id
        //{
        //    get { return _id; }
        //    set { UpdateProperty(ref _id, value);}
        //}

        //private string _intro;
        //public string Intro
        //{
        //    get { return _intro; }
        //    set { UpdateProperty(ref _intro, value); }
        //}

        //private string _pic;
        //public string Pic
        //{
        //    get { return _pic; }
        //    set { UpdateProperty(ref _pic, value); }
        //}

        //private string _title;
        //public string Title
        //{
        //    get { return _title;}
        //    set { UpdateProperty(ref _title, value);}
        //}


    }
}