using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;

	// Smooth movement
	private Vector3 _velocity = Vector3.zero;
	[Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;  // How much to smooth out the movement

	private bool _facingRight = true;  // For determining which way the player is currently facing.

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void Move(float move)
	{
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);
		// And then smoothing it out and applying it to the character
		_rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !_facingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && _facingRight)
		{
			// ... flip the player.
			Flip();
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_facingRight = !_facingRight;

		transform.Rotate(0f, 180f, 0f);
	}
}
