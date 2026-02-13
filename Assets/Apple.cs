using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            // Destroy this falling object
            Destroy(this.gameObject);

            // ONLY apples should remove a basket
            if (CompareTag("Apple"))
            {
                ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
                apScript.AppleMissed();
            }
        }
    }
}
