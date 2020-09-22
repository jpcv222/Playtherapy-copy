using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAvatarController : MonoBehaviour
{
    public float speed;
    private Vector3 position;

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && GameManagerTiroLibre.gm.isAbleToMove)
        {
            position = gameObject.transform.position;
            position.x -= speed * Time.deltaTime;
            gameObject.transform.position = position;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && GameManagerTiroLibre.gm.isAbleToMove)
        {
            position = gameObject.transform.position;
            position.x += speed * Time.deltaTime;
            gameObject.transform.position = position;
        }
    }
}
