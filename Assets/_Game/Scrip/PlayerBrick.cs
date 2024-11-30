using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrick : MonoBehaviour
{
    // Stack lưu trữ các Brick
    public List<GameObject> playerStack = new List<GameObject>();
    public Transform footPosition;
     public float stackHeight = 0.5f;
    // Hàm thêm Brick vào stack
    public Transform playerHeightTransform;
    public void AddStack(GameObject brick)
    {
        Vector3 newBrickPosition = footPosition.position - new Vector3(0, -stackHeight * playerStack.Count, 0);
        brick.transform.position = newBrickPosition;
        brick.transform.parent = footPosition;
        playerStack.Add(brick);
        playerHeightTransform.position += new Vector3(0,stackHeight, 0); // Tăng chiều cao của đối tượng con
    }

    // Hàm loại bỏ Brick khỏi stack
    public void RemoveStack()
    {
        if (playerStack.Count > 0)
        {
            GameObject removedBrick = playerStack[playerStack.Count - 1];
            playerStack.RemoveAt(playerStack.Count - 1);
            playerHeightTransform.position -= new Vector3(0,stackHeight, 0);
            Destroy(removedBrick); // Hủy Brick đã loại bỏ (có thể thay đổi tùy yêu cầu)
        }
        else
        {
            GameManager.instance.GameOver();
            Debug.Log("Stack is empty.");
        }
    }

    // Hàm xóa toàn bộ Brick trong stack
    public void ClearStack()
    {
        foreach (GameObject brick in playerStack)
        {
            Destroy(brick);
        }
        playerStack.Clear();
        Debug.Log("Stack cleared.");
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Finish")) {
            GameManager.instance.FinishGame();
            ClearStack();
            }
    }
}
