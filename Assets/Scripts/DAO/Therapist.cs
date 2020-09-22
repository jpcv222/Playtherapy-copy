using UnityEngine;
using System.Collections;

public class Therapist
{
    private string id_num;
    private string id_type;
    private string name;
    private string lastname;
    private string genre;
    private string password;

    public Therapist(string id_num, string id_type, string name, string lastname, string genre)
    {
        this.id_num = id_num;
        this.id_type = id_type;
        this.name = name;
        this.lastname = lastname;
        this.genre = genre;
        //this.password = password;
    }

    public string Id_num
    {
        get
        {
            return id_num;
        }

        set
        {
            id_num = value;
        }
    }

    public string Id_type
    {
        get
        {
            return id_type;
        }

        set
        {
            id_type = value;
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

    public string Lastname
    {
        get
        {
            return lastname;
        }

        set
        {
            lastname = value;
        }
    }

    public string Genre
    {
        get
        {
            return genre;
        }

        set
        {
            genre = value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }

        set
        {
            password = value;
        }
    }
}
