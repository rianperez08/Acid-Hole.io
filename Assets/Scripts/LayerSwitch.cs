using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitch : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private string Enter;
    [SerializeField] private string Exit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SuckableObject")
            other.gameObject.layer = LayerMask.NameToLayer("FallingLayer");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SuckableObject")
            other.gameObject.layer = LayerMask.NameToLayer(Exit);
    }
}
