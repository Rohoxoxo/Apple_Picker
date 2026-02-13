using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Apple Settings")]
    public GameObject applePrefab;
    public GameObject branchPrefab;

    [Range(0f, 1f)]
    public float branchDropChance = 0.1f;

    public float speed = 1f;
    public float appleDropDelay = 1f;

    private float leftAndRightEdge = 10f;

    private void Start()
    {
        Invoke(nameof(DropApple), appleDropDelay);
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Bounce off the edges.
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    private void DropApple()
    {
        GameObject objToDrop;

        // Sometimes drop a branch instead of an apple.
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
