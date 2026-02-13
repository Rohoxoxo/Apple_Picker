using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;

    private Text uiText;

    private void Start()
    {
        uiText = GetComponent<Text>();
    }

    private void Update()
    {
        uiText.text = score.ToString("#,0");
    }
}
