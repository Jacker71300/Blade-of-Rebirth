using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
