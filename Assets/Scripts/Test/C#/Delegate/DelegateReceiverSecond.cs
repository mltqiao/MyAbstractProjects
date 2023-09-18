using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DelegateReceiverSecond: MonoBehaviour
{
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        // 订阅委托事件
        DelegateControlCenter.TestDelegateEvent += SecondHandleEvent;
        Debug.Log("Second event subscribed");
    }

    // 委托事件的处理方法
    private void SecondHandleEvent(string message)
    {
        Debug.Log("Second event : " + message);
        // 在这里执行委托的内容
    }
    
    private void UnsubscribeEvents()
    {
        // 取消订阅委托事件
        DelegateControlCenter.TestDelegateEvent -= SecondHandleEvent;
        Debug.Log("Second event unsubscribed");
    }
}
