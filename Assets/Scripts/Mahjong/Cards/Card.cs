using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Card : MonoBehaviour
{
    public BoxCollider cardCollider;
    public Rigidbody rig;
    private CardsContainer _cardsContainer;
    public int buildState;  // 0、没码  1、开始码  2、正在码  3、码好了
    public Vector3 v3BuildPos;
    public Vector3 v3BuildRot;
    
    private float _buildRotationSpeed;
    private float _buildMoveSpeed;
    private float _maxMixCentripetalDistance;
    private float _minMixFollowingDistance;
    // Start is called before the first frame update
    void Start()
    {
        _cardsContainer = GetComponentInParent<CardsContainer>();
        _buildRotationSpeed = MahjongParameters.CardsBuildRotationSpeed;
        _buildMoveSpeed = MahjongParameters.CardsBuildMoveSpeed;
        _maxMixCentripetalDistance = MahjongParameters.CardsMixCentripetalMaxDistance;
        _minMixFollowingDistance = MahjongParameters.CardsMixFollowingMinDistance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (buildState)
        {
            // 洗牌、没码时
            case 0 :
                rig.AddForce(Vector3.down * 800f);
                if (Vector3.Distance(transform.position, transform.parent.position) >= 200f)
                {
                    rig.velocity = Vector3.zero;
                    transform.localPosition = Vector3.zero;
                }
                break;
            case 1 :
                buildState = 2;
                cardCollider.enabled = false;
                rig.isKinematic = true;
                StartCoroutine(MoveToBuildTransform());
                break;
            case 2 :
                // in IEnumerator
                break;
            case 3 :
                
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 洗牌
        if (other.gameObject.layer == LayerMask.NameToLayer("Hand") && Hand.MouseMixingButtonDown)
        {
            Vector3 forceDirection = Hand.V3Speed.normalized;
            float distance = Vector3.Distance(other.transform.position, transform.position);
            // 感觉这里应该加一个限制最大拖动力的大小，但是不知道该怎么限制，有待研究^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            rig.AddForce(forceDirection * MahjongParameters.CardsMixDragForce/distance, ForceMode.Impulse);
            // 限制向心力大小
            float distanceCenter = Vector3.Distance(-transform.position, Vector3.zero);
            if (distanceCenter >= _maxMixCentripetalDistance)
            {
                distanceCenter = _maxMixCentripetalDistance;
            }
            rig.AddForce(-transform.position.normalized * distanceCenter * MahjongParameters.CardsMixCentripetalForce, ForceMode.Impulse);
            // 限制跟随力最小距离
            if (distance >= _minMixFollowingDistance)
            {
                rig.AddForce(_minMixFollowingDistance * Hand.IntersectionPosition * MahjongParameters.CardsMixFollowingForce,ForceMode.Impulse);
            }
            // 暂时禁用重力
            if (rig.useGravity)
            {
                rig.useGravity = false;
            }
            // 限制y轴速度 让牌不容易飞起来
            if (rig.velocity.y >= 100f)
            {
                rig.velocity = new Vector3(rig.velocity.x, -10f, rig.velocity.z);
            }
        }
        // 码牌
        else if (other.gameObject.layer == LayerMask.NameToLayer("Hand") && Hand.MouseBuildingButtonDown)
        {
            // 抵消下压力和自身重力
            rig.AddForce(Vector3.up * 800f);
            rig.useGravity = false;
            // 整合拖拽方向
            Vector3 forceDirection = Hand.V3Speed.normalized;
            if (Hand.V3Speed.z < 0 && Mathf.Abs(Hand.V3Speed.x) < -Hand.V3Speed.z)
            {
                forceDirection = Vector3.back;
            }
            else if (Hand.V3Speed.x < 0 && Mathf.Abs(Hand.V3Speed.z) < -Hand.V3Speed.x)
            {
                forceDirection = Vector3.left;
            }
            else if (Hand.V3Speed.z > 0 && Mathf.Abs(Hand.V3Speed.x) < Hand.V3Speed.z)
            {
                forceDirection = Vector3.forward;
            }
            else if (Hand.V3Speed.x > 0 && Mathf.Abs(Hand.V3Speed.z) < Hand.V3Speed.x)
            {
                forceDirection = Vector3.right;
            }
            rig.AddForce(forceDirection * MahjongParameters.CardsBuildDragForce, ForceMode.Impulse);
        }
        // 松开时恢复重力
        else
        {
            rig.useGravity = true;
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls") && Hand.MouseBuildingButtonDown)
        {
            if (buildState == 0)
            {
                Vector3 buildDirection = Hand.V3Speed.normalized;
                // Bottom build
                if (buildDirection.z < 0 && Math.Abs(buildDirection.x) < -buildDirection.z)
                {
                    _cardsContainer.transWalls[0].BuildWall(this);
                }
                // Left build
                else if (buildDirection.x < 0 && Math.Abs(buildDirection.z) < -buildDirection.x)
                {
                    _cardsContainer.transWalls[1].BuildWall(this);
                }
                // Top build
                else if (buildDirection.z > 0 && Math.Abs(buildDirection.x) < buildDirection.z)
                {
                    _cardsContainer.transWalls[2].BuildWall(this);
                }
                // Right build
                else if (buildDirection.x > 0 && Math.Abs(buildDirection.z) < buildDirection.x)
                {
                    _cardsContainer.transWalls[3].BuildWall(this);
                }
            }
        }
    }
    
    private IEnumerator MoveToBuildTransform()
    {
        if (buildState == 2)
        {
            Vector3 startRot = transform.eulerAngles; // 获取起始的欧拉角度
            float elapsedTime = 0f;

            while (elapsedTime < 1.0f)
            {
                // 使用Lerp进行插值旋转
                transform.eulerAngles = Vector3.Lerp(startRot, v3BuildRot, elapsedTime);

                elapsedTime += Time.deltaTime * _buildRotationSpeed;
                yield return null;
            }

            // 确保旋转到目标角度
            transform.eulerAngles = v3BuildRot;

            // 移动物体至目标位置
            while (Vector3.Distance(transform.position, v3BuildPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, v3BuildPos, _buildMoveSpeed * Time.deltaTime);
                yield return null;
            }

            // 确保物体准确停在目标位置
            transform.position = v3BuildPos;
            buildState = 3;
        }
    }
}
