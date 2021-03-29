using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegSlashHitbox : MonoBehaviour
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
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyCollision") || other.gameObject.layer == LayerMask.NameToLayer("EnemyNoCollision") || other.gameObject.layer == LayerMask.NameToLayer("EnemyAbsoluteCollision"))
        {
            Collisions.Add(other.gameObject);
        }
    }

    public List<GameObject> getCollisions()
    {
        return Collisions;
    }

    public void ClearNulls()
    {
        for(int i = 0; i < Collisions.Count; i++)
        {
            if(Collisions[i] == null)
            {
                Collisions.RemoveAt(i);
                i--;
            }
        }
    }
}
