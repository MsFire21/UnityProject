using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int playerHealth;
    public static bool gameOver;
    public static bool isPaused;
    public TextMeshProUGUI playerHealthText;
    public TMP_Text gameOverText;
    public GameObject RedOverlay;
    public Button restartButton;
    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI boardCountText;
    public GameObject pauseMenu; // Добавьте эту строку

    void Start()
    {
        playerHealth = 20;
        gameOver = false;
        isPaused = false;
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false); // Убедитесь, что меню паузы скрыто при запуске
    }

    void Update()
    {
        playerHealthText.text = "" + playerHealth;

        if (gameOver)
        {
            gameOverText.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            restartButton.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        int keyCount = InventoryManager.GetItemCount("Key");
        int boardCount = InventoryManager.GetItemCount("Board");

        keyCountText.text = " " + keyCount;
        boardCountText.text = " " + boardCount;
    }

    public IEnumerator Damage(int DamageCount)
    {
        playerHealth -= DamageCount;
        RedOverlay.SetActive(true);

        if (playerHealth <= 0)
        {
            gameOver = true;
        }

        yield return new WaitForSeconds(0.1f);
        RedOverlay.SetActive(false);
    }

    public void RestartGame()
    {
        gameOver = false;
        Time.timeScale = 1f;
        InventoryManager.ClearInventory();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Убедитесь, что у вас есть сцена с названием MainMenu
    }

    public static bool IsGamePaused()
    {
        return isPaused;
    }
}
