using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMousePosition : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake() { mainCamera = Camera.main; }

    private void Update()
    {
        SetPositionToMouse();
    }

    private void SetPositionToMouse()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);
    }

    private Vector3 GetMouseWorldPosition()
    {
        // 스크린 상의 마우스 좌표를 기준으로 월드 상의 좌표를 구함
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(position);
        return mouseWorldPosition;
    }
}
