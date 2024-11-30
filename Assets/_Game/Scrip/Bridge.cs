using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] GameObject TopBridge;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerBrick player = other.GetComponent<PlayerBrick>();
            
            if (!TopBridge.activeSelf)  // Kiểm tra xem TopBridge có đang tắt không
            {
                // Kích hoạt TopBridge và trừ stack
                TopBridge.SetActive(true);
                player.RemoveStack();
            }
            else
            {
                // Nếu TopBridge đã active rồi thì không trừ stack nữa
                Debug.Log("TopBridge đã active, không trừ stack.");
            }
        }
    }
}
