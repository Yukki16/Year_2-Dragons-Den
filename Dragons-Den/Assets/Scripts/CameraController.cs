using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform minimPoz;
    [SerializeField] Transform maximPoz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) && this.transform.position.x > minimPoz.position.x)
        {
            Vector3 newPoz = new Vector3(this.transform.position.x - speed, 0, -10);
            this.transform.position = newPoz;
        }

        if (Input.GetKey(KeyCode.D) && this.transform.position.x < maximPoz.position.x)
        {
            Vector3 newPoz = new Vector3(this.transform.position.x + speed, 0, -10);
            this.transform.position = newPoz;
        }
    }

}
