using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSlashHitbox : MonoBehaviour
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
    private void OnTriggerStay(Collider other)
    {
        // Filter out objects that aren't enemies
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyCollision") || other.gameObject.layer == LayerMask.NameToLayer("EnemyNoCollision") || other.gameObject.layer == LayerMask.NameToLayer("EnemyAbsoluteCollision") && !Collisions.Contains(other.gameObject))
        {
            Collisions.Add(other.gameObject);
            //print(other.gameObject.name);
        }
    }

    public List<GameObject> getCollisions()
    {
        return Collisions;
    }
}
