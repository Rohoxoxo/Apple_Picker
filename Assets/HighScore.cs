using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // We need this line for uGUI to work.

public class HighScore : MonoBehaviour
{
    static private Text _UI_TEXT;
    static private int _SCORE = 1000;

    private Text txtCom;   // txtCom is a reference to this GO's Text component

    void Awake()
    {
        _UI_TEXT = GetComponent<Text>();

        // If the PlayerPrefs HighScore already exists, read it
        if (PlayerPrefs.HasKey("HighScore"))
        {
            SCORE = PlayerPrefs.GetInt("HighScore");
        }

        // Make sure HighScore key exists in PlayerPrefs
        PlayerPrefs.SetInt("HighScore", SCORE);
    }

    static public int SCORE
    {
        get { return _SCORE; }

        private set
        {
            _SCORE = value;

            // Save new high score to PlayerPrefs
            PlayerPrefs.SetInt("HighScore", value);

            if (_UI_TEXT != null)
            {
                _UI_TEXT.text = "High Score: " + value.ToString("#,0");
            }
        }
    }

    static public void TRY_SET_HIGH_SCORE(int scoreToTry)
    {
        if (scoreToTry <= SCORE)
            return; // If scoreToTry is too low, return

        SCORE = scoreToTry;
    }

    // Allows you to reset the PlayerPrefs HighScore from the Inspector
    [Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;

    void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 1000);
            Debug.LogWarning("PlayerPrefs HighScore reset to 1,000.");
        }
    }
}
