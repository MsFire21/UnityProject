using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int playerHealth;
    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;
    public TMP_Text gameOverText;
    public GameObject RedOverlay;
    public Button restartButton;
    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI boardCountText;

    void Start()
    {
        playerHealth = 20;
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
}

