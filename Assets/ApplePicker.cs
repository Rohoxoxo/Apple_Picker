using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;

    [Header("Round System")]
    public int currentRound = 1;
    public int maxRounds = 4;
    public TMPro.TextMeshProUGUI roundText;
    public GameObject restartButton;

    public List<GameObject> basketList;

    void Start()
    {
        // Show Round number at start
        roundText.text = "Round " + currentRound;

        // Hide Restart button at start
        restartButton.SetActive(false);

        basketList = new List<GameObject>();

        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);

            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);

            tBasketGO.transform.position = pos;

            basketList.Add(tBasketGO);
        }
    }

    public void AppleMissed()
    {
        // Destroy all falling apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        // Destroy one basket
        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];

        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        // If no baskets left → next round or game over
        if (basketList.Count == 0)
        {
            currentRound++;

            if (currentRound <= maxRounds)
            {
                // Update round UI
                roundText.text = "Round " + currentRound;

                // Respawn baskets
                for (int i = 0; i < numBaskets; i++)
                {
                    GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);

                    Vector3 pos = Vector3.zero;
                    pos.y = basketBottomY + (basketSpacingY * i);

                    tBasketGO.transform.position = pos;

                    basketList.Add(tBasketGO);
                }
            }
            else
            {
                // Game Over
                roundText.text = "Game Over";
                restartButton.SetActive(true);
                Time.timeScale = 0f; // Freeze game
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Unfreeze time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
