﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public enum DialogueID
{
	WithBear,
	WithButterfly
}

public class DialogueManager : MonoBehaviour
{
	public TextMeshProUGUI textDisplay;

	public Dictionary<DialogueID, Dialogue> dialogues = new Dictionary<DialogueID, Dialogue>();

	public float typeTime = 0.2f;

	public static UnityEvent dialogueStarted = new UnityEvent();
	public static UnityEvent dialogueFinished = new UnityEvent();

	// Speakers
	private readonly Speaker playerSpeaker = new Speaker("Triangle", new Color(.898f, .713f, 1));
	private readonly Speaker bearSpeaker = new Speaker("Bear", new Color(.490f, .407f, 1f));
	private readonly Speaker butterflySpeaker = new Speaker("Butterfly", new Color(.850f, .231f, .470f));

	private void Awake()
	{
		// Create all the dialogues
		dialogues.Add(DialogueID.WithBear, new Dialogue());
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "Hello"));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "Hello to you"));

		dialogues.Add(DialogueID.WithButterfly, new Dialogue());
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(playerSpeaker, "Hi"));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(butterflySpeaker, "Hi to you"));
	}

	public void StartDialogue(DialogueID dialogueID)
	{
		dialogueStarted.Invoke();
		StartCoroutine(TypeDialogue(dialogues[dialogueID]));
	}

	IEnumerator TypeDialogue(Dialogue dialogue)
	{
		foreach (Sentence sentence in dialogue.sentences)
		{
			textDisplay.color = sentence.speaker.color;

			// Print name only if not player
			if (sentence.speaker != playerSpeaker)
			{
				textDisplay.text = sentence.speaker.name + ":  ";
				yield return new WaitForSeconds(typeTime);
			}

			foreach (char letter in sentence.sentence)
			{
				textDisplay.text += letter;
				if (letter != ' ') // don't wait if space
				yield return new WaitForSeconds(typeTime);
			}

			yield return new WaitForSeconds(1f);
			textDisplay.text += "   >";

			yield return new WaitUntil(() => { return Input.GetKeyDown("space"); });
		}

		textDisplay.text = "";
		dialogueFinished.Invoke();
	}
}

public class Speaker
{
	public string name;
	public Color color;

	public Speaker(string name, Color color)
	{
		this.name = name;
		this.color = color;
	}
}

public class Sentence
{
	public Speaker speaker;
	public string sentence;

	public Sentence(Speaker speaker, string sentence)
	{
		this.speaker = speaker;
		this.sentence = sentence;
	}
}

public class Dialogue
{
	public List<Sentence> sentences = new List<Sentence>();
}

