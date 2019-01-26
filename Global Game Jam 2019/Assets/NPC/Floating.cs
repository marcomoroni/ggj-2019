using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
	[Header("Floating")]
	public Vector2 maxFloatingDistance;
	private Vector2 randFloating;
	private Vector2 floatingSpeed;

	Vector2 startPos;

	private void Start()
	{
		randFloating.x = Random.Range(-3.14f, 3.14f);
		randFloating.y = Random.Range(-3.14f, 3.14f);
		floatingSpeed.x = .5f;
		floatingSpeed.y = .5f;

		startPos = transform.position;
	}

	private void Update()
	{
		transform.position = startPos + new Vector2(
			maxFloatingDistance.x * Mathf.Sin(Time.time * floatingSpeed.x + randFloating.x),
			maxFloatingDistance.y * Mathf.Sin(Time.time * floatingSpeed.y + randFloating.y));
	}
}
