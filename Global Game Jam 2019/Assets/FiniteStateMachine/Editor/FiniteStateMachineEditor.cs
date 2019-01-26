using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FiniteStateMachine), true)]
public class FiniteStateMachineEditor : Editor
{
	string currentState = "";

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EditorGUILayout.Space();

		FiniteStateMachine script = (FiniteStateMachine)target;

		EditorGUILayout.LabelField("Finite State Machine Info");
		EditorGUI.indentLevel++;

		// Show current state
		currentState = "";
		if (Application.isPlaying)
		{
			if (currentState != null)
			{
				currentState = script.CurrentState.ToString();
			}
		}
		EditorGUILayout.LabelField("Current state", currentState);

		EditorGUI.indentLevel--;

		// Force repaint
		if (EditorApplication.isPlaying)
			Repaint();
	}
}
