using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float horangel = 0;
    private float vertanger = 0;

    public GameObject player;
    public GameObject head;

    public float speedRotation = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        horangel += Input.GetAxis("Mouse X");
        vertanger += Input.GetAxis("Mouse Y");

        vertanger = Mathf.Clamp(vertanger, -60, 40);

        player.transform.rotation = Quaternion.Euler(0, horangel*speedRotation, 0);
        head.transform.rotation = Quaternion.Euler(-vertanger*speedRotation, horangel*speedRotation, 0);

        
    }
}
