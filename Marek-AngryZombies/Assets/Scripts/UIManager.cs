using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;

    public TextMeshProUGUI ammoInMag;
    public TextMeshProUGUI ammoCarrying;
    public TextMeshProUGUI currentWeapon;
    public TextMeshProUGUI health;

    private bool isInMenu = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isInMenu)
        {
            Stop();
        } else if (Input.GetKeyDown(KeyCode.Escape) && isInMenu)
        {
            Resume();
        }
    }

    private void Stop()
    {
        playerCombat.enabled = false;
        playerMovement.enabled = false;
        isInMenu = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        playerCombat.enabled = true;
        playerMovement.enabled = true;
        isInMenu = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        //  PlayerPrefs save all things
        Application.Quit();
    }

    public void ChangeText(TextMeshProUGUI tmp, string yourText)
    {
            tmp.text = yourText;
    }

    //  Put this in Game Manager!!
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
