using UnityEngine;
using System.Collections;

public class Minigame
{
    private string id;
    private string name;
    private string description;

    public Minigame(string id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
    }

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }
}
