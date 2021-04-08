using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 spinDistance;
    [SerializeField] float spinTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate
        gameObject.transform.Rotate(spinDistance.x * Time.deltaTime / spinTime, spinDistance.y * Time.deltaTime / spinTime, spinDistance.z * Time.deltaTime / spinTime, Space.Self);
    }
}
