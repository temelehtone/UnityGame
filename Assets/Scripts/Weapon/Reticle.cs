using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle; // The RecTransform of reticle UI element.

    public Rigidbody playerRigidbody;

    public float restingSize;
    public float maxSize;
    public float speed;
    private float currentSize;

    private void Start() {

        reticle = GetComponent<RectTransform>();

    }

    private void Update() {

        // Check if player is currently moving and Lerp currentSize to the appropriate value.
        if (isMoving) {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        } else {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        // Set the reticle's size to the currentSize value.
        reticle.sizeDelta = new Vector2(currentSize, currentSize);

    }

    bool isMoving
    {
        get
        {
            if (
                Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0 ||
                Input.GetAxis("Mouse X") != 0 ||
                Input.GetAxis("Mouse Y") != 0
                    )
                return true;
            else
                return false;
        }
    }


}
