using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;



    [SerializeField] KeyCode reloadKey;

    void Update() {
        if (Input.GetMouseButton(0)) {
            shootInput?.Invoke();
        }
        if (Input.GetKeyDown(reloadKey)) {
            reloadInput?.Invoke();
        }

    }
}
