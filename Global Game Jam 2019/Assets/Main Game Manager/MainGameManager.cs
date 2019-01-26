using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainGameManager : FiniteStateMachine
{
	public PlayerStateMachine player;
	public Image overlay;
	public GameObject introCamera;
	public DialogueManager dialogueManager;

	public static UnityEvent introFinished = new UnityEvent();

	private void Awake()
	{
		overlay.gameObject.SetActive(true);

		InitializeFiniteStateMachine<MainGameManagerState>(MainGameManagerState.Intro);
		AddTransitionsToState(MainGameManagerState.Intro, new Enum[] { MainGameManagerState.Playing });
	}

	private void Start()
	{
		StartCoroutine(IntroCutscene());
	}

	private IEnumerator IntroCutscene()
	{
		yield return new WaitForSeconds(1.0f);

		dialogueManager.StartDialogue(DialogueID.Intro, true);

		yield return new WaitForSeconds(10.0f);

		introCamera.SetActive(false);

		StartCoroutine(FadeOutOverlay());

		yield return new WaitForSeconds(3.0f);

		ChangeCurrentState(MainGameManagerState.Playing);
		introFinished.Invoke();
	}

	private IEnumerator FadeOutOverlay()
	{
		while (overlay.color.a > 0)
		{
			//overlay.color.a -= 0.2f * Time.deltaTime;
			overlay.color = new Color(
				0.07450981f,
				0.03921569f,
				0.1568628f,
				overlay.color.a - 0.2f * Time.deltaTime);

			yield return null;
		}
	}
}

public enum MainGameManagerState
{
	Intro,
	Playing
}