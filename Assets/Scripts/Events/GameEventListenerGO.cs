using UnityEngine;
using UnityEngine.Events;


public class GameEventListenerGO : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEventGO Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEventGO Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject _go)
    {
        Response.Invoke(_go);
    }

}
