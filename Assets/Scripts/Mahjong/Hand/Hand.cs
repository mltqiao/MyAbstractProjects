using System;
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

    public bool triggerOrCollider;
    private Rigidbody _rig;
    private float _maxRotationSpeed;
    public static Vector3 V3Speed;
    public static bool MouseMixingButtonDown;
    public BoxCollider mixingTrigger;
    public BoxCollider mixingCollider;
    public static bool MouseBuildingButtonDown;
    public BoxCollider buildingCollider;
    public static Vector3 IntersectionPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _maxRotationSpeed = MahjongParameters.HandMixMaxAngle;
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

    private void FixedUpdate()
    {
        FollowMousePositionPhysics();
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

            if (!triggerOrCollider)
            {
                if (!mixingTrigger.enabled)
                {
                    mixingTrigger.enabled = true;
                }
                if (mixingCollider.enabled)
                {
                    mixingCollider.enabled = false;
                }
                
                transform.position = Vector3.MoveTowards(transform.position,IntersectionPosition,float.MaxValue);
                
                if (V3Speed != Vector3.zero)
                {
                    float step = _maxRotationSpeed * Time.fixedDeltaTime;
                    Quaternion targetRotation = Quaternion.LookRotation(V3Speed.normalized);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation.normalized,step);
                }
            }
        }
    }

    private void FollowMousePositionPhysics()
    {
        if (MouseMixingButtonDown)
        {
            if (triggerOrCollider)
            {
                if (mixingTrigger.enabled)
                {
                    mixingTrigger.enabled = false;
                }
                if (!mixingCollider.enabled)
                {
                    mixingCollider.enabled = true;
                }
                Vector3 direction = IntersectionPosition - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
                float step = _maxRotationSpeed * Time.fixedDeltaTime;
                _rig.MoveRotation(Quaternion.RotateTowards(_rig.rotation, targetRotation, step));
                _rig.MovePosition(IntersectionPosition);
            }
        }
    }
}
