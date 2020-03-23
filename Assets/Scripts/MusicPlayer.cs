using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] float loadingTime = 4f;


    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<SceneLoader>().GetActiveSceneIndex() == 0)
        {
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(loadingTime);
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }


    public void ResetMusic()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
