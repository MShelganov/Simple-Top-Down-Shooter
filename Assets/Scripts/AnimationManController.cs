using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManController : MonoBehaviour
{
    public ParticleSystem fireParticles;
    private Animator animator;
    private PlayerController input;
    private int isRuningHash;
    private int isShootingHash;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        input = PlayerController.Instance;
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
            if (fireParticles != null && !fireParticles.isPlaying)
                fireParticles.Play();
            animator.SetBool(isShootingHash, true);
        }
        else if (isShooting && !input.IsShooting)
        {
            if (fireParticles != null && fireParticles.isPlaying)
                fireParticles.Stop();
            animator.SetBool(isShootingHash, false);
        }
    }
}
