using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MenuScript : MonoBehaviour
    {
        [SerializeField] private GameObject _pLost;
        [SerializeField] private GameObject p1effectLost;
        [SerializeField] private Image _soundLock;
        [SerializeField] private Image _musicLock;
        [SerializeField] private Image _effectLock;
        [SerializeField] private Image _lowLock;
        [SerializeField] private Image _mediumLock;
        [SerializeField] private Image _ultraLock;
        [SerializeField] private GameObject _exitMenu;
        [SerializeField] private GameObject _tochMenu;
        [SerializeField] private GameObject _canvasMenu;
        [SerializeField] private GameObject _setMenu;
        [SerializeField] private GameObject _levelMenu;
        [SerializeField] private GameObject _instructionMenu;
        [SerializeField] private int _musicOFF = 0;
        [SerializeField] private int _soundOFF = 0;
        [SerializeField] private int _musicON = 1;
        [SerializeField] private int _soundON = 1;
        [SerializeField] private int _effectOFF = 0;
        [SerializeField] private int _effectON = 1;

        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private TextMeshProUGUI _recordText;
        [SerializeField] private TextMeshProUGUI _coinsText;

        [SerializeField] private int _lvlcmplt;
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private List<ParticleSystem> _particles;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("Sound"))       //активация звуков при первом включении
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
            _lvlcmplt = PlayerPrefs.GetInt("LevelSave", 1);

            for (int i = 0; i < _buttons.Count; i++)       // блокировка непройденных уровней
            {
                _buttons[i].interactable = false;
            }
            for (int i = 0; i < _lvlcmplt; i++)
            {
                _buttons[i].interactable = true;
            }

            if (PlayerPrefs.GetInt("Sound") == _soundOFF)                  // параметры звука при старте
            {
                AudioListener.pause = true;
                _soundLock.gameObject.SetActive(true);

            }

            else
            {
                AudioListener.pause = false;
                _soundLock.gameObject.SetActive(false);

            }

            if (PlayerPrefs.GetInt("Music") == _musicOFF)
            {
                _pLost.GetComponent<AudioSource>().mute = true;
                _musicLock.gameObject.SetActive(true);

            }

            else
            {
                _pLost.GetComponent<AudioSource>().mute = false;
                _musicLock.gameObject.SetActive(false);

            }

            if (PlayerPrefs.GetInt("Music1eff") == _effectOFF)             //отключение зыуковых эффектов
            {
                p1effectLost.GetComponent<AudioSource>().mute = true;

                _effectLock.gameObject.SetActive(true);
                foreach (ParticleSystem partikl in _particles)
                {
                    partikl.GetComponent<AudioSource>().mute = true;
                }
            }

            else
            {
                p1effectLost.GetComponent<AudioSource>().mute = false;

                _effectLock.gameObject.SetActive(false);

                foreach (ParticleSystem partikl in _particles)
                {
                    partikl.GetComponent<AudioSource>().mute = false;
                }
            }
            UpdateRecord();                                // обновление рекорда очков
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape) && (_setMenu.activeSelf == true || _levelMenu.activeSelf == true || _instructionMenu.activeSelf == true || _tochMenu.activeSelf == true))   //  остлеживание кнопки шаг назад
            {
                _canvasMenu.SetActive(true);
                _levelMenu.SetActive(false);
                _setMenu.SetActive(false);
                _instructionMenu.SetActive(false);
                _tochMenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _exitMenu.activeSelf == true && _canvasMenu.activeSelf == false)
            {
                _canvasMenu.SetActive(true);
                _exitMenu.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _canvasMenu.activeSelf == true)
            {
                _exitMenu.SetActive(true);
                _canvasMenu.SetActive(false);
            }
            //if (Input.GetKeyDown(KeyCode.Escape) && exitMenu.activeSelf == true)  //&& canvasMenu.activeSelf == false
            //{
            //    exitMenu.SetActive(false);
            //    canvasMenu.SetActive(true);
            //}
        }

        public void UpdateRecord()
        {
            _recordText.text = _scoreManager.Record.ToString();
            _coinsText.text = _scoreManager.Coins.ToString();
        }
        public void StartButton()
        {
            if (PlayerPrefs.GetInt("LevelSave") > 0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("LevelSave"));
                _scoreManager.Score = 0;
            }
            else
            {
                SceneManager.LoadScene(1);
                _scoreManager.Score = 0;
            }
        }
        public void ExitGame()
        {
            Application.Quit();
        }

        public void ExitGamePanel()                               //панель подтверждения выхода
        {
            _exitMenu.SetActive(true);
            _canvasMenu.SetActive(false);
        }

        public void Low()
        {
            //GetComponent<AudioSource>().Play();
            QualitySettings.SetQualityLevel(0, true);
            _lowLock.gameObject.SetActive(true);
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
            _pLost.GetComponent<AudioSource>().mute = !_pLost.GetComponent<AudioSource>().mute;
            if (_pLost.GetComponent<AudioSource>().mute == true)
            {

                PlayerPrefs.SetInt("Music", _musicOFF);
                _musicLock.gameObject.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("Music", _musicON);
                _musicLock.gameObject.SetActive(false);
            }
        }

        public void MusicEffectsButton()
        {
            //GetComponent<AudioSource>().Play();
            p1effectLost.GetComponent<AudioSource>().mute = !p1effectLost.GetComponent<AudioSource>().mute;
            if (p1effectLost.GetComponent<AudioSource>().mute == true)
            {

                PlayerPrefs.SetInt("Music1eff", _effectOFF);
                _effectLock.gameObject.SetActive(true);
                foreach (ParticleSystem partikl in _particles)
                {
                    partikl.GetComponent<AudioSource>().mute = true;
                }
            }
            else
            {
                PlayerPrefs.SetInt("Music1eff", _effectON);
                _effectLock.gameObject.SetActive(false);
                foreach (ParticleSystem partikl in _particles)
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

                PlayerPrefs.SetInt("Sound", _soundOFF);
                _soundLock.gameObject.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("Sound", _soundON);
                _soundLock.gameObject.SetActive(false);
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
}
