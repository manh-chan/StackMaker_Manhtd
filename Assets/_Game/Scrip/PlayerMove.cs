using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Direction;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private LayerMask brickLayerMask;
    [SerializeField] float moveSpeed = 5f;
    
    Vector3 targetPos;
    Vector3 direction;
    bool canMove;  // Biến kiểm soát việc có thể nhận hướng mới

    private void OnEnable()
    {
        Direction.Swipe += Move;
    }

    private void OnDisable()
    {
        Direction.Swipe -= Move;
    }

    private void Start()
    {
        targetPos = transform.position;
        canMove = true;  // Ban đầu cho phép di chuyển
    }

    private void Move(SwipeDirection swipe)
    {
        // Kiểm tra nếu đang di chuyển thì không nhận hướng mới
        if (!canMove) return;

        // Xác định hướng di chuyển mới dựa trên swipe
        switch (swipe)
        {
            case SwipeDirection.Left:
                direction = Vector3.left;
                break;
            case SwipeDirection.Right:
                direction = Vector3.right;
                break;
            case SwipeDirection.Up:
                direction = Vector3.forward;
                break;
            case SwipeDirection.Down:
                direction = Vector3.back;
                break;
        }

        RaycastHit hit;
        for (int i = 1; i <= 50; i++)
        {
            if (Physics.Raycast(transform.position + direction * i + Vector3.up * 3.5f, Vector3.down, out hit, brickLayerMask))
            {
                Debug.DrawRay(transform.position + direction * i + Vector3.down * 3.5f, Vector3.down, Color.red, 1f);
                targetPos = hit.collider.transform.position;
                targetPos.y = transform.position.y;

                // Khi nhận vị trí đích mới, tạm thời không cho phép nhận hướng mới
                canMove = false;
            }
            else
            {
                return;
            }
        }
    }

    private void Update()
    {
        // Di chuyển tới vị trí đích
        if (Vector3.Distance(transform.position, targetPos) > 0.02f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Khi đến vị trí đích, cho phép nhận hướng mới
            canMove = true;
        }
    }
}
