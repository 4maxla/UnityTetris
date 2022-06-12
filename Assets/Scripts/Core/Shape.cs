using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public bool m_canRotate = true; // Vai figūra var rotēt

    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }

    public void MoveLeft() // Figūras kustība pa kreisi
    {
        Move(new Vector3(-1, 0, 0));
    }

    public void MoveRight() // Figūras kustība pa labi
    {
        Move(new Vector3(1, 0, 0));
    }

    public void MoveDown() // Figūras kustība uz leju
    {
        Move(new Vector3(0, -1, 0));
    }

    public void MoveUp() // Figūras kustība uz leju
    {
        Move(new Vector3(0, 1, 0));
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

    // Start is called before the first frame update
    void Start() 
    {
       // InvokeRepeating("MoveDown", 0, 0.5f); // Invoke atkārtoti izsauc izvēlēto metodi ik pēc noteikta laika (Method, StartDelay, RepeatRate)

       // InvokeRepeating("RotateRight", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
