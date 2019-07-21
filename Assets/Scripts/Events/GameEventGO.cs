using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GameEventGO : ScriptableObject
{
    private readonly List<GameEventListenerGO> eventListeners = 
        new List<GameEventListenerGO>();

    public void Raise(GameObject _go)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(_go);
    }

    public void RegisterListener(GameEventListenerGO listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListenerGO listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
