using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        MoveTo(new Vector3(x, y, 0));
    }

    private void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }

}