using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHint : MonoBehaviour
{
SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		PlayerInteractionArea.triggerDialogueAreaEnter.AddListener(OnInteractionAreaEnter);
		PlayerInteractionArea.triggerDialogueAreaExit.AddListener(OnInteractionAreaExit);
		DialogueManager.dialogueStarted.AddListener(OnDialogueStart);
	}

	private void OnInteractionAreaEnter(DialogueID dialogueID)
	{
		spriteRenderer.enabled = true;
	}

	private void OnInteractionAreaExit(DialogueID dialogueID)
	{
		spriteRenderer.enabled = false;
	}

	private void OnDialogueStart()
	{
		spriteRenderer.enabled = false;
	}
}
