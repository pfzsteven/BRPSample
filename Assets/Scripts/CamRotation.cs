using UnityEngine;

public class CamRotation : MonoBehaviour
{
    [SerializeField] private Transform target;

    public float distance = 10.0f; // 相机与目标的距离
    public float xSpeed = 120.0f; // 水平旋转速度
    public float ySpeed = 120.0f; // 垂直旋转速度

    public float yMinLimit = -20f; // 垂直旋转最小角度
    public float yMaxLimit = 80f; // 垂直旋转最大角度

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // 使相机开始时就对准目标物体
        if (target != null)
        {
            transform.position = target.position - transform.forward * distance;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (Input.GetMouseButton(0))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}