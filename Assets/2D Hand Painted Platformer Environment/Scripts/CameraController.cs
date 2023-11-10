using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject boundary; // 在检查器中拖放边界对象到这个字段
    public Transform player; // 玩家的 Transform 组件
    private Camera mainCamera;
    private BoxCollider2D boundaryCollider;

    void Start()
    {
        player = transform.parent; // 获取玩家 Transform 组件
        mainCamera = GetComponent<Camera>();
        boundaryCollider = boundary.GetComponent<BoxCollider2D>();

        // 计算边界的尺寸
        Vector3 boundarySize = boundaryCollider.bounds.size;

        // 计算相机应该拥有的视野大小
        float cameraSize = Mathf.Min(boundarySize.x / (2 * mainCamera.aspect), boundarySize.y / 2);

        // 设置相机的视野大小
        mainCamera.orthographicSize = cameraSize;

        // 获取边界的中心位置
        Vector3 boundaryCenter = boundaryCollider.bounds.center;

        // 限制相机的位置，使其不会超出边界
        float cameraX = Mathf.Clamp(transform.position.x, boundaryCenter.x - boundarySize.x / 2 + cameraSize * mainCamera.aspect, boundaryCenter.x + boundarySize.x / 2 - cameraSize * mainCamera.aspect);
        float cameraY = Mathf.Clamp(transform.position.y, boundaryCenter.y - boundarySize.y / 2 + cameraSize, boundaryCenter.y + boundarySize.y / 2 - cameraSize);

        transform.position = new Vector3(cameraX, cameraY, transform.position.z);
    }

    void Update()
    {
        // 获取边界的中心位置
        Vector3 boundaryCenter = boundaryCollider.bounds.center;

        // 限制相机的位置，使其不会超出边界
        float cameraX = Mathf.Clamp(transform.position.x, boundaryCenter.x - boundaryCollider.bounds.size.x / 2 + mainCamera.orthographicSize * mainCamera.aspect, boundaryCenter.x + boundaryCollider.bounds.size.x / 2 - mainCamera.orthographicSize * mainCamera.aspect);
        float cameraY = Mathf.Clamp(transform.position.y, boundaryCenter.y - boundaryCollider.bounds.size.y / 2 + mainCamera.orthographicSize, boundaryCenter.y + boundaryCollider.bounds.size.y / 2 - mainCamera.orthographicSize);

        transform.position = new Vector3(cameraX, cameraY, transform.position.z);
    }
}
