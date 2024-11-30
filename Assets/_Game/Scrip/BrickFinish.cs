using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
           
        }
    }
}
