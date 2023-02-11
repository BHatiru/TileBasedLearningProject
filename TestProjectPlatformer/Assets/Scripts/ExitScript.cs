using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitScript : MonoBehaviour
{
    public GameObject victoryPane;
    PlayerMovement pl;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        pl = other.GetComponent<PlayerMovement>();
        if (other.tag == "Player" && pl.GetLiveState())
            StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(currentSceneIndex == SceneManager.sceneCountInBuildSettings-1){
            victoryPane.SetActive(true);
            Time.timeScale = 0f;
        }else{
            SceneManager.LoadScene(nextSceneIndex);
        }
        
    }
}
