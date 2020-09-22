using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField]
    float shake = 0f;
    [SerializeField]
    float shakeAmount = 0.7f;
    [SerializeField]
    float decreaseFactor = 1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (shake > 0)
        {
            transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
        }
    }

    public void Shake(float shake)
    {
        this.shake = shake;
    }
}
