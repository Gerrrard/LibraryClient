using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : IQueryObject
{
    private int id;
    private string name;
    private string address;
    private string phone;

    public User() { }

    public IDictionary<string, string> GetDictInfo()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary["id"] = id.ToString();
        dictionary["name"] = name;
        dictionary["address"] = address;
        dictionary["phone"] = phone;

        return dictionary;
    }

    public void SetFromDict(IDictionary<string, string> dict)
    {
        id = int.Parse(dict["id"]);
        name = dict["name"];
        address = dict["address"];
        phone = dict["phone"];
    }
}
