using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6.0f;
    private float h, v;
    private float camRayLength = 100.0f;
    private int floorMask;

    //flag!!
    //bool enable = true;

    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
    }

    void Update()
    {
        //1.receive player's input
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //2.move character
        Move();
        Turning();

        //set animation(moving <-> idle)
        if (h == 0 && v == 0) 
        {
            //idle
            GetComponent<Animator>().SetBool("IsWalk", false);

        }
        else
        {
            //moving
            GetComponent<Animator>().SetBool("IsWalk", true);
        }

    }

    void Move()
    {
        transform.Translate(h * speed * Time.deltaTime, 0f, v * speed * Time.deltaTime);
    }

    void Turning()
    {
        //1. switch mouse position to ray of 3d space!
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //2. do raycast, before get hitInfo
        if (Physics.Raycast(ray, out hitInfo, camRayLength, floorMask))
        {
            // vector that i 
            Vector3 dir = hitInfo.point - transform.position;
            dir.y = 0;

            //set quaternion which ratate up direct
            Quaternion newRotation = Quaternion.LookRotation(dir);
            transform.rotation = newRotation;
        }
    }


}