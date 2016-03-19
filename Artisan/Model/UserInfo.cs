using Artisan.Interface;
using Artisan.Toolkit;

namespace Artisan.Model
{
    /*
    (GET)根据用户ID获取用户信息 http://api.artisan.com/user_info.json
请求参数        必选        类型        描述
------------------------------------------------
uid             true        Str         用户ID

返回结果        类型        描述        备注
------------------------------------------------
uid             Str         用户ID
name            Str         用户昵称
pic             Str         头像地址
gender          int         性别        0: 男, 1: 女, 2: 未知
geo             Object      地理信息
intro           Str         介绍文本
works           int         作品数
article         int         文章数
follows         int         关注数
fans            int         粉丝数

JSON示例 {
    "uid": 1404376560,
    "name": "张三",
    "pic": "http://api.artisan.com/img/KasoLu/profile_head",
    "gender": 0,
    "geo": {
        "city": "西安",
        "province": "陕西",
    },
    "intro": "人生五十年，乃如梦如幻；有生斯有死，壮士复何憾。",
    "works": 20,
    "article": 20,
    "follows": 20,
    "fans": 20
}
    */
    public class UserInfo:NotifyPropertyObject,IUserInfo
    {
        private string _uid;

        public string Uid
        {
            get { return _uid; }
            set { UpdateProperty(ref _uid, value); }
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
            set { UpdateProperty(ref _geo, (UserInfoGeo)value);}
        }

        private string _intro;

        public string Intro
        {
            get { return _intro; }
            set { UpdateProperty(ref _intro, value);}
        }

        private int _works;

        public int Works
        {
            get { return _works; }
            set { UpdateProperty(ref _works, value);}
        }

        private int _article;

        public int Article
        {
            get { return _article; }
            set { UpdateProperty(ref _article ,value);}
        }

        private int _follows;

        public int Follows
        {
            get { return _follows; }
            set { UpdateProperty(ref _follows ,value);}
        }

        private int _fans;

        public int Fans
        {
            get { return _fans; }
            set { UpdateProperty(ref _fans, value);}
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