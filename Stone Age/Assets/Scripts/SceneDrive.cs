using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneDrive : MonoBehaviour
{
    public int unlockLevel;
    [SerializeField] private GameObject p1effectLost;
    [SerializeField] private GameObject pLost;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject loseGame;
    [SerializeField] private GameObject winGame;    
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject canvasMenu;        
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hungerText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private HungerManager hungerManager;
    [SerializeField] private GameEvent lifeEvent;    

    void Start()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            pLost.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            pLost.GetComponent<AudioSource>().mute = false;
        }
        if (PlayerPrefs.GetInt("Music1eff") == 0)
        {
            p1effectLost.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            p1effectLost.GetComponent<AudioSource>().mute = false;
        }       
    }

    void Update()
    {
        ESCbutton();
        
        if (scoreManager.Score >= scoreManager.ScoreGameWin)
        {
            winGame.SetActive(true);
            Time.timeScale = 0;
            if (PlayerPrefs.GetInt("LevelSave") < unlockLevel)
            {
                PlayerPrefs.SetInt("LevelSave", unlockLevel);
            }
        }
    }

    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void NoButton()
    {
        if (exitMenu.activeSelf == true && loseGame.activeSelf == true)
        {
            exitMenu.SetActive(false);
            loseGame.SetActive(true);
        }
        if (exitMenu.activeSelf == true && winGame.activeSelf == true)
        {
            exitMenu.SetActive(false);
            winGame.SetActive(true);
        }
        if (exitMenu.activeSelf == true && pauseMenu.activeSelf == true)
        {
            exitMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
        if (exitMenu.activeSelf == true)
        {
            exitMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }

    }
    public void ExitGameMenu()
    {
        exitMenu.SetActive(true);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        scoreManager.Score = 0;             
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(unlockLevel);
        Time.timeScale = 1;
        scoreManager.Score = 0;        
    }
    public void UpdateScore()
    {
        scoreText.text = scoreManager.Score.ToString();
        hungerText.text = hungerManager.Hunger.ToString();
        lifeText.text = hungerManager.Life.ToString();
    }

    void ESCbutton()                             // кнопка esc или  шаг назад на телефоне
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false && exitMenu.activeSelf == false && winGame.activeSelf == false && loseGame.activeSelf == false)     // Отслеживание нажатия кнопки шаг назад в планшете или esc на клавиатуре
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true && exitMenu.activeSelf == false && winGame.activeSelf == false && loseGame.activeSelf == false)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && exitMenu.activeSelf == true && winGame.activeSelf == false && loseGame.activeSelf == false)
        {
            pauseMenu.SetActive(true);
            exitMenu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && exitMenu.activeSelf == true && winGame.activeSelf == true)
        {
            winGame.SetActive(true);
            exitMenu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && exitMenu.activeSelf == true && loseGame.activeSelf == true)
        {
            loseGame.SetActive(true);
            exitMenu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && exitMenu.activeSelf == true && pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(true);
            exitMenu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (loseGame.activeSelf == true || winGame.activeSelf == true || pauseMenu.activeSelf == true))
        {
            //loseGame.SetActive(true);
            exitMenu.SetActive(true);
        }
    }

}
