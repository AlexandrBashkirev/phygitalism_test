/**
 * ----------------------------------------------------------------------------
 *  File:             FiniteStateMachine.cs
 *  
 * System:            
 * Component Name:    
 * Status:            Version 0.1
 * 
 * Language:          C#
 * 
 * License:           
 * 
 * Author:            Tasos Giannakopoulos (tasosg@voidinspace.com)
 * Copyright:         
 * Date:              05 May 2013
 * 
 * Description:       A generic finite state machine
 * 
 *----------------------------------------------------------------------------
 **/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void Callback();


/// <summary>
/// StateTransition class
/// </summary>
/// <typeparam name="S">State enum</typeparam>
public class StateTransition<S> : System.IEquatable<StateTransition<S>>
{
  // Public variables
  // ----------------------------------------
  
  // Protected variables
  // ----------------------------------------
  protected S   mInitState;
  protected S   mEndState;
  
  // Public functions
  // ----------------------------------------
  public      StateTransition() {}
  public      StateTransition(S init, S end) { mInitState = init; mEndState = end; }

  public bool Equals(StateTransition<S> other)
  {
    if (ReferenceEquals(null, other)) 
      return false;
    if (ReferenceEquals(this, other)) 
      return true;

    return mInitState.Equals(other.GetInitState()) && mEndState.Equals(other.GetEndState());
  }

  public override int GetHashCode()
  {
    if((mInitState == null || mEndState == null))
      return 0;

    unchecked
    {
      int hash = 17;
      hash = hash * 23 + mInitState.GetHashCode();
      hash = hash * 23 + mEndState.GetHashCode();
      return hash;
    }
  }
  
  public S    GetInitState() { return mInitState; }
  public S    GetEndState() { return mEndState; }
}


/// <summary>
/// A generic Finite state machine
/// </summary>
/// <typeparam name="S"></typeparam>
public class FiniteStateMachine<S>
{
  // Public variables
  // ----------------------------------------

  // Protected variables
  // ----------------------------------------
  protected S                 mState;
  protected S                 mPrevState;
  protected bool              mbLocked = false;

  protected Dictionary<StateTransition<S>, System.Delegate>  
    mTransitions;


  // Public functions
  // ----------------------------------------
  public      FiniteStateMachine() 
  {
    mTransitions  = new Dictionary<StateTransition<S>, System.Delegate>();
  }

  public void Initialise(S state) { mState = state; }

  public void Advance(S nextState)
  {
    if(mbLocked)
      return;

    StateTransition<S> transition = new StateTransition<S>(mState, nextState);

    // Check if the transition is valid
    System.Delegate d;
    if (mTransitions.TryGetValue(transition, out d)) // new StateTransition(mState, nextState)
    {
      	mPrevState            = mState;
      	mState                = nextState;

		if (d != null)
		{
			Callback c = d as Callback;
			c();
		}
    }
    else
    {
      //Debug.Log("[FMS] Cannot advance to " + nextState + " state");
    }
  }

  public void AddTransition(S init, S end, Callback c)
  {
    StateTransition<S> tr = new StateTransition<S>(init, end);

    if (mTransitions.ContainsKey(tr))
    {
      return;
    }

    mTransitions.Add(tr, c);

  }

  // Call this to prevent the state machine from leaving this state
  public void Lock() { mbLocked = true; }

  public void Unlock() 
  { 
    mbLocked = false; 
    Advance(mPrevState); 
  }

  public S GetState() { return mState; }
  public S GetPrevState() { return mPrevState; }
}