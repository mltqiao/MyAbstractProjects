using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 执行能触发委托的函数
        if (Input.GetMouseButtonDown(0))
        {
            DelegateControlCenter.TriggerEventLeft("abc");
        }
    }
}
