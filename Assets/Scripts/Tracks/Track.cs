using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track
{
    Vector3[] points;

    public Track(TrackPointsSource _trackPointsSource)
    {
        points = new Vector3[_trackPointsSource.Length];

        int j = 0;
        foreach (Vector3 v in _trackPointsSource)
            points[j++] = v;
    }

    public int Length
    {
        get
        {
            return points.Length;
        }
    }

    public Vector3 getPoint(int i)
    {
        return points[i];
    }
}
