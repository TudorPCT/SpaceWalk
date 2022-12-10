using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    #region MonoBehaviour Callbacks

    [SerializeField]
    private float directionDampTime = 0.25f;
    private Animator _animator;
    
    // Use this for initialization
    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (!_animator)
        {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }


    // Update is called once per frame
    private void Update()
    {
        if (!_animator)
        {
            return;
        }
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (v < 0)
        {
            v = 0;
        }
        _animator.SetFloat("Speed", h * h + v * v);
        _animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
    }


    #endregion
}

