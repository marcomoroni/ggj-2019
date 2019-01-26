using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerController2D))]
public class PlayerStateMachine : FiniteStateMachine
{
	PlayerController2D _playerController2D;
	Animator _animator;
	public DialogueManager dialogueManager;

	public float walkSpeed = 40f;

	private float _horizontalMove = 0f;

	public DialogueID? dialogueAvailable = null;

	private void Awake()
	{
		InitializeFiniteStateMachine<PlayerStates>(PlayerStates.Normal);
		AddTransitionsToState(PlayerStates.Normal, new Enum[] { PlayerStates.Dialogue });
		AddTransitionsToState(PlayerStates.Dialogue, new Enum[] { PlayerStates.Normal });

		_playerController2D = GetComponent<PlayerController2D>();
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		PlayerInteractionArea.triggerDialogueAreaEnter.AddListener(OnInteractionAreaEnter);
		PlayerInteractionArea.triggerDialogueAreaExit.AddListener(OnInteractionAreaExit);
		DialogueManager.dialogueFinished.AddListener(OnDialogueFinished);
	}

	void UpdateNormal()
	{
		_horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;

		if (dialogueAvailable != null && Input.GetKeyDown("space"))
		{
			dialogueManager.StartDialogue(dialogueAvailable.GetValueOrDefault());
			ChangeCurrentState(PlayerStates.Dialogue);
		}
	}

	void FixedUpdateNormal()
	{
		// Move our character
		_playerController2D.Move(_horizontalMove * Time.fixedDeltaTime);
	}

	protected override void Update()
	{
		base.Update();

		_animator.SetFloat("velocity", Math.Abs(_horizontalMove));
	}

	private void OnInteractionAreaEnter(DialogueID dialogueID)
	{
		dialogueAvailable = dialogueID;
	}

	private void OnInteractionAreaExit(DialogueID dialogueID)
	{
		dialogueAvailable = null;
	}

	private void OnDialogueFinished()
	{
		dialogueAvailable = null;
		ChangeCurrentState(PlayerStates.Normal);
	}
}

public enum PlayerStates
{
	Normal, Dialogue
}