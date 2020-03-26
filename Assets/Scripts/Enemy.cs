using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathVFX;
    [SerializeField] Transform instanceParent;
    [SerializeField] int killPoints = 100;

    private void Start()
    {
        if (GetComponent<BoxCollider>() == null) // Check if gameObject already has BoxCollider on it
        {
            AddNonTriggerBoxCollider();
        }
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider enemyBoxCollider = gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        //  Add to score
        FindObjectOfType<ScoreBoard>().ScoreHit(killPoints);
        
        //  Instantiate death FX under "Runtime instances" parent
        GameObject fx = Instantiate(enemyDeathVFX, transform.position, Quaternion.identity);
        fx.transform.parent = instanceParent;

        //  Self-destroy
        Destroy(gameObject);
    }
}
