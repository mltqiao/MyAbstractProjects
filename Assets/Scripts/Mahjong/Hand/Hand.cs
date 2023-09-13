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
    public BoxCollider mixingCollider;
    public static bool MouseBuildingButtonDown;
    public BoxCollider buildingCollider;
    public Transform transWashCards;
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
        if (mixingCollider.enabled != MouseMixingButtonDown)
        {
            mixingCollider.enabled = MouseMixingButtonDown;
        }
        // 左键码牌
        MouseBuildingButtonDown = Input.GetMouseButton(0);
        if (buildingCollider.enabled != MouseBuildingButtonDown)
        {
            buildingCollider.enabled = MouseBuildingButtonDown;
        }
        FollowMousePosition();
        V3Speed = (_thisFramePos - _lastFramePos) / Time.deltaTime;
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
            Vector3 intersectionPoint = new Vector3(_thisFramePos.x,0f,_thisFramePos.z);
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
            if (!_isCheckingX && _isCheckingZ)
            {
                if (intersectionPoint.x <= -125)
                {
                    intersectionPoint.x = -125f;
                }
                else if (intersectionPoint.x >= 125)
                {
                    intersectionPoint.x = 125f;
                }
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
            }
            transform.position = Vector3.MoveTowards(transform.position,intersectionPoint,float.MaxValue);

            if (V3Speed != Vector3.zero)
            {
                // Vector3 targetDirection = V3Speed.normalized;
                // Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f*Time.deltaTime);
                transform.forward = V3Speed.normalized;
            }
        }
    }
}
