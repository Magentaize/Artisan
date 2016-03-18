﻿namespace Artisan.Interface
{
    public interface IHomePivotListItem
    {
        //Objects of HomePage pivot ListViewItem
        int Id { get; set; }
        string AuthorName { get; set; } //Set per-post's author's name
        string CreatTime { get; set; } //Set per-post's posted time
        string Text { get; set; } //Set per-post's info
        string Pics { get; set; } //Set per-post's thumbnail image fetch url
    }

    public interface IHomePivotListItemGeo
    {
        string City { get; set; }
        string Province { get;set; }
    }

    public interface IHomePivotListItemUser
    {
        int Uid { get; set; }
        string Name { get; set; }
        string Pic { get; set; }
        int Gender { get; set; }
        string Intro { get; set; }
    }
}