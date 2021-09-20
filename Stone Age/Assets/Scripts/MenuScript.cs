using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject pLost;
    public GameObject p1effectLost;
    [SerializeField] private Image soundLock;
    [SerializeField] private Image musicLock;
    [SerializeField] private Image effectLock;
    [SerializeField] private Image lowLock;
    [SerializeField] private Image mediumLock;
    [SerializeField] private Image ultraLock;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject tochMenu;
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject setMenu;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject instructionMenu;
    [SerializeField] private int musicOFF = 0;
    [SerializeField] private int soundOFF = 0;
    [SerializeField] private int musicON = 1;
    [SerializeField] private int soundON = 1;
    [SerializeField] private int effectOFF = 0;
    [SerializeField] private int effectON = 1;

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI recordText;
    
    [SerializeField] private int lvlcmplt;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<ParticleSystem> particles;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Sound"))       //активация звуков при 1 включении
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        if (!PlayerPrefs.HasKey("Music1eff"))
        {
            PlayerPrefs.SetInt("Music1eff", 1);
        }
        if (!PlayerPrefs.HasKey("LevelSave"))
        {
            PlayerPrefs.SetInt("LevelSave", 1);
        }
    }
    void Start()
    {
        lvlcmplt = PlayerPrefs.GetInt("LevelSave", 1);
       
        for (int i = 0; i < buttons.Count; i++)       // блокировка непройденных уровней
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < lvlcmplt; i++)
        {
            buttons[i].interactable = true;
        }

        if (PlayerPrefs.GetInt("Sound") == soundOFF)                  // параметры звука при старте
        {
            AudioListener.pause = true;
            soundLock.gameObject.SetActive(true);

        }

        else
        {
            AudioListener.pause = false;
            soundLock.gameObject.SetActive(false);

        }

        if (PlayerPrefs.GetInt("Music") == musicOFF)
        {
            pLost.GetComponent<AudioSource>().mute = true;
            musicLock.gameObject.SetActive(true);

        }

        else
        {
            pLost.GetComponent<AudioSource>().mute = false;
            musicLock.gameObject.SetActive(false);

        }

        if (PlayerPrefs.GetInt("Music1eff") == effectOFF)             //отключение зыуковых эффектов
        {
            p1effectLost.GetComponent<AudioSource>().mute = true;
            
            effectLock.gameObject.SetActive(true);
            foreach(ParticleSystem partikl in particles)
            {
                partikl.GetComponent<AudioSource>().mute = true;
            }
        }

        else
        {
            p1effectLost.GetComponent<AudioSource>().mute = false;

            effectLock.gameObject.SetActive(false);

            foreach (ParticleSystem partikl in particles)
            {
                partikl.GetComponent<AudioSource>().mute = false;
            }
        }
        UpdateRecord();                                // обновление рекорда очков
    }

    private void Update()
    {          

        if (Input.GetKeyDown(KeyCode.Escape) && (setMenu.activeSelf == true || levelMenu.activeSelf == true || instructionMenu.activeSelf == true || tochMenu.activeSelf == true))   //  остлеживание кнопки шаг назад
        {
            canvasMenu.SetActive(true);
            levelMenu.SetActive(false);
            setMenu.SetActive(false);
            instructionMenu.SetActive(false);
            tochMenu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && exitMenu.activeSelf == true && canvasMenu.activeSelf == false)
        {
            canvasMenu.SetActive(true);
            exitMenu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && canvasMenu.activeSelf == true)
        {
            exitMenu.SetActive(true);
            canvasMenu.SetActive(false);           
        }
        //if (Input.GetKeyDown(KeyCode.Escape) && exitMenu.activeSelf == true)  //&& canvasMenu.activeSelf == false
        //{
        //    exitMenu.SetActive(false);
        //    canvasMenu.SetActive(true);
        //}
    }

    public void UpdateRecord()
    {
        recordText.text = scoreManager.Record.ToString();
    }
    public void StartButton()
    {
        if (PlayerPrefs.GetInt("LevelSave") > 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelSave"));
            scoreManager.Score = 0;
            scoreManager.Hunger = 250;
        }
        else
        {
            SceneManager.LoadScene(1);
            scoreManager.Score = 0;
            scoreManager.Hunger = 300;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void ExitGamePanel()                               //панель подтверждения выхода
    {
        exitMenu.SetActive(true);
        canvasMenu.SetActive(false);
    }

    public void Low()
    {
        //GetComponent<AudioSource>().Play();
        QualitySettings.SetQualityLevel(0, true);
        lowLock.gameObject.SetActive(true);
    }
    public void Medium()
    {
        //GetComponent<AudioSource>().Play();                       запуск звука кнопки, если не сработает основной вариант
        QualitySettings.SetQualityLevel(2, true);        
    }
    public void Ultra()
    {
        // GetComponent<AudioSource>().Play();
        QualitySettings.SetQualityLevel(4, true);
    }
    public void MusicButton()
    {
        //GetComponent<AudioSource>().Play();
        pLost.GetComponent<AudioSource>().mute = !pLost.GetComponent<AudioSource>().mute;
        if (pLost.GetComponent<AudioSource>().mute == true)
        {
           
            PlayerPrefs.SetInt("Music", musicOFF);
            musicLock.gameObject.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Music", musicON);
            musicLock.gameObject.SetActive(false);
        }
    }

    public void MusicEffectsButton()
    {
        //GetComponent<AudioSource>().Play();
        p1effectLost.GetComponent<AudioSource>().mute = !p1effectLost.GetComponent<AudioSource>().mute;
        if (p1effectLost.GetComponent<AudioSource>().mute == true)
        {
            
            PlayerPrefs.SetInt("Music1eff", effectOFF);
            effectLock.gameObject.SetActive(true);
            foreach (ParticleSystem partikl in particles)
            {
                partikl.GetComponent<AudioSource>().mute = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Music1eff", effectON);
            effectLock.gameObject.SetActive(false);
            foreach (ParticleSystem partikl in particles)
            {
                partikl.GetComponent<AudioSource>().mute = false;
            }
        }
    }
    public void SoundButton()
    {
        
        AudioListener.pause = !AudioListener.pause;
        if (AudioListener.pause)
        {
            
            PlayerPrefs.SetInt("Sound", soundOFF);
            soundLock.gameObject.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", soundON);
            soundLock.gameObject.SetActive(false);
        }
    }

    public void ResettButton()
    {
        //GetComponent<AudioSource>().Play();
        print(PlayerPrefs.GetInt("LevelSave"));
        PlayerPrefs.SetInt("LevelSave", 1);
        PlayerPrefs.SetInt("Record", 0);
    }

    public void Start1Button()
    {
        //GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(1);
    }
    public void Start2Button()
    {
        //GetComponent<AudioSource>().Play();
        if (PlayerPrefs.GetInt("LevelSave") >= 2)
        {
            SceneManager.LoadScene(2);            
        }
    }

    public void Start3Button()
    {
        //GetComponent<AudioSource>().Play();
        if (PlayerPrefs.GetInt("LevelSave") >= 3)
        {
            SceneManager.LoadScene(3);            
        }
    }

    public void Start4Button()
    {
        //GetComponent<AudioSource>().Play();
        if (PlayerPrefs.GetInt("LevelSave") >= 4)
        {
            SceneManager.LoadScene(4);
        }
    }
    public void Start5Button()
    {
        //GetComponent<AudioSource>().Play();
        if (PlayerPrefs.GetInt("LevelSave") >= 5)
        {
            SceneManager.LoadScene(5);
        }
    }
    public void Start6Button()
    {
        //GetComponent<AudioSource>().Play();
        if (PlayerPrefs.GetInt("LevelSave") >= 6)
        {
            SceneManager.LoadScene(6);
        }
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
