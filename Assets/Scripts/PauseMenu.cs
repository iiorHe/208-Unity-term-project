using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private static bool _paused = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(_paused){
                Resume();
            } else {
                Pause();
            }
        }
    }
    void Resume(){
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        _paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Pause(){
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        _paused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void ReturnToMenu(){
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void QuitGame(){
        Application.Quit();
    }
}
