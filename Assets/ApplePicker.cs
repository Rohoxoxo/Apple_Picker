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

    public List<GameObject> basketList;

    void Start()
    {
        // ✅ Show Round number at start
        roundText.text = "Round " + currentRound;

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
        // Destroy all of the falling Apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");

        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        // Destroy one of the Baskets
        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];

        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        // If there are no Baskets left, restart the game
        if (basketList.Count == 0)
        {
            SceneManager.LoadScene("_Scene_0");
        }
    }
}
