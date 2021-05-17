using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    
    public List<Enemy> enemiesList;
    public EndScreen endScreen;
    public PauseMenu pauseMenu;
    public Component[] enemies;
    bool won = false;

    void Start()
    {
        enemiesList = new List<Enemy>();
        enemies = GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemiesList.Add(enemy);
        }

        GameObject.Find("Global Light 2D").GetComponent<Light2D>().intensity = GameObject.Find("SetDifficulty").GetComponent<DifficultyHolder>().difficulty;
    }

    void Update()
    {
        foreach (Enemy enemy in enemiesList)
        {
            if(enemy.GetDeath())
            {
                enemiesList.Remove(enemy);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.isPaused)
            {
                pauseMenu.Resume();
            }else
            {
                pauseMenu.Pause();
            }
        }
        
        if(enemiesList.Count <= 0)
        {
            if (!won)
            {
                FindObjectOfType<AudioManager>().Play("Victory");
                won = true;
            }
            endScreen.EndGame();
        }
    }
}
