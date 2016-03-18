using System.Collections.ObjectModel;
using Artisan.Toolkit;
using System;
using Artisan.Model;

namespace Artisan.ViewModel
{
    public class PostDetailPageViewModel:NotifyPropertyObject
    {
        private ObservableCollection<AuthorOtherPostListItem> _authorOtherPostList =
            new ObservableCollection<AuthorOtherPostListItem>();

        public ObservableCollection<AuthorOtherPostListItem> AuthorOtherPostList
        {
            get { return _authorOtherPostList; }
            set
            {
                if (_authorOtherPostList != value)
                    UpdateProperty(ref _authorOtherPostList, value);
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { UpdateProperty(ref _name, value); }
        }

        private string _pics;

        public string Pics
        {
            get { return _pics; }
            set { UpdateProperty(ref _pics,value);}
        }



        public PostDetailPageViewModel()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            for (int j = 1; j <= 4; j++)
            {
                string str = "../Assets/img/" + random.Next(1, 7).ToString() + ".jpg";
                AuthorOtherPostList.Add(new AuthorOtherPostListItem
                {
                    PostSource = str
                });
            }
        }

    }

}