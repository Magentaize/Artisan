using System.Collections.ObjectModel;
using Artisan.Toolkit;
using System;
using Artisan.Model;

namespace Artisan.ViewModel
{
    public class PostDetailPageViewModel:NotifyPropertyObject
    {
        private string _postSource;

        private ObservableCollection<AuthorOtherPostListItem> _authorOtherPostList =
            new ObservableCollection<AuthorOtherPostListItem>();

        public string PostSource
        {
            get { return _postSource; }
            set { UpdateProperty(ref _postSource,value);}
        }

        public ObservableCollection<AuthorOtherPostListItem> AuthorOtherPostList
        {
            get { return _authorOtherPostList; }
            set
            {
                if(_authorOtherPostList!=value)
                    UpdateProperty(ref _authorOtherPostList,value);
            }
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