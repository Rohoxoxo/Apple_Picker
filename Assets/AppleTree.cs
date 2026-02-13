using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Apple Settings")]
    public GameObject applePrefab;
    public GameObject branchPrefab;

    [Range(0f, 1f)]
    public float branchDropChance = 0.1f; // 10% chance to drop branch

    public float speed = 1f;
    public float appleDropDelay = 1f;

    private float leftAndRightEdge = 10f;

    void Start()
    {
        Invoke(nameof(DropApple), appleDropDelay);
    }

    void Update()
    {
        // Move left and right
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Change direction at screen edges
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    void DropApple()
    {
        GameObject objToDrop;

        // Random chance to drop branch instead
        if (Random.value < branchDropChance)
        {
            objToDrop = Instantiate(branchPrefab);
        }
        else
        {
            objToDrop = Instantiate(applePrefab);
        }

        objToDrop.transform.position = transform.position;

        Invoke(nameof(DropApple), appleDropDelay);
    }
}
