using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGamepadController : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask floorMask;

    private Animator playerAnimator;
    private Ray ray;
    private RaycastHit raycastHit;
    private Camera mainCamera;
    private float horizontal;
    private float vertical;
    private Vector3 movement;
    private float velocityZ;
    private float velocityX;
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AimTowardMouse();

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0.0f, vertical);

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        velocityX = Vector3.Dot(movement.normalized, transform.right);

        playerAnimator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        playerAnimator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    private void AimTowardMouse()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 2000, floorMask))
        {
            Vector3 dir = raycastHit.point - transform.position;
            dir.y = 0f;
            dir.Normalize();
            transform.forward = dir;
        }
    }
}
