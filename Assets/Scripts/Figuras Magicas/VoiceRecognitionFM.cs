using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

public class VoiceRecognitionFM : MonoBehaviour {

	KeywordRecognizer keywordRecognizer;
	Dictionary<string,System.Action> keywords;
	GesturesShapeManager manager;
	PlayerControllerFM player;
	float velocityToImplement = 5;
	// Use this for initialization
	void Start () {
		player=  FindObjectOfType<PlayerControllerFM>(); 
		manager = FindObjectOfType<GesturesShapeManager> ();

		keywords = new Dictionary<string, System.Action> ();


		keywords.Add ("go", onCast);
		keywords.Add ("parar", onStop);
		keywords.Add ("abajo", onDown);
		keywords.Add ("arriba", onUp);
		keywords.Add ("izquierda", onLeft);
		keywords.Add ("dcerecha", onRight);

		keywordRecognizer = new KeywordRecognizer (keywords.Keys.ToArray());
		keywordRecognizer.OnPhraseRecognized += onKeywordRecognize;
		keywordRecognizer.Start ();
	}
	void onKeywordRecognize(PhraseRecognizedEventArgs args)
	{
		System.Action keywordAction;
        print(args.text);
		if (keywords.TryGetValue(args.text,out keywordAction)) {
			if (keywordAction!=null) {
				keywordAction ();
			}

		}
	}
	void onCast()
	{
		print ("just say lanzar hechizo");
		if (manager== null) {
			
		}
		manager.recognizeShape ();
	}
	void onStop()
	{
		player.Move (Vector3.zero.normalized*velocityToImplement);

	}
	void onUp()
	{
		player.Move (Vector3.forward.normalized*velocityToImplement);

	}
	void onDown()
	{
		player.Move (Vector3.back.normalized*velocityToImplement);

	}
	void onRight()
	{
		player.Move (Vector3.right.normalized*velocityToImplement);

	}
	void onLeft()
	{
		player.Move (Vector3.left.normalized*velocityToImplement);

	}
	// Update is called once per frame
	void Update () {
		
	}
}
