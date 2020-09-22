using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroLibreTargetBehaviour : MonoBehaviour
{
    public MeshRenderer mesh;
    public Collider coll;
    public GameObject particle;
    public int scoreToGrant;
    public AudioSource hitSound;
    public ScoreFeedbackBehaviour scoreFeedback;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            EnableTarget(false);
            GameManagerTiroLibre.gm.BallHit(scoreToGrant);
            hitSound.Play();
            ShowHitParticles();
            ShowScoreFeedback();
            //GameManagerTiroLibre.gm.NextMovement();
            //StartCoroutine(DelayedShow());
        }
    }

    public void ShowScoreFeedback()
    {
        scoreFeedback.Show(gameObject.transform.position);
    }

    public void ShowHitParticles()
    {
        //particle.SetActive(true);
        particle.transform.position = gameObject.transform.position;
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void EnableTarget(bool enabled)
    {
        mesh.enabled = enabled;
        coll.enabled = enabled;
    }

    public IEnumerator DelayedShow()
    {
        yield return new WaitForSeconds(2f);
        EnableTarget(true);
    }

    public IEnumerator DelayedShow(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        EnableTarget(true);
    }
}
