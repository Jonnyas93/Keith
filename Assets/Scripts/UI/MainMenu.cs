using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class MainMenu : MonoBehaviour
    {

        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }
        public void PlayGame()
        {
            StartCoroutine(sceneTransition());
        }
        IEnumerator sceneTransition() 
        {
            var crossfade = FindObjectOfType<Animator>();
            crossfade.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("SpawnPoint", 0);
            Time.timeScale = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }

        public void QuitGame ()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    }
}