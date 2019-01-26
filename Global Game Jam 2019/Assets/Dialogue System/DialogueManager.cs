using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public enum DialogueID
{
	Test1
}

public class DialogueManager : MonoBehaviour
{
	public TextMeshProUGUI textDisplay;

	public Dictionary<DialogueID, Dialogue> dialogues = new Dictionary<DialogueID, Dialogue>();

	public float typeTime = 0.2f;

	public UnityEvent dialogueFinished = new UnityEvent();

	private void Awake()
	{
		// Create speakers
		Speaker playerSpeaker = new Speaker("Triangle", new Color(229, 182, 255));
		Speaker npc1 = new Speaker("NPC1", new Color(217, 59, 120));

		// Create all the dialogues
		dialogues.Add(DialogueID.Test1, new Dialogue());
		dialogues[DialogueID.Test1].sentences.Add(new Sentence(playerSpeaker, "Hello"));
		dialogues[DialogueID.Test1].sentences.Add(new Sentence(npc1, "Hello to you"));
	}

	public void StartDialogue(DialogueID dialogueID)
	{
		StartCoroutine(TypeDialogue(dialogues[dialogueID]));
	}

	IEnumerator TypeDialogue(Dialogue dialogue)
	{
		foreach (Sentence sentence in dialogue.sentences)
		{
			textDisplay.text = "";

			textDisplay.color = sentence.speaker.color;

			foreach (char letter in sentence.sentence)
			{
				textDisplay.text += letter;
				yield return new WaitForSeconds(typeTime);
			}

			yield return new WaitUntil(() => { return Input.GetKeyDown("space"); });
		}
	}

	// TEST ONLY
	private void Update()
	{
		// Continue dialogue
		if (Input.GetKeyDown("d"))
		{
			StartDialogue(DialogueID.Test1);
		}
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

