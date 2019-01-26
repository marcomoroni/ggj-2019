// https://unitygem.wordpress.com/state-machine-basic/

using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

/// <summary>
/// Finite State Machine component. Inherits from <c>MonoBehaviour</c>.
/// </summary>
public abstract class FiniteStateMachine : MonoBehaviour
{
	class State
	{
		public Enum name = null;

		public Action updateMethod = () => { };
		public Action fixedUpdateMethod = () => { };
		public Action lateUpdateMethod = () => { };
		public Action<Enum> enterMethod = (state) => { };
		public Action<Enum> exitMethod = (state) => { };
		public bool forceTransition = false;
		public List<Enum> transitions = null;

		public State(Enum name)
		{
			this.name = name;
			this.transitions = new List<Enum>();
		}
	}

	#region Variables

	private Dictionary<Enum, State> states;

	private State currentState = null;
	private bool inTransition = false;
	private bool initialized = false;
	private bool debugMode = false;

	private Action OnUpdate = null;
	private Action OnFixedUpdate = null;
	private Action OnLateUpdate = null;

	/// <summary>
	/// Get the current active state of the finite state machine.
	/// </summary>
	public Enum CurrentState { get { return this.currentState.name; } }

	#endregion

	#region Unity lifecycle

	protected virtual void Update()
	{
		this.OnUpdate();
	}

	protected virtual void FixedUpdate()
	{
		this.OnFixedUpdate();
	}

	protected virtual void LateUpdate()
	{
		this.OnLateUpdate();
	}

	#endregion

	private bool Initialized()
	{
		if (!initialized)
		{
			Debug.LogError(this.GetType().ToString() + ": FiniteStateMachine is not initialized. You need to call InitializeFiniteStateMachine().");
			return false;
		}
		return true;
	}

	private static T GetMethodInfo<T>(object obj, Type type, string method, T Default) where T : class
	{
		Type baseType = type;
		MethodInfo methodInfo = baseType.GetMethod(method, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		if (methodInfo != null)
		{
			return Delegate.CreateDelegate(typeof(T), obj, methodInfo) as T;
		}
		return Default;
	}

	/// <summary>
	/// Initialize the finite state machine. This is better done in <c>Awake()</c>.
	/// </summary>
	/// <typeparam name="T">The enum with the states.</typeparam>
	/// <param name="initialState">The initial state.</param>
	/// <param name="debug">If true, log the what's happening every frame.</param>
	protected void InitializeFiniteStateMachine<T>(Enum initialState, bool debug = false)
	{
		if (this.initialized == true)
		{
			Debug.LogError("The FiniteStateMachine component on " + this.GetType().ToString() + " is already initialized.");
			return;
		}
		this.initialized = true;

		var values = Enum.GetValues(typeof(T));
		this.states = new Dictionary<Enum, State>();
		for (int i = 0; i < values.Length; i++)
		{
			this.initialized = this.CreateNewState((Enum)values.GetValue(i));
		}
		this.currentState = this.states[initialState];
		this.inTransition = false;
		this.debugMode = debug;

		this.currentState.enterMethod(currentState.name);
		this.OnUpdate = this.currentState.updateMethod;
		this.OnFixedUpdate = this.currentState.fixedUpdateMethod;
		this.OnLateUpdate = this.currentState.lateUpdateMethod;
		if (this.debugMode == true)
		{
			Debug.Log("FiniteStateMachine : " + this.GetType().ToString() + " initialized with " + this.currentState.name + " state.");
		}
	}

	private bool CreateNewState(Enum newstate)
	{
		if (this.Initialized() == false) { return false; }
		if (this.states.ContainsKey(newstate) == true)
		{
			Debug.Log("State " + newstate + " is already registered in " + this.GetType().ToString() + ".");
			return false;
		}
		State s = new State(newstate);
		Type type = this.GetType();
		s.enterMethod = FiniteStateMachine.GetMethodInfo<Action<Enum>>(this, type, "Enter" + newstate, DoNothingEnterExit);
		s.updateMethod = FiniteStateMachine.GetMethodInfo<Action>(this, type, "Update" + newstate, DoNothingUpdate);
		s.fixedUpdateMethod = FiniteStateMachine.GetMethodInfo<Action>(this, type, "FixedUpdate" + newstate, DoNothingUpdate);
		s.lateUpdateMethod = FiniteStateMachine.GetMethodInfo<Action>(this, type, "LateUpdate" + newstate, DoNothingUpdate);
		s.exitMethod = FiniteStateMachine.GetMethodInfo<Action<Enum>>(this, type, "Exit" + newstate, DoNothingEnterExit);
		this.states.Add(newstate, s);
		return true;
	}

	/// <summary>
	/// Add the legal states a state can transition into.
	/// </summary>
	/// <param name="sourceState">The state considered.</param>
	/// <param name="transitions">The valid transitions.</param>
	/// <param name="forceTransition">Force transition.</param>
	/// <returns>Return <c>true</c> if the operation is completed correctly.</returns>
	protected bool AddTransitionsToState(Enum sourceState, Enum[] transitions, bool forceTransition = false)
	{
		if (this.Initialized() == false) { return false; }
		if (this.states.ContainsKey(sourceState) == false) { return false; }
		State s = states[sourceState];
		s.forceTransition = forceTransition;
		foreach (Enum t in transitions)
		{
			if (s.transitions.Contains(t) == true)
			{
				Debug.LogError("State: " + sourceState + " already contains a transition for " + t + " in " + this.GetType().ToString() + ".");
				continue;
			}
			s.transitions.Add(t);
		}
		return true;
	}

	/// <summary>
	/// Check if a transition is legal.
	/// </summary>
	/// <param name="fromstate">From state.</param>
	/// <param name="tostate">To state.</param>
	/// <returns>Whether the transition is legal.</returns>
	protected bool IsLegalTransition(Enum fromstate, Enum tostate)
	{
		if (this.Initialized() == false) { return false; }

		if (this.states.ContainsKey(fromstate) && this.states.ContainsKey(tostate))
		{
			if (this.states[fromstate].forceTransition == true || this.states[fromstate].transitions.Contains(tostate) == true)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Change to a new state.
	/// </summary>
	/// <param name="newstate">New state.</param>
	/// <param name="forceTransition">Force trnasition even if the transition is not valid.</param>
	/// <returns>Return <c>true</c> if the operation is completed correctly.</returns>
	protected bool ChangeCurrentState(Enum newstate, bool forceTransition = false)
	{
		if (this.Initialized() == false) { return false; }

		if (this.inTransition)
		{
			if (this.debugMode == true)
			{
				Debug.LogWarning(this.GetType().ToString() + " requests transition to state " + newstate +
						" when still transitioning.");
			}
			return false;
		}

		if (forceTransition || this.IsLegalTransition(this.currentState.name, newstate))
		{
			if (this.debugMode == true)
			{
				Debug.Log(this.GetType().ToString() + " transition: " + this.currentState.name + " -> " + newstate + ".");
			}

			State transitionSource = this.currentState;
			State transitionTarget = this.states[newstate];
			this.inTransition = true;
			this.currentState.exitMethod(transitionTarget.name);
			transitionTarget.enterMethod(transitionSource.name);
			this.currentState = transitionTarget;

			if (transitionTarget == null || transitionSource == null)
			{
				Debug.LogError(this.GetType().ToString() + " cannot finalize transition; source or target state is null.");
			}
			else
			{
				this.inTransition = false;
			}
		}
		else
		{
			Debug.LogError(this.GetType().ToString() + " requests transition: " + this.currentState.name + " -> " + newstate + " is not a defined transition.");
			return false;
		}

		this.OnUpdate = this.currentState.updateMethod;
		this.OnFixedUpdate = this.currentState.fixedUpdateMethod;
		this.OnLateUpdate = this.currentState.lateUpdateMethod;
		return true;
	}

	private static void DoNothingUpdate() { }
	private static void DoNothingEnterExit(Enum state) { }
}