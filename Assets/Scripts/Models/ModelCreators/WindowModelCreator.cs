using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refused to use, too much for the task
public class WindowModelCreator
{
    public BooksWindow CreateBooksWindow()
    {
        var booksWindow = new BooksWindow();
        booksWindow.InitWindowModel(WindowSelected.books);

        return booksWindow;
    }

    public UsersWindow CreateUsersWindow()
    {
        var usersWindow = new UsersWindow();
        usersWindow.InitWindowModel(WindowSelected.users);

        return usersWindow;
    }

    public TransactionsWindow CreateTransactionsWindow()
    {
        var transactionsWindow = new TransactionsWindow();
        transactionsWindow.InitWindowModel(WindowSelected.transactions);

        return transactionsWindow;
    }
}
