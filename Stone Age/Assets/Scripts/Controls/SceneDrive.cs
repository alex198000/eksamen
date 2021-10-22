using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Levels
{
    public class SceneDrive : MonoBehaviour
    {
        [SerializeField] private int _unlockLevel;
        [SerializeField] private GameObject _p1effectLost;
        [SerializeField] private GameObject _pLost;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _loseGame;
        [SerializeField] private GameObject _winGame;
        [SerializeField] private GameObject _exitMenu;
        [SerializeField] private GameObject _canvasMenu;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _hungerText;
        [SerializeField] private TextMeshProUGUI _lifeText;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private HungerManager _hungerManager;
        [SerializeField] private GameEvent _lifeEvent;
        [SerializeField] private SceletonManager _sceletonManager;
        [SerializeField] private Text _textSceletonPlus;
        [SerializeField] private Text _textSceletonMinus;

        public int UnlockLevel { get => _unlockLevel; set => _unlockLevel = value; }
       
        private void OnEnable()
        {
            SkeletonHealth.OnSceletonPlus += UpdateSceleton;
            ObjectDestro.OnSceletonMinus += UpdateSceleton;
            ScoreManager.OnGameWin += CoinsPlus;
            ScoreManager.OnGameWin += WinGame;
        }
        private void OnDisable()
        {
            SkeletonHealth.OnSceletonPlus -= UpdateSceleton;
            ObjectDestro.OnSceletonMinus -= UpdateSceleton;
            ScoreManager.OnGameWin -= CoinsPlus;
            ScoreManager.OnGameWin -= WinGame;
        }

        void Start()
        {
            if (PlayerPrefs.GetInt("Music") == 0)
            {
                _pLost.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                _pLost.GetComponent<AudioSource>().mute = false;
            }
            if (PlayerPrefs.GetInt("Music1eff") == 0)
            {
                _p1effectLost.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                _p1effectLost.GetComponent<AudioSource>().mute = false;
            }
        }

        void Update()
        {
            ESCbutton();
        }

        public void PauseButton()
        {
            if (_winGame.activeSelf != true && _loseGame.activeSelf != true)
            {
                if (_pauseMenu.activeSelf == false)
                {
                    _pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
                else if (_pauseMenu.activeSelf == true)
                {
                    _pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
        public void ResumeButton()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        public void QuitButton()
        {
            Application.Quit();
        }
        public void NoButton()
        {
            if (_exitMenu.activeSelf == true && _loseGame.activeSelf == true)
            {
                _exitMenu.SetActive(false);
                _loseGame.SetActive(true);
            }
            if (_exitMenu.activeSelf == true && _winGame.activeSelf == true)
            {
                _exitMenu.SetActive(false);
                _winGame.SetActive(true);
            }
            if (_exitMenu.activeSelf == true && _pauseMenu.activeSelf == true)
            {
                _exitMenu.SetActive(false);
                _pauseMenu.SetActive(true);
            }
            if (_exitMenu.activeSelf == true)
            {
                _exitMenu.SetActive(false);
                _pauseMenu.SetActive(true);
            }

        }
        public void ExitGameMenu()
        {
            _exitMenu.SetActive(true);
        }

        public void WinGame()
        {
            _winGame.SetActive(true);
            if (PlayerPrefs.GetInt("LevelSave") < _unlockLevel)
            {
                PlayerPrefs.SetInt("LevelSave", _unlockLevel);
            }
        }
        public void CoinsPlus()
        {
           _scoreManager.Coins += 25;
            PlayerPrefs.SetInt("Coins", _scoreManager.Coins);
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
            _scoreManager.Score = 0;
        }

        public void NextLevelButton()
        {
            SceneManager.LoadScene(_unlockLevel);
            Time.timeScale = 1;
            _scoreManager.Score = 0;
        }
        public void UpdateScore()
        {
            _scoreText.text = _scoreManager.Score.ToString();
            _hungerText.text = _hungerManager.Hunger.ToString();
            _lifeText.text = _hungerManager.Life.ToString();
        }

        public void UpdateSceleton()
        {
            _textSceletonPlus.text = _sceletonManager.SceletonPlus.ToString();
            _textSceletonMinus.text = _sceletonManager.SceletonMinus.ToString();
        }

        void ESCbutton()                             // кнопка esc или  шаг назад на телефоне
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _pauseMenu.activeSelf == false && _exitMenu.activeSelf == false && _winGame.activeSelf == false && _loseGame.activeSelf == false)     // Отслеживание нажатия кнопки шаг назад в планшете или esc на клавиатуре
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _pauseMenu.activeSelf == true && _exitMenu.activeSelf == false && _winGame.activeSelf == false && _loseGame.activeSelf == false)
            {
                _pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _exitMenu.activeSelf == true && _winGame.activeSelf == false && _loseGame.activeSelf == false)
            {
                _pauseMenu.SetActive(true);
                _exitMenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _exitMenu.activeSelf == true && _winGame.activeSelf == true)
            {
                _winGame.SetActive(true);
                _exitMenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _exitMenu.activeSelf == true && _loseGame.activeSelf == true)
            {
                _loseGame.SetActive(true);
                _exitMenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _exitMenu.activeSelf == true && _pauseMenu.activeSelf == true)
            {
                _pauseMenu.SetActive(true);
                _exitMenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && (_loseGame.activeSelf == true || _winGame.activeSelf == true || _pauseMenu.activeSelf == true))
            {
                //loseGame.SetActive(true);
                _exitMenu.SetActive(true);
            }
        }
    }
}
