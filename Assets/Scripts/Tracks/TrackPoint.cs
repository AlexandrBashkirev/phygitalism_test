using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPointsSource
{
    public float[] x;
    public float[] y;
    public float[] z;

    public int Length
    {
        get
        {
            return x.Length;
        }
    }

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < Length; i++)
        {
            yield return new Vector3(x[i], y[i], z[i]);
        }
    }
}
