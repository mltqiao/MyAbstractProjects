using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
using UnityEngine;

public class LamdaTest : MonoBehaviour
{
    public int a;
    public int b;
    public bool minusOrPlus;

    private delegate int IntPlus(int x, int y);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Result();
        }
    }

    private void Result()
    {
        if (minusOrPlus)
        {
            IntPlus plus = (a, b) => a + b;
            int result = plus(a, b);
            Debug.Log(result);
        }
        else
        {
            IntPlus minus = (a, b) => a - b;
            int result = minus(a, b);
            Debug.Log(result);
        }
    }
}
