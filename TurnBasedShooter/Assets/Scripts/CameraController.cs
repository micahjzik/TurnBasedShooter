using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 20f;
    [SerializeField] private float mouseDragSpeed = 1f;
    [SerializeField] private float zoomSmoothTime = 0.15f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float targetFollowYOffset;
    private CinemachineTransposer cinemachineTransposer;
    private float zoomVelocity;

    private void Awake()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowYOffset = cinemachineTransposer.m_FollowOffset.y;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseDrag();
        HandleMouseScroll();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Rotate(0, Input.GetAxisRaw("Mouse X") * 10f, 0);
        }
    }

    private void HandleMouseDrag()
    {
        if (!Input.GetMouseButton(2))
        {
            return;
        }

        Vector3 mouseDelta = new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y"));
        Vector3 horizontalRight = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
        Vector3 horizontalForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Vector3 moveVector = horizontalRight * -mouseDelta.x + horizontalForward * -mouseDelta.z;
        transform.position += moveVector * mouseDragSpeed;
    }

    private void HandleMouseScroll()
    {

        float zoomAmount = 1f;

        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowYOffset -= zoomAmount;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowYOffset += zoomAmount;
        }

        targetFollowYOffset = Mathf.Clamp(targetFollowYOffset, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        Vector3 followOffset = cinemachineTransposer.m_FollowOffset;
        followOffset.y = Mathf.SmoothDamp(
            followOffset.y,
            targetFollowYOffset,
            ref zoomVelocity,
            zoomSmoothTime);
        cinemachineTransposer.m_FollowOffset = followOffset;
    }
}
