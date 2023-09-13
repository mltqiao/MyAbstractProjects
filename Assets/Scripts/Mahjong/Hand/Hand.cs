using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool _isCheckingX;
    private bool _isCheckingZ;
    private Vector3 _lastFramePos;
    private Vector3 _thisFramePos;
    public static Vector3 V3Speed;
    public static bool MouseButtonDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseButtonDown = Input.GetMouseButton(0);
        _lastFramePos = _thisFramePos;
        FollowMousePosition();
        _thisFramePos = transform.position;
        V3Speed = (_thisFramePos - _lastFramePos) / Time.deltaTime;
        Debug.Log(V3Speed);
    }

    private void FollowMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 cameraToHitPoint = hit.point - Camera.main.transform.position;
            float t = -Camera.main.transform.position.y / cameraToHitPoint.y;
            Vector3 intersectionPoint = Camera.main.transform.position + t * cameraToHitPoint;
            intersectionPoint.y = 0f;
            // 判断检测状态
            if (intersectionPoint.x < -125f || intersectionPoint.x > 125f)
            {
                _isCheckingX = false;
            }
            else
            {
                _isCheckingX = true;
            }
            if (intersectionPoint.z < -125f || intersectionPoint.z > 125f)
            {
                _isCheckingZ = false;
            }
            else
            {
                _isCheckingZ = true;
            }
            // 根据检测状态给transform赋值
            if (_isCheckingX && _isCheckingZ)
            {
                transform.position = Vector3.MoveTowards(transform.position,intersectionPoint,float.MaxValue);
            }
            else if (!_isCheckingX && _isCheckingZ)
            {
                if (intersectionPoint.x <= -125)
                {
                    intersectionPoint.x = -125f;
                }
                else if (intersectionPoint.x >= 125)
                {
                    intersectionPoint.x = 125f;
                }
                transform.position = Vector3.MoveTowards(transform.position,intersectionPoint,float.MaxValue);
            }
            else if (_isCheckingX && !_isCheckingZ)
            {
                if (intersectionPoint.z <= -125)
                {
                    intersectionPoint.z = -125f;
                }
                else if (intersectionPoint.z >= 125)
                {
                    intersectionPoint.z = 125f;
                }
                transform.position = Vector3.MoveTowards(transform.position,intersectionPoint,float.MaxValue);
            }
            else if (!_isCheckingX && !_isCheckingZ)
            {
                if (intersectionPoint.x <= -125)
                {
                    intersectionPoint.x = -125f;
                }
                else if (intersectionPoint.x >= 125)
                {
                    intersectionPoint.x = 125f;
                }
                if (intersectionPoint.z <= -125)
                {
                    intersectionPoint.z = -125f;
                }
                else if (intersectionPoint.z >= 125)
                {
                    intersectionPoint.z = 125f;
                }
                transform.position = Vector3.MoveTowards(transform.position,intersectionPoint,float.MaxValue);
            }
        }
    }
}
