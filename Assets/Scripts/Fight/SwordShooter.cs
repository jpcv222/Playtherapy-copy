﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShooter : MonoBehaviour {

    public GameObject Proyectile;
    public GameObject[] array;
    public GameObject Objective;


    // Use this for initialization
    void Start()
    {


        array = GameObject.FindGameObjectsWithTag("Robot");
        Objective = array[0];

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "RightSword")
        {
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Proyectile, this.transform.position, new Quaternion(0f, -1f, 0f, 1f)) as GameObject;
            var vector = new Vector3(-(float)(other.transform.position.x - Objective.transform.position.x), -(float)(other.transform.position.y - Objective.transform.position.y), (float)(-Objective.transform.position.z)).normalized * 50;//force

            Temporary_Bullet_Handler.GetComponent<Rigidbody>().velocity = vector;

            print("espada derecha");
        }

        if (other.tag == "LeftSword")
        {
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Proyectile, this.transform.position, new Quaternion(0f, -1f, 0f, 1f)) as GameObject;
            var vector = new Vector3(-(float)(other.transform.position.x - Objective.transform.position.x), -(float)(other.transform.position.y - Objective.transform.position.y), (float)(-Objective.transform.position.z)).normalized * 50;//force

            Temporary_Bullet_Handler.GetComponent<Rigidbody>().velocity = vector;

            print("espada derecha");
        }

        


    }
}
