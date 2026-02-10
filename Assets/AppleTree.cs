using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject applePrefab;

    public float speed = 1f;
    public float leftAndRightEdge = 10f;

    [Tooltip("Chance per second to change direction (e.g., 0.5 = 50% chance per second)")]
    public float changeDirChancePerSecond = 0.2f;

    public float appleDropDelay = 1f;

    void Start()
    {
        Invoke(nameof(DropApple), 2f);
    }

    void Update()
    {
        // Move
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Bounce at edges
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // left
        }

        // Random direction change (chance per second, frame-rate independent)
        float chanceThisFrame = changeDirChancePerSecond * Time.deltaTime;
        if (Random.value < chanceThisFrame)
        {
            speed *= -1f;
        }
    }

    void DropApple()
    {
        GameObject apple = Instantiate(applePrefab);
        apple.transform.position = transform.position;

        Invoke(nameof(DropApple), appleDropDelay);
    }
}
