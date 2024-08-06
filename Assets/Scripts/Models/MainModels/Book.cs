using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : IQueryObject
{
    private int id;
    private string title;
    private string author;
    private int year;
    private string genre;

    public Book() { }

    public IDictionary<string, string> GetDictInfo()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary["id"] = id.ToString();
        dictionary["title"] = title;
        dictionary["author"] = author;
        dictionary["year"] = year.ToString();
        dictionary["genre"] = genre;

        return dictionary;
    }

    public void SetFromDict(IDictionary<string, string> dict)
    {
        id = int.Parse(dict["id"]);
        title = dict["title"];
        author = dict["author"];
        year = int.Parse(dict["year"]);
        genre = dict["genre"];
    }
}
