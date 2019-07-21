using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    public UnityEventGO ballChanged;
    public List<string> trackNames;

    List<Ball> balls = new List<Ball>();
    int currentBallId = 0;
    
    void Awake()
    {
        createBalls();

        balls[currentBallId].SetIsCurrent(true);
        ballChanged.Invoke(balls[currentBallId].gameObject);
    }

    public void useNextBall()
    {
        balls[currentBallId].SetIsCurrent(false);

        currentBallId++;
        if (currentBallId >= balls.Count)
            currentBallId = 0;

        balls[currentBallId].SetIsCurrent(true);
        ballChanged.Invoke(balls[currentBallId].gameObject);
    }
    public void usePrevBall()
    {
        balls[currentBallId].SetIsCurrent(false);

        currentBallId--;
        if (currentBallId < 0)
            currentBallId = balls.Count-1;

        balls[currentBallId].SetIsCurrent(true);
        ballChanged.Invoke(balls[currentBallId].gameObject);
    }

    void createBalls()
    {
        GameObject ballPrefab = Resources.Load<GameObject>("prefabs/Ball");

        TracksLoader tracksLoader = new TracksLoader();

        foreach (string trackName in trackNames)
        {
            Ball ball = Instantiate<GameObject>(ballPrefab).GetComponent<Ball>();
            ball.name = "ball_" + trackName;
            ball.SetTrack(tracksLoader.LoadTrack(trackName));
            balls.Add(ball);
        }
    }
}
