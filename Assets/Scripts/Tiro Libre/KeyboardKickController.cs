using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKickController : MonoBehaviour
{
    public Kick kickScript;

	// Use this for initialization
	void Start ()
    {
        if (kickScript == null)
            kickScript = gameObject.GetComponent<Kick>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (kickScript != null && GameManagerTiroLibre.gm.targetReady)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(0);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(1);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(2);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(3);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(4);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(5);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(6);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(7);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            {
                kickScript.kicking = false;

                kickScript.CalculatedTargetPosition = GameManagerTiroLibre.gm.getTargetPosition(8);

                kickScript.kicked = true;
                kickScript.hitSound.Play();
            }
        }
	}
}
