using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    public float mousePosX;
    public float mousePosY;

    [SerializeField] float movementQuantity;

    // Update is called once per frame
    void Update()
    {
        mousePosX = Input.mousePosition.x;
        mousePosY = Input.mousePosition.y;

        this.GetComponent<RectTransform>().position = new Vector2(

            (mousePosX / Screen.width) * movementQuantity + (Screen.width / 2),
            (mousePosY / Screen.height) * movementQuantity + (Screen.height / 2));

    }
}
