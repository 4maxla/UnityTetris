using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shape : MonoBehaviour
{

    public static Vector3 moveLeft = new Vector3(-1, 0, 0);
    public static Vector3 moveRight = new Vector3(1, 0, 0);
    public static Vector3 moveDown = new Vector3(0, -1, 0);
    public static Vector3 moveUp = new Vector3(0, 1, 0);

    public bool m_canRotate = true; // Vai figūra var rotēt

    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }

    public void MoveLeft() // Figūras kustība pa kreisi
    {
        Move(moveLeft);
    }

    public void MoveRight() // Figūras kustība pa labi
    {
        Move(moveRight);
    }

    public void MoveDown() // Figūras kustība uz leju
    {
        Move(moveDown);
    }

    public void MoveUp() // Figūras kustība uz leju
    {
        Move(moveUp);
    }

    public void RotateRight() // Rotācija pa labi
    {
        if (m_canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
    }

    public void RotateLeft() //  Rotācija pa kreisi
    {
        if (m_canRotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }
}
