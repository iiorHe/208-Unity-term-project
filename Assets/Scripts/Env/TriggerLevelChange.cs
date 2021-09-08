using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLevelChange : MonoBehaviour
{
    public int levelIndex;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
