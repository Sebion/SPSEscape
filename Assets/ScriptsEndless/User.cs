using System;
using UnityEngine.Experimental.UIElements;


[Serializable]
public class User
{
    private String username;
    private int score;

    public User(String username, int score)
    {
        this.username = username;
        this.score = score;
    }

    public User()
    {
    }

    public override string ToString()
    {
        return this.username;
    }


    public String getUsername()
    {
        return this.username;
    }

    public int getScore()
    {
        return this.score;
    }
}