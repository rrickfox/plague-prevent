using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsClass
{
    public string title;
    public string message;

    public NewsClass(string title, string message)
    {
        this.title = title;
        this.message = message;
    }

    public NewsClass() { }
}

public class InfectedNews : NewsClass
{
    public InfectedNews(string stateName)
    {
        title = stateName + " infiziert";
        message = "In " + stateName + " wurde der erste Infizierte gemeldet!";
    }
}
