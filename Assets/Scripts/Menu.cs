using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public GameObject pauseManu;
   public AudioMixer AudioMixer;
   public void PlatGame()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void QuitGame()
   {
      Application.Quit();
   }

   public void PauseGame()
   {
      pauseManu.SetActive(true);
      Time.timeScale = 0f;
   }
   public void ResumeGame()
   {
      pauseManu.SetActive(false);
      Time.timeScale = 1f;
   }

   public void ExitGame()
   {
      SceneManager.LoadScene(0);
   }

   public void SetVolume(float value)
   {
      AudioMixer.SetFloat("MainVolume" , value);
   }

}
