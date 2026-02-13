using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    private void Update()
    {
        if (transform.position.y >= bottomY) return;

        Destroy(gameObject);

        // Only apples should cost a basket (branches shouldn't).
        if (CompareTag("Apple"))
        {
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleMissed();
        }
    }
}
