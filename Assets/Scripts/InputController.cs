using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private static InputController instance;
    public bool IsMoving { get; private set; }
    public bool IsShooting { get; private set; }

    public static InputController Instance { get => instance; }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        IsMoving = false;
        if (Input.GetKey(KeyCode.W))
        {
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            IsMoving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            IsMoving = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            IsShooting = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsShooting = false;
        }
    }
}
