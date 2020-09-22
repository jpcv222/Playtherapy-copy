using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour
{
    [SerializeField] float time = 0.5f;
    [SerializeField] float distance = 4f;
    [SerializeField] float timeUp = 1f;
    [SerializeField] ParticleSystem upParticles;
    private float currentTime;
    private Vector3 startPosition;
    private ITween currentTween;
    public bool isUp;
    public bool isMoving;
    public ScoreFeedbackBehaviourMoles feedback;

    // Use this for initialization
    void Start ()
    {
        isUp = false;
        isMoving = false;
        startPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Animation(time, distance);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Animation(time, -distance);

        currentTime -= Time.deltaTime;

        if (!isMoving && isUp && currentTime <= 0)
            Down();
	}

    public void Up()
    {
        if (!isMoving && !isUp)
        {
            currentTime = GameManagerMoles.gm.moleUptime;
            isMoving = true;
            Animation(time, distance);
            upParticles.Stop();
            upParticles.Play();
        }
    }

    public void Down()
    {
        isMoving = true;
        Animation(time, -distance);
    }

    public void Animation(float time, float distance)
    {
        float startY = transform.localPosition.y;
        float finalY = startY + distance;

        currentTween = gameObject.Tween(gameObject.GetHashCode().ToString(), startY, finalY, time, TweenScaleFunctions.CubicEaseInOut, 
            (t) =>
            {
                Vector3 v = transform.localPosition;
                v.y = t.CurrentValue;
                transform.localPosition = v;
            }, 
            (t) =>
            {
                isMoving = false;
                if (startY < finalY)
                    isUp = true;
                else
                {
                    isUp = false;
                    MoleMiss();
                }
            }
        );
    }

    public void ResetMole()
    {
        if (currentTween != null)
            currentTween.Stop(TweenStopBehavior.DoNotModify);
        transform.localPosition = startPosition;
        isMoving = false;
        isUp = false;
    }

    public void MoleMiss()
    {
        GetComponent<AudioSource>().Play();
        GameManagerMoles.gm.UpdateScore(-1);
        feedback.Bad(transform);
    }
}
