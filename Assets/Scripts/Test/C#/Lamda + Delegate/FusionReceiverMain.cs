using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionReceiverMain : MonoBehaviour
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
        FusionControlCenter.TestFusionEvent += MainHandleEvent;
        Debug.Log("Main event subscribed");
    }

    // 委托事件的处理方法
    private void MainHandleEvent(bool minusOrPlus, int a, int b)
    {
        // 在这里执行委托的内容
        if (minusOrPlus)
        {
            int result = a + b;
            Debug.Log("Main event : " + result);
        }
        else
        {
            int result = a - b;
            Debug.Log("Main event : " + result);
        }
    }
    
    private void UnsubscribeEvents()
    {
        // 取消订阅委托事件
        FusionControlCenter.TestFusionEvent -= MainHandleEvent;
        Debug.Log("Main event unsubscribed");
    }
}
