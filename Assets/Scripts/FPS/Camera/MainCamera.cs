using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float sensitivityX = 2.0f; // 鼠标X轴灵敏度
    public float sensitivityY = 2.0f; // 鼠标Y轴灵敏度
    public float minYRotation = -90.0f; // 最小Y轴旋转角度
    public float maxYRotation = 90.0f; // 最大Y轴旋转角度

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    public Transform headTransform;

    void Start()
    {
        if (headTransform == null)
        {
            Debug.LogError("Head Transform not found! Make sure to set the correct name.");
        }
    }

    void Update()
    {
        if (headTransform == null)
            return;

        // 获取鼠标移动的距离
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 计算水平旋转（头部物体绕Y轴）
        rotationY += mouseX * sensitivityX;

        // 计算垂直旋转（头部物体绕X轴）
        rotationX -= mouseY * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minYRotation, maxYRotation);

        // 应用旋转到头部物体
        headTransform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}