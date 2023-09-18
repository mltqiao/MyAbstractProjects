using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DelegateControlCenter
{
    // 定义委托
    public delegate void DelegateEvent(string message);

    // 声明委托事件
    public static event DelegateEvent TestDelegateEvent;

    // 用来触发委托事件的函数
    public static void TriggerEventLeft(string message)
    {
        if (TestDelegateEvent != null)
        {
            TestDelegateEvent(message);
        }
    }
}
