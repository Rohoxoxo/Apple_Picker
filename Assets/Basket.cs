using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;

    void Start()
    {
        // Find a GameObject named ScoreCounter in the Scene Hierarchy
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        // Get the ScoreCounter (Script) component of scoreGO
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    void Update()
    {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        // The Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of this Basket to the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll)
    {
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;

        // 🍎 APPLE LOGIC
        if (collidedWith.CompareTag("Apple"))
        {
            Destroy(collidedWith);

            // Increase the score
            scoreCounter.score += 100;

            // Attempt to set the high score
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        }

        // 🌿 BRANCH LOGIC (Instant Game Over)
        if (collidedWith.CompareTag("Branch"))
        {
            // Destroy the branch
            Destroy(collidedWith);

            // Trigger Game Over
            ApplePicker ap = Camera.main.GetComponent<ApplePicker>();
            ap.currentRound = ap.maxRounds + 1; // Force game over
            ap.roundText.text = "Game Over";
            ap.restartButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
