using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAnimationMoles : MonoBehaviour
{
    [SerializeField] float cameraRotationTime;
    [SerializeField] float cameraRotationAngle;
    [SerializeField] float moleDistance;
    [SerializeField] float moleTime;
    private GameObject mainCamera;
    public bool isDone;

    private void Start()
    {
        isDone = false;
    }

    public void Begin(GameObject[] moles)
    {
        StartCoroutine(FinalAnimation(moles));
    }

    public IEnumerator FinalAnimation(GameObject[] moles)
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        float startAngle = mainCamera.transform.rotation.eulerAngles.x;
        float finalAngle = startAngle - cameraRotationAngle;
        mainCamera.Tween(mainCamera.GetHashCode().ToString(), startAngle, finalAngle, cameraRotationTime, TweenScaleFunctions.CubicEaseInOut, 
            (t) =>
            {
                Vector3 v = mainCamera.transform.rotation.eulerAngles;
                v.x = t.CurrentValue;
                mainCamera.transform.rotation = Quaternion.Euler(v);
            },
            (t) =>
            {

            });
        
        for (int i = 0; i < moles.Length; i++)
        {
            MoleBehaviour mb = moles[i].GetComponentInChildren<MoleBehaviour>();
            //mb.ResetMole();
            mb.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            float startY = mb.gameObject.transform.localPosition.y;
            float finalY = startY + moleDistance;
            moles[i].Tween(i, startY, finalY, moleTime, TweenScaleFunctions.CubicEaseInOut,
                (t) =>
                {
                    Vector3 v = mb.gameObject.transform.localPosition;
                    v.y = t.CurrentValue;
                    mb.gameObject.transform.localPosition = v;
                },
                (t) =>
                {

                });
        }
        
        yield return new WaitForSeconds(3f);
        isDone = true;
    }
}
