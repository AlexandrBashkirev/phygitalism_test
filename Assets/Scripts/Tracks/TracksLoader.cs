using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksLoader 
{
    public Track LoadTrack(string name)
    {
        string jsonData = Resources.Load<TextAsset>("track_datas/" + name).text;
        TrackPointsSource trackPoints = JsonUtility.FromJson<TrackPointsSource>(jsonData);

        return new Track(trackPoints);
    }
}
