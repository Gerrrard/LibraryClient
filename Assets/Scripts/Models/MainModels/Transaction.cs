using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transaction : IQueryObject
{
    private int id;
    private int bookId;
    private int userId;
    private DateTime issueDate;
    private DateTime returnDate;

    public Transaction() { }

    public IDictionary<string, string> GetDictInfo()
    {
        IDictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary["id"] = id.ToString();
        dictionary["book_id"] = bookId.ToString();
        dictionary["user_id"] = userId.ToString();
        dictionary["issue_date"] = issueDate.Date.ToString("yyyy-MM-dd");
        dictionary["return_date"] = returnDate.Date.ToString("yyyy-MM-dd");

        return dictionary;
    }

    public void SetFromDict(IDictionary<string, string> dict)
    {
        id = int.Parse(dict["id"]);
        bookId = int.Parse(dict["book_id"]);
        userId = int.Parse(dict["user_id"]);
        issueDate = DateTime.Parse(dict["issue_date"]).Date;
        if(string.IsNullOrEmpty(dict["return_date"]))
            returnDate = DateTime.MinValue;
        else
            returnDate = DateTime.Parse(dict["return_date"]).Date;
    }
}
