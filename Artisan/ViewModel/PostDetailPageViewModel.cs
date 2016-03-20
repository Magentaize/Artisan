using System.Collections.ObjectModel;
using Artisan.Toolkit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Artisan.Model;
using Artisan.Toolkit.Helper;
using Artisan.Toolkit.Net;

namespace Artisan.ViewModel
{
    public class PostDetailPageViewModel:NotifyPropertyObject
    {
        private ObservableCollection<PostListItem> _authorOtherPostList =
            new ObservableCollection<PostListItem>();

        public ObservableCollection<PostListItem> AuthorOtherPostList
        {
            get { return _authorOtherPostList; }
            set { UpdateProperty(ref _authorOtherPostList, value); }
        }

        private PostListItem _postItem;//当前页中的作者及作品信息，与作者其他作品列表分离

        public PostListItem PostItem
        {
            get { return _postItem; }
            set { UpdateProperty(ref _postItem, value);}
        }

        private string _nickame;

        public string NickName
        {
            get { return _nickame; }
            set { UpdateProperty(ref _nickame, value); }
        }

        private string _pic;

        public string Pic
        {
            get { return _pic; }
            set { UpdateProperty(ref _pic,value);}
        }



        //public PostDetailPageViewModel()
        //{
        //    Random random = new Random((int)DateTime.Now.Ticks);
        //    for (int j = 1; j <= 4; j++)
        //    {
        //        string str = "../Assets/img/" + random.Next(1, 7).ToString() + ".jpg";
        //        AuthorOtherPostList.Add(new AuthorOtherPostListItem
        //        {
        //            PostSource = str,
                    
        //        });
        //    }
        //}

        public async Task<bool?> GetAuthorOtherPostAsync()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("uid", PostItem.User.Uid);
            var targetUri = ResourceLoader.GetForCurrentView().GetString("HostUri") +
                            ResourceLoader.GetForCurrentView().GetString("UserOtherWorkUri");
            var result = await HttpWebPost.GetJsonStringFromUriAsync(targetUri, param);
            if (result == null) return false;

            var items = JsonObjectParser.ParseAuthorOtherPostItem(result);
            foreach (var item in items)
            {
                AuthorOtherPostList.Add(new PostListItem()
                {
                    User = PostItem.User,
                    Work = item.Work
                });
            }
            return AuthorOtherPostList != null;
        } 

    }

}