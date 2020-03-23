using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1.5f;
    [SerializeField] GameObject deathVFX;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("HandleDeathSequence");        
    }

    IEnumerator HandleDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathVFX.SetActive(true);
        yield return new WaitForSeconds(2f);
        FindObjectOfType<SceneLoader>().ReloadScene();
    }
}
