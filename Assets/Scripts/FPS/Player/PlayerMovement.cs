using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5.0f; // 步行速度
    public float runSpeed = 10.0f; // 跑步速度
    public float crouchSpeed = 2.5f; // 蹲伏速度
    public float jumpForce = 5.0f; // 跳跃力度

    private CharacterController controller;
    private Transform cameraTransform;
    private float verticalVelocity = 0;
    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // 获取输入
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        

        // 根据摄像机局部坐标系计算移动方向
        Vector3 moveDirection = cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput;

        
        // 控制蹲伏
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? 1.0f : 2.0f; // 调整角色高度
        }

        // 控制跑步
        float moveSpeed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
        Debug.Log(controller.isGrounded);
        // 控制跳跃
        if (controller.isGrounded)
        {
            verticalVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            // 应用重力
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // 移动角色
        Vector3 move = moveDirection.normalized * moveSpeed;
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}
