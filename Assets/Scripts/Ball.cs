using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum eBallState
{
    Idle,
    Move,
}

public class Ball : MonoBehaviour, IPointerClickHandler
{
    public FloatVariable speed;
    public float maxSpeed;
    public TrailRenderer trail;

    class BallFSM : FiniteStateMachine<eBallState> { }
    BallFSM ballFSM;

    Track track;

    int currentPoint = 0;
    float nextPointProgress = 0;

    bool isCurrent;

    public void SetIsCurrent(bool t)
    {
        isCurrent = t;
    }

    public void SetTrack(Track _track)
    {
        track = _track;
        BeginIdle();
    }

    void Start()
    {
        ballFSM = new BallFSM();

        ballFSM.AddTransition(eBallState.Idle, eBallState.Move, BeginMove);
        ballFSM.AddTransition(eBallState.Move, eBallState.Idle, BeginIdle);

        ballFSM.Initialise(eBallState.Idle);
    }

    void Update()
    {
        if(ballFSM.GetState() == eBallState.Move && isCurrent)
        {
            if(currentPoint < track.Length -1)
            {
                nextPointProgress += Time.deltaTime * speed.Value * maxSpeed/(track.getPoint(currentPoint+1)-track.getPoint(currentPoint)).magnitude;

                if(nextPointProgress >= 1)
                {
                    currentPoint++;
                    nextPointProgress -= 1;

                    if(currentPoint >= track.Length-1)
                    {
                        ballFSM.Advance(eBallState.Idle);
                        return;
                    }
                }

                transform.position = Vector3.Lerp(track.getPoint(currentPoint),
                    track.getPoint(currentPoint+1),
                    nextPointProgress);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCurrent)
            return;

        if (eventData.clickCount == 2)
            ballFSM.Advance(eBallState.Idle);
        else
            ballFSM.Advance(eBallState.Move);
    }

    void BeginMove()
    {
        trail.emitting = true;
    }

    void BeginIdle()
    {
        transform.position = track.getPoint(0);
        trail.emitting = false;
        trail.Clear();
        currentPoint = 0;
        nextPointProgress = 0;
    }
}