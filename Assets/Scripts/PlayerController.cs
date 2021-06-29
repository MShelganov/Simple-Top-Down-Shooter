using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance { get => instance; }

    public bool IsMoving { get; private set; }
    public bool IsShooting { get; private set; }

    public float speed = 1000f;
    public LayerMask floorMask;

    public GameObject projectile;
    public float fireDelta = 0.5F;

    private Ray ray;
    private RaycastHit raycastHit;
    private Camera mainCamera;
    private float horizontal;
    private float vertical;
    private float rollY;
    private float rollX;
    private Vector3 movement;

    private float nextFire = 0.5F;
    //private GameObject newProjectile;
    private float myTime = 0.0F;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        IsMoving = false;
        myTime = myTime + Time.deltaTime;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        rollY = Input.GetAxis("Right Joystick Y");
        rollX = Input.GetAxis("Right Joystick X");

        AimTowardMouse();

        movement = new Vector3(horizontal, 0.0f, vertical);

        if (movement.magnitude > 0)
        {
            IsMoving = true;
            movement.Normalize();
            movement *= speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        if (Input.GetAxis("Fire1") > 0 && myTime > nextFire)
        {
            IsShooting = true;
            nextFire = myTime + fireDelta;
            //newProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;

            // create code here that animates the newProjectile

            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
        else if(Input.GetAxis("Fire1") == 0)
        {
            IsShooting = false;
        }
    }

    private void AimTowardMouse()
    {
        if (Config.Gamepad)
        {
            var rollAxis = new Vector3(rollX, 0.0f, rollY);

            if (rollAxis.magnitude > 0)
            {
                rollAxis.Normalize();
                transform.forward = rollAxis;
            }
        }
        else {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 2000, floorMask))
            {
                Vector3 dir = raycastHit.point - transform.position;
                dir.y = 0.0f;
                dir.Normalize();
                transform.forward = dir;
            }
        }
    }
}
