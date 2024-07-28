using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static int GetRandomIndexWithCustomProb(float[] prob)
    {
        int rInt = UnityEngine.Random.Range(0, 100);
        float rFloat = (float)rInt / 100f;
        for (int i = 0; i < prob.Length; i++)
        {
            if (rFloat > prob[i]) rFloat -= prob[i];
            else return i;
        }
        return 0;
    }
}
