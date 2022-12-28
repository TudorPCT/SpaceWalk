using System.Collections;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    // Rotation speed (degrees per second)
    public float speed = 1;
    public float angularVelocity;
    public Vector3 direction = Vector3.right;

    // Coroutine function to rotate the game object
    IEnumerator Rotate()
    {
        float rotationAmount = angularVelocity / 50f;

        while (true)
        {
            transform.Rotate(direction, rotationAmount * speed);

            yield return null;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Rotate());
    }
}
