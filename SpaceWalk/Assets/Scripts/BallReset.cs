using System.Collections;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Rigidbody _rigidbody;
    
    public void Start()
    {
        var transform1 = transform;
        _initialPosition = transform1.position;
        _initialRotation = transform1.rotation;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Terrain") return;
        StartCoroutine(ResetPosition());
    }

    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(2f);
        var transform1 = transform;

        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        transform1.rotation = _initialRotation;
        transform1.position = _initialPosition;
        _rigidbody.isKinematic = false;
    }
}
