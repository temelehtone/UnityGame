using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle; // The RecTransform of reticle UI element.

    [SerializeField] float restingSize;
    [SerializeField] float maxSize;
    [SerializeField] float speed;
    private float currentSize;

    private void Start()
    {

        reticle = GetComponent<RectTransform>();

    }

    private void Update()
    {

        
        if (isMoving)
        {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);

    }

    public bool isMoving
    {
        get
        {
            if (
                Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0

                    )
                return true;
            else
                return false;
        }
    }


}
