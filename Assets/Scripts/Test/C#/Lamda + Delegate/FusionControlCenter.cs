using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FusionControlCenter
{
    // 定义委托
    public delegate void FusionEvent(bool minusOrPlus, int x, int y);

    // 声明委托事件
    public static event FusionEvent TestFusionEvent;

    // 用来触发委托事件的函数
    public static void TriggerEvent(bool minusOrPlus, int a, int b)
    {
        if (TestFusionEvent != null)
        {
            TestFusionEvent(minusOrPlus ,a, b);
        }
    }
}
