using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerBrick player = other.GetComponent<PlayerBrick>();
                player.AddStack(gameObject);
                // Ẩn hoặc di chuyển Brick nếu cần thiết
                // gameObject.SetActive(false);
        }
    }
}
