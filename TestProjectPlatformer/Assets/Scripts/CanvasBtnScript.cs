using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CanvasBtnScript : MonoBehaviour
{
    public TextMeshProUGUI lvlText;
    void Start(){
        lvlText.SetText("Level " + SceneManager.GetActiveScene().buildIndex);
    }
    public void ReloadLevel(){
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadGame(){
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void ExitGame(){
        Application.Quit();
    }

}
