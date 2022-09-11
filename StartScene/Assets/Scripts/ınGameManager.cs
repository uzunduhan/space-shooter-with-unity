using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ınGameManager : MonoBehaviour
{
    public GameObject rocketPrefab;
    public Transform rocketSpawnPoint1, rocketSpawnPoint2;

    public float fireInterval = 2f;
    private bool canFire = true;

    public Image healthBarFill;
    public float healthBarChangeTime = 0.5f;

    public GameObject pauseMenu;
    public GameObject deathMenu;

    public void ChangeHealthBar(int maxHealth, int currentHealth)
    {
        if (currentHealth < 0)
            return;

        if(currentHealth == 0)
        {
            Invoke("OpenDeathMenu", healthBarChangeTime);
        }

        float healthPct = currentHealth / (float)maxHealth;
        StartCoroutine(SmoothHealthBarChange(healthPct));
    }

    private IEnumerator SmoothHealthBarChange(float newFillAmt)
    {
        float elapsed = 0f;
        float oldFillAmt = healthBarFill.fillAmount;

        while(elapsed <= healthBarChangeTime)
        {
            elapsed += Time.deltaTime;
            float currentFillAmt = Mathf.Lerp(oldFillAmt, newFillAmt, elapsed / healthBarChangeTime);
            healthBarFill.fillAmount = currentFillAmt;
            yield return null;
        }
    }

    public void OnFireButtonClicked()
    {
        if (canFire)
        {
            FireRockets();

            canFire = false;

            StartCoroutine(ReloadDelay());
        }
    }

    private void FireRockets()
    {
        Instantiate(rocketPrefab, rocketSpawnPoint1.position, Quaternion.identity);
        Instantiate(rocketPrefab, rocketSpawnPoint2.position, Quaternion.identity);


    }

    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(fireInterval);

        canFire = true;
    }
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit App");

        Application.Quit();
    }

    public void OnPuseButtonClicked()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void OnContinouButtonClicked()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenDeathMenu()
    {
        Time.timeScale = 0f;
        deathMenu.SetActive(true);
    }
}
