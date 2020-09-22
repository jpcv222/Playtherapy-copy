using UnityEngine;
using System.Collections;

public class Movement
{
    private string id;
    private string name;

    public Movement(string id, string name)
    {
        this.Id = id;
        this.Name = name;
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
}
