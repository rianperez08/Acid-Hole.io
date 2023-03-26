using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SuckableObject")) 
        { 
            print("haha");
            other.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
