using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetDificulty(float f)
    {
        GameObject.FindObjectOfType<DifficultyHolder>().difficulty = f;
        Debug.Log(GameObject.FindObjectOfType<DifficultyHolder>().difficulty);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
