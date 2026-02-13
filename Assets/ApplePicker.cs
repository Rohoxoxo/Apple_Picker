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

    private void Start()
    {
        roundText.text = "Round " + currentRound;
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
        // Clear any apples still falling so the next round starts clean.
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        // Remove one basket (the last one in the list).
        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];

        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        // No baskets left -> advance round, or end game.
        if (basketList.Count != 0) return;

        currentRound++;

        if (currentRound <= maxRounds)
        {
            roundText.text = "Round " + currentRound;

            // Respawn baskets for the new round.
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
            roundText.text = "Game Over";
            restartButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
