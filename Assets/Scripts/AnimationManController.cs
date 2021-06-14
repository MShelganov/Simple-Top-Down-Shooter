using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManController : MonoBehaviour
{
    private Animator animator;
    private InputController input;
    private int isRuningHash;
    private int isShootingHash;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        input = InputController.Instance;
        isRuningHash = Animator.StringToHash("IsRuning");
        isShootingHash = Animator.StringToHash("IsShooting");
    }

    void Update()
    {
        bool isRuning = animator.GetBool(isRuningHash);
        bool isShooting = animator.GetBool(isShootingHash);
        // Imput contriller sends - Is Moving
        if (!isRuning && input.IsMoving)
        {
            // Set the IsRuning boolean parameter to be true
            animator.SetBool(isRuningHash, true);
        }
        else if (isRuning && !input.IsMoving)
        {
            animator.SetBool(isRuningHash, false);
        }

        if (!isShooting && input.IsShooting)
        {
            animator.SetBool(isShootingHash, true);
        }
        else if (isShooting && !input.IsShooting)
        {
            animator.SetBool(isShootingHash, false);
        }
    }
}
