using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using UnityEngine.UI;

public class FinalAnimationManagerRieles : MonoBehaviour
{
    public GameObject chest;
    public float chestScaleTime;
    public GameObject chestTop;
    public float chestTopOpenTime;
    public float chestTopOpenEndAngle;
    public GameObject left;
    public GameObject right;
    public GameObject front;
    public GameObject back;
    public GameObject coinHolder;
    public GameObject coin;
    public GameObject referenceCoin;
    public float coinSpawnHeightOffset;
    public float startTimeBetweenCoins;
    public GameObject finalAnimationPanel;
    public float panelScaleTime;
    public Text scoreText;
    public GameObject resultsPanel;

    private int score = 100;

    
    private void Start()
    {
        chest.transform.localScale = Vector3.zero;
        chestTop.transform.localRotation = Quaternion.identity;
        //FinalAnimation(); // test
        //SpawnCoins();
    }

    public void FinalAnimation()
    {
        chest.SetActive(true);
        ChestScaleAnimation();
    }

    public void FinalAnimation(int finalScore)
    {
        this.score = finalScore;
        ChestScaleAnimation();
    }

    private void ChestScaleAnimation()
    {
        chest.SetActive(true);
        Vector3 startScale = chest.transform.localScale;
        Vector3 endScale = Vector3.one;
        chest.Tween("ScaleChest", startScale, endScale, chestScaleTime, TweenScaleFunctions.CubicEaseInOut, (t) =>
        {
            // progress
            chest.transform.localScale = t.CurrentValue;
        }, (t) =>
        {
            //completion
            PanelScaleAnimation();
        });
    }

    private void PanelScaleAnimation()
    {
        finalAnimationPanel.SetActive(true);
        scoreText.text = "X " + score.ToString();
        Vector3 startScale = finalAnimationPanel.transform.localScale;
        Vector3 endScale = Vector3.one;

        finalAnimationPanel.Tween("ScalePanel", startScale, endScale, panelScaleTime, TweenScaleFunctions.CubicEaseInOut, (t) =>
        {
            // progress
            finalAnimationPanel.transform.localScale = t.CurrentValue;
        }, (t) =>
        {
            //completion
            ChestTopOpenAnimation();
        });
    }

    private void ChestTopOpenAnimation()
    {
        float startAngle = chestTop.transform.rotation.eulerAngles.z;
        float endAngle = chestTopOpenEndAngle;
        chestTop.GetComponent<AudioSource>().Play();
        chest.Tween("RotateChestTop", startAngle, endAngle, chestTopOpenTime, TweenScaleFunctions.CubicEaseInOut, (t) =>
        {
            // progress
            chestTop.transform.localRotation = Quaternion.identity;
            chestTop.transform.Rotate(chestTop.transform.right, -t.CurrentValue);
        }, (t) =>
        {
            //completion
            StartCoroutine(SpawnCoins());
        });
    }

    private IEnumerator SpawnCoins()
    {
        float timeStep = startTimeBetweenCoins / score;
        Vector3 coinSize = referenceCoin.GetComponent<Collider>().bounds.size;

        float leftBound = left.transform.position.x + (coinSize.x / 2);
        float rightBound = right.transform.position.x - (coinSize.x / 2);
        float frontBound = front.transform.position.z + (coinSize.y / 2);
        float backBound = back.transform.position.z - (coinSize.y / 2);
        float bottomBound = front.GetComponent<Collider>().bounds.max.y + coinSpawnHeightOffset;
        float topBound = bottomBound + coinSpawnHeightOffset;

        //Debug.Log(leftBound + " - " + rightBound + " - " + frontBound + " - " + backBound + " - " + coinSize.x + " - " + coinSize.y + " - " + coinSize.z);

        Vector3 randomPosition = Vector3.zero;
        Vector3 randomRotation = Vector3.zero;

        for (int i = 1; i <= score; i++)
        {
            randomPosition.x = Random.Range(leftBound, rightBound);
            randomPosition.y = Random.Range(bottomBound, topBound);
            randomPosition.z = Random.Range(frontBound, backBound);
            randomRotation.x = Random.Range(0f, 360f);
            randomRotation.y = Random.Range(0f, 360f);
            randomRotation.z = Random.Range(0f, 360f);

            GameObject obj = Instantiate(coin, coinHolder.transform) as GameObject;
            obj.transform.localRotation = Quaternion.Euler(randomRotation);
            obj.transform.position = randomPosition;
            obj.GetComponent<AudioSource>().Play();
            scoreText.text = "X " + (score - i).ToString();
            yield return new WaitForSeconds(startTimeBetweenCoins);
            startTimeBetweenCoins -= timeStep;
        }

        yield return new WaitForSeconds(1f);

        finalAnimationPanel.SetActive(false);
        resultsPanel.SetActive(true);

        // playlist block
        if (PlaylistManager.pm != null && PlaylistManager.pm.active)
            PlaylistManager.pm.NextGame();
    }
}
