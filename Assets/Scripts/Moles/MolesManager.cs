using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolesManager : MonoBehaviour
{
    public GameObject[] moles;
    private int currentMole;
    public GameObject firstModel;
    public GameObject secondModel;
    public GameObject threeModel;
    public GameObject fourModel;
    public GameObject fiveModel;
    public GameObject sixModel;
    public GameObject sevenModel;
    public GameObject eigthModel;

    //public GameObject scenaryTwo;


    public void NextMole(int scenary)
    {
        if (GameManagerMoles.gm != null && GameManagerMoles.gm.isPlaying && scenary == 1)
        {
            StartCoroutine(NextMoleRoutine(moles, scenary));

        }
        if (GameManagerMoles.gm != null && GameManagerMoles.gm.isPlaying && scenary == 2)
        {
            firstModel.transform.position = transform.position + new Vector3(8, 1.530001f, 18.6f);
            secondModel.transform.position = transform.position + new Vector3(-2.5f, 1.530001f, 18.6f);
            threeModel.transform.position = transform.position + new Vector3(13, 1.530001f, 11f);
            fourModel.transform.position = transform.position + new Vector3(-5f, 1.530001f, 11f);
            fiveModel.transform.position = transform.position + new Vector3(8, 1.530001f, 5f);
            sixModel.transform.position = transform.position + new Vector3(-1.8f, 1.530001f, 5f);
            sevenModel.transform.position = transform.position + new Vector3(3.6f, 1.530001f, 14f);
            eigthModel.transform.position = transform.position + new Vector3(3.6f, 1.530001f, 9f);
            StartCoroutine(NextMoleRoutine(moles, scenary));

        }
        if (GameManagerMoles.gm != null && GameManagerMoles.gm.isPlaying && scenary == 3)
        {
            firstModel.transform.position = transform.position + new Vector3(4, 1.530001f, 18f);
            secondModel.transform.position = transform.position + new Vector3(0.4f, 1.530001f, 11.7f);
            threeModel.transform.position = transform.position + new Vector3(7, 1.530001f, 11.7f);
            fourModel.transform.position = transform.position + new Vector3(-5f, 1.530001f, 7.5f);
            fiveModel.transform.position = transform.position + new Vector3(4, 1.530001f, 5f);
            sixModel.transform.position = transform.position + new Vector3(-5f, 1.530001f, 14.17f);
            sevenModel.transform.position = transform.position + new Vector3(12.4f, 1.530001f, 14.17f);
            eigthModel.transform.position = transform.position + new Vector3(12f, 1.530001f, 7.5f);
            StartCoroutine(NextMoleRoutine(moles, scenary));

        }
    }

    private IEnumerator NextMoleRoutine(GameObject[] level, int scenary)
    {
        currentMole = Random.Range(0, level.Length);
        if (!level[currentMole].GetComponentInChildren<MoleBehaviour>().isUp)
            level[currentMole].GetComponentInChildren<MoleBehaviour>().Up();

        yield return new WaitForSeconds(GameManagerMoles.gm.timeBetweenMoles);

        NextMole(scenary);
    }

}
