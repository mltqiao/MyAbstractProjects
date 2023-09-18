using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionTrigger : MonoBehaviour
{
    public int a;
    public int b;

    // Update is called once per frame
    void Update()
    {
        // 执行能触发委托的函数
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FusionControlCenter.TriggerEvent(true, a, b);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FusionControlCenter.TriggerEvent(false, a, b);
        }
    }
}
