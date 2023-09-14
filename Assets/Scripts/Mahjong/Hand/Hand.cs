using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool _isCheckingX;
    private bool _isCheckingZ;
    private Vector3 _lastFramePos;
    private Vector3 _thisFramePos;
    public static Vector3 V3Speed;
    public static bool MouseMixingButtonDown;
    public BoxCollider mixingTrigger;
    public static bool MouseBuildingButtonDown;
    public BoxCollider buildingCollider;
    public static Vector3 IntersectionPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lastFramePos = _thisFramePos;
        // 右键洗牌
        MouseMixingButtonDown = Input.GetMouseButton(1);
        // 左键码牌
        MouseBuildingButtonDown = Input.GetMouseButton(0);
        if (buildingCollider.enabled != MouseBuildingButtonDown)
        {
            buildingCollider.enabled = MouseBuildingButtonDown;
        }
        FollowMousePosition();
        V3Speed = (_thisFramePos - _lastFramePos) / Time.deltaTime;
        // 限制一帧内的鼠标运动速度(没啥大用，后面取值都是取的Normalized值)
        if (V3Speed.x >= 150f)
        {
            V3Speed = new Vector3(150f, 0, V3Speed.z);
        }
        else if (V3Speed.x <= -150f)
        {
            V3Speed = new Vector3(-150f, 0, V3Speed.z);
        }
        if (V3Speed.z >= 150f)
        {
            V3Speed = new Vector3(V3Speed.x, 0, 150f);
        }
        else if (V3Speed.z <= -150f)
        {
            V3Speed = new Vector3(V3Speed.x, 0, -150f);
        }
    }

    private void FollowMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 cameraToHitPoint = hit.point - Camera.main.transform.position;
            float t = -Camera.main.transform.position.y / cameraToHitPoint.y;
            _thisFramePos = Camera.main.transform.position + t * cameraToHitPoint;
            IntersectionPosition = new Vector3(_thisFramePos.x,0f,_thisFramePos.z);
            // 判断检测状态
            if (IntersectionPosition.x < -125f || IntersectionPosition.x > 125f)
            {
                _isCheckingX = false;
            }
            else
            {
                _isCheckingX = true;
            }
            if (IntersectionPosition.z < -125f || IntersectionPosition.z > 125f)
            {
                _isCheckingZ = false;
            }
            else
            {
                _isCheckingZ = true;
            }
            // 根据检测状态给transform赋值
            if (!_isCheckingX && _isCheckingZ)
            {
                if (IntersectionPosition.x <= -125)
                {
                    IntersectionPosition.x = -125f;
                }
                else if (IntersectionPosition.x >= 125)
                {
                    IntersectionPosition.x = 125f;
                }
            }
            else if (_isCheckingX && !_isCheckingZ)
            {
                if (IntersectionPosition.z <= -125)
                {
                    IntersectionPosition.z = -125f;
                }
                else if (IntersectionPosition.z >= 125)
                {
                    IntersectionPosition.z = 125f;
                }
            }
            else if (!_isCheckingX && !_isCheckingZ)
            {
                if (IntersectionPosition.x <= -125)
                {
                    IntersectionPosition.x = -125f;
                }
                else if (IntersectionPosition.x >= 125)
                {
                    IntersectionPosition.x = 125f;
                }
                if (IntersectionPosition.z <= -125)
                {
                    IntersectionPosition.z = -125f;
                }
                else if (IntersectionPosition.z >= 125)
                {
                    IntersectionPosition.z = 125f;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position,IntersectionPosition,float.MaxValue);

            if (V3Speed != Vector3.zero)
            {
                transform.forward = V3Speed.normalized;
            }
        }
    }
}
