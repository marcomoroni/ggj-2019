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
	public GameObject endText;

	public GameObject wallOutro;

	public static UnityEvent introFinished = new UnityEvent();

	private void Awake()
	{
		overlay.gameObject.SetActive(true);

		InitializeFiniteStateMachine<MainGameManagerState>(MainGameManagerState.Intro);
		AddTransitionsToState(MainGameManagerState.Intro, new Enum[] { MainGameManagerState.Playing });
		AddTransitionsToState(MainGameManagerState.Playing, new Enum[] { MainGameManagerState.Outro });

		OutroTrigger.outroTriggered.AddListener(OnOutroTrigger);
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
			overlay.color = new Color(
				0.07450981f,
				0.03921569f,
				0.1568628f,
				overlay.color.a - 0.2f * Time.deltaTime);

			yield return null;
		}
	}

	private void OnOutroTrigger()
	{
		ChangeCurrentState(MainGameManagerState.Outro);
	}

	void EnterOutro(Enum previousState)
	{
		wallOutro.SetActive(true);
		StartCoroutine(OutroCutscene());
	}

	private IEnumerator OutroCutscene()
	{
		dialogueManager.StartDialogue(DialogueID.Outro, true);

		yield return new WaitForSeconds(5.0f);

		StartCoroutine(FadeInOverlay());
	}

	private IEnumerator FadeInOverlay()
	{
		while (overlay.color.a < 1)
		{
			overlay.color = new Color(
				0.07450981f,
				0.03921569f,
				0.1568628f,
				overlay.color.a + 0.1f * Time.deltaTime);

			yield return null;
		}

		yield return new WaitForSeconds(4.0f); // Guess timing

		endText.SetActive(true);
	}
}

public enum MainGameManagerState
{
	Intro,
	Playing,
	Outro
}