using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public PlayerHealth playerHealth;
    public GameManager gameManager;

    public GameObject background;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject scoreboard;
    public GameObject enterUsername;

    public TMP_InputField nameInputField;
    public GameObject usernameWarningText;

    public TextMeshProUGUI ammoInMag;
    public TextMeshProUGUI ammoCarrying;
    public TextMeshProUGUI currentWeapon;
    public TextMeshProUGUI health;

    public Leadboard leadboard;
    public List<TextMeshProUGUI> rankingNames;
    public List<TextMeshProUGUI> rankingScores;

    public string playerName = "";

    private bool _isInMenu = false;
    private bool _isInScoreboard = false;
    public bool _isNameAdded = false;
    public bool _isDead = false;
    private bool _isInMainMenu;

    private void Start()
    {
        _isInMainMenu = gameManager.MainMenuCheck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isInMenu && !_isInMainMenu)
        {
            _isInMenu = true;
            Stop();
            pauseMenu.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Escape) && _isInMenu)
        {
            _isInMenu = false;
            Resume();
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !_isInMenu && !_isInScoreboard && !_isInMainMenu)
        {
            _isInScoreboard = true;
            Stop();
            scoreboard.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Tab) && !_isInMenu && _isInScoreboard)
        {
            _isInScoreboard = false;
            Resume();
            scoreboard.SetActive(false);
        }

        if (!_isInMainMenu)
        {
            if (playerHealth._isDead)
            {
                Stop();
                enterUsername.SetActive(true);
            }
        }
    }



    public void getInputValue()
    {
        string text = nameInputField.text;
        playerName = text;
        _isNameAdded = true;
    }

    public void Stop()
    {
        playerCombat.enabled = false;
        playerMovement.enabled = false;
        background.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        playerCombat.enabled = true;
        playerMovement.enabled = true;
        background.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeText(TextMeshProUGUI tmp, string yourText)
    {
        tmp.text = yourText;
    }

    public void Play()
    {
        gameManager.LoadNextScene();
    }

    public void ReturToMainMenu()
    {
        Play();
        gameManager.ReturnToMainMenu();
    }

    public void AcceptAndReturnToMainMenu()
    {
        if (_isNameAdded)
        {
            Play();
            gameManager.ReturnToMainMenu();
            return;
        }
        usernameWarningText.SetActive(true);
    }
}
