using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSlashHitbox : MonoBehaviour
{
    [SerializeField] List<GameObject> Collisions;
    // Start is called before the first frame update
    void Start()
    {
        Collisions = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handles applying damage to enemies
    private void OnTriggerEnter(Collider other)
    {
        // Filter out objects that aren't enemies
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Dont affect the same enemy twice!
            if (!Collisions.Contains(other.gameObject))
            {
                Collisions.Add(other.gameObject);
                //print(other.gameObject.name);
            }
        }
    }

    public int getNumCollisions()
    {
        return Collisions.Count;
    }
}
