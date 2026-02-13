using UnityEngine;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;

    private void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    private void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;

        // Convert screen mouse position to world position.
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = transform.position;
        pos.x = mousePos3D.x;
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;

        // Apple = score.
        if (collidedWith.CompareTag("Apple"))
        {
            Destroy(collidedWith);

            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        }

        // Branch = instant game over.
        if (collidedWith.CompareTag("Branch"))
        {
            Destroy(collidedWith);

            ApplePicker ap = Camera.main.GetComponent<ApplePicker>();
            ap.currentRound = ap.maxRounds + 1;
            ap.roundText.text = "Game Over";
            ap.restartButton.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}
