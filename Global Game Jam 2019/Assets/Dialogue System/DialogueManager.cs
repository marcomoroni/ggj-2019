using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public enum DialogueID
{
	Intro,
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

		dialogues.Add(DialogueID.Intro, new Dialogue());
		dialogues[DialogueID.Intro].sentences.Add(new Sentence(playerSpeaker, "Whoopsiedaisies."));
		dialogues[DialogueID.Intro].sentences.Add(new Sentence(playerSpeaker, "I lost my way home."));
		dialogues[DialogueID.Intro].sentences.Add(new Sentence(playerSpeaker, "I'd like to go home."));

		dialogues.Add(DialogueID.WithBear, new Dialogue());
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "Hello."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "Hello to you."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "I lost my way home."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "Do you know how can I get home?"));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "Where do you live?"));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "At home."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "I'm afraid I don't know where it is."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "Butterfly knows a lot of things."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "Perhaps Butterfly knows where your home is."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "Thank you."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "You're welcome."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "Do you like honey?"));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "Yes I do."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "I hope you will find some honey today."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "Thank you."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "You're welcome."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(playerSpeaker, "I have to go now. Goodbye."));
		dialogues[DialogueID.WithBear].sentences.Add(new Sentence(bearSpeaker, "Goodbye to you."));

		dialogues.Add(DialogueID.WithButterfly, new Dialogue());
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(playerSpeaker, "Hello."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(butterflySpeaker, "Hello."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(playerSpeaker, "Are you butterfly?"));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(butterflySpeaker, "Yes I am."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(playerSpeaker, "Do you know how can I get home?"));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(butterflySpeaker, "Where do you live?"));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(playerSpeaker, "At home."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(butterflySpeaker, "I know where it is."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(butterflySpeaker, "Keep walking and you'll find it."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(playerSpeaker, "Thank you. You really know a lot of things."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(butterflySpeaker, "Thank you."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(playerSpeaker, "You're welcome."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(playerSpeaker, "I have to go now. Goodbye."));
		dialogues[DialogueID.WithButterfly].sentences.Add(new Sentence(butterflySpeaker, "Goodbye."));
	}

	public void StartDialogue(DialogueID dialogueID, bool auto = false)
	{
		dialogueStarted.Invoke();
		StartCoroutine(TypeDialogue(dialogues[dialogueID], auto));
	}

	IEnumerator TypeDialogue(Dialogue dialogue, bool auto)
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

			if (auto)
				yield return new WaitForSeconds(3f);
			else
			{
				yield return new WaitForSeconds(0.6f);
				textDisplay.text += "   >";

				yield return new WaitUntil(() => { return Input.GetKeyDown("space"); });
			}

			textDisplay.text = "";
		}
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

