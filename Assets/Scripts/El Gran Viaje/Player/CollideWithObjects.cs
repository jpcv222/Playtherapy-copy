using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollideWithObjects : MonoBehaviour {

    ScoreHandler handler;
    public CameraShake.Properties testProperties;
    GameObject emmiterCrash;
    GameObject emmiterCoin;
    AudioSource fireSound;
    AudioSource coinSound;
	AudioSource coinMultipleSound;
    bool isInmune = false;
    float timer_inmune = 0;
    float TIME_DEFAULT_INMUNE = 3f;
    void Start ()
    {
        isInmune = false;
        handler = FindObjectOfType<ScoreHandler>();
        emmiterCrash = GameObject.Find("EmmiterCrash");
        
        emmiterCoin = GameObject.Find("EmmiterCoin");
        fireSound = GameObject.Find("FireSound").GetComponent<AudioSource>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
		coinMultipleSound = GameObject.Find("CoinMultipleSound").GetComponent<AudioSource>();
    }
    void Update()
    {
        if (timer_inmune>0)
        {
            timer_inmune -= Time.deltaTime;
            if (timer_inmune<=0)
            {
                isInmune = false;
                timer_inmune = 0;
                makeNormal();
            }
        }


    }
    void makeTransparent()
    {
        Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer render in renders)
        {
            render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 0.5f);
        }

    }
    void makeNormal()
    {
        Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer render in renders)
        {
            render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f);
        }
    }
    void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Clouds":
                print("hi clouds");
               
                break;
            case "Terrain":
               
                if (isInmune == false)
                {
                    emmiterCrash.transform.position = gameObject.transform.position;
                    emmiterCrash.GetComponent<ParticleSystem>().Play(true);
                    fireSound.Play();
                    FindObjectOfType<CameraShake>().StartShake(testProperties);
                    //Destroy(other.gameObject);

                    handler.sum_score(-2);
                    isInmune = true;
                    makeTransparent();
                    timer_inmune = TIME_DEFAULT_INMUNE;
                }
                break;
            case "Planes":
               
                if (isInmune == false)
                {
                    emmiterCrash.transform.position = other.transform.position;
                    emmiterCrash.GetComponent<ParticleSystem>().Play(true);
                    fireSound.Play();
                    FindObjectOfType<CameraShake>().StartShake(testProperties);
                    Destroy(other.gameObject);
                    handler.sum_score(-2);
                    isInmune = true;
                    makeTransparent();
                    timer_inmune = TIME_DEFAULT_INMUNE;
                }
                break;
            case "Airballoon":
                

                if (isInmune == false)
                {
                    emmiterCrash.transform.position = other.transform.position;
                    emmiterCrash.GetComponent<ParticleSystem>().Play(true);
                    fireSound.Play();
                    FindObjectOfType<CameraShake>().StartShake(testProperties);
                    Destroy(other.gameObject);
                    handler.sum_score(-1);
                    isInmune = true;
                    timer_inmune = TIME_DEFAULT_INMUNE;
                    makeTransparent();
                }
                break;                
            case "Coins":
				handler.rubies_caught++;
				coinMultipleSound.Play();
                emmiterCoin.transform.position = other.transform.position;
                emmiterCoin.GetComponent<ParticleSystem>().Play(true);
                Destroy(other.gameObject);
                handler.sum_score(5);

                break;

            default:
                break;
        }

    }
}
