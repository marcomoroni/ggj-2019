using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerController2D))]
public class PlayerStateMachine : FiniteStateMachine
{
	PlayerController2D _playerController2D;

	public float walkSpeed = 40f;

	private float _horizontalMove = 0f;

	private void Awake()
	{
		InitializeFiniteStateMachine<PlayerStates>(PlayerStates.Normal);
		AddTransitionsToState(PlayerStates.Normal, new Enum[] { PlayerStates.Dialogue });
		AddTransitionsToState(PlayerStates.Dialogue, new Enum[] { PlayerStates.Normal });

		_playerController2D = GetComponent<PlayerController2D>();
	}

	void UpdateNormal()
	{
		_horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
	}

	void FixedUpdateNormal()
	{
		// Move our character
		_playerController2D.Move(_horizontalMove * Time.fixedDeltaTime);
	}
}

public enum PlayerStates
{
	Normal, Dialogue
}