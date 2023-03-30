using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoidHelper {

    const int numViewDirections = 300;
    public static readonly Vector2[] directions;

    static BoidHelper () {
        directions = new Vector2[BoidHelper.numViewDirections];

        float goldenRatio = (1 + Mathf.Sqrt (5)) / 2;
        float angleIncrement = Mathf.PI * 4 * goldenRatio;

        for (int i = 0; i < numViewDirections; i++) {
            float t = (float) i / numViewDirections;
            float inclination = Mathf.Acos (1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin (inclination) * Mathf.Cos (azimuth);
            float y = Mathf.Sin (inclination) * Mathf.Sin (azimuth);
            directions[i] = new Vector2 (x, y);
        }
    }

}