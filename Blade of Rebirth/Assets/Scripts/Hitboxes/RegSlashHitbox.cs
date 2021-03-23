using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegSlashHitbox : MonoBehaviour
{
    [SerializeField] GameObject Collisions;
    // Start is called before the first frame update
    void Start()
    {
        Collisions = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Handles applying damage to enemies
    private void OnTriggerStay(Collider other)
    {
        // Filter out objects that aren't enemies
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyCollision") || other.gameObject.layer == LayerMask.NameToLayer("EnemyNoCollision") || other.gameObject.layer == LayerMask.NameToLayer("EnemyAbsoluteCollision"))
        {
            Collisions = other.gameObject;
        }
    }

    public GameObject getCollisions()
    {
        return Collisions;
    }
}
