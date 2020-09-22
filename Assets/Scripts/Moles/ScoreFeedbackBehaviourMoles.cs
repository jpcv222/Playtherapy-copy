using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFeedbackBehaviourMoles : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] float duration;
    [SerializeField] GameObject good;
    [SerializeField] GameObject bad;

    public void Good(Transform mole)
    {
        GameObject go = Instantiate(good, transform);
        go.transform.position = mole.position;
        float startY = go.transform.localPosition.y;
        float finalY = startY + distance;
        go.Tween(go.GetHashCode().ToString(), startY, finalY, duration, TweenScaleFunctions.QuadraticEaseInOut,
            (t) =>
            {
                Vector3 v = go.transform.localPosition;
                v.y = t.CurrentValue;
                go.transform.localPosition = v;
            },
            (t) =>
            {
                go.GetComponent<MeshRenderer>().enabled = false;
                Destroy(go);
            }
        );
    }

    public void Bad(Transform mole)
    {
        GameObject go = Instantiate(bad, transform);
        go.transform.position = mole.position;
        float startY = go.transform.localPosition.y;
        float finalY = startY - distance;
        go.Tween(go.GetHashCode().ToString(), startY, finalY, duration, TweenScaleFunctions.QuadraticEaseInOut,
            (t) =>
            {
                Vector3 v = go.transform.localPosition;
                v.y = t.CurrentValue;
                go.transform.localPosition = v;
            },
            (t) =>
            {
                go.GetComponent<MeshRenderer>().enabled = false;
                Destroy(go);
            }
        );
    }
}
