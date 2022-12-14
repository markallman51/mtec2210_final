using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector3 target1;
    public Vector3 target2;
    public Vector3 currentTarget;

    private Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);

    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        currentTarget = target1;
    }

    // Update is called once per frame
    void Update()
    {
         if(transform.position == currentTarget ||
            (transform.position.x < currentTarget.x + offset.x && transform.position.y < currentTarget.y + offset.y) ||
            (transform.position.x > currentTarget.x + offset.x && transform.position.y > currentTarget.y + offset.y))
            SwitchTarget();
        
        //if not at destination
        if(transform.position != currentTarget)
        {

            //determine what direction to move in to get to target
            //move right
            if(transform.position.x < currentTarget.x)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
                Debug.Log(target2 - target1);
            }
                

            //move left
            if (transform.position.x > currentTarget.x)
                transform.Translate(-speed * Time.deltaTime, 0, 0);

            //move up
            if (transform.position.y < currentTarget.y)
                transform.Translate(0, speed * Time.deltaTime, 0);

            //move down
            if (transform.position.y > currentTarget.y)
                transform.Translate(0, -speed * Time.deltaTime, 0);


        }

        void SwitchTarget()
        {
            if(currentTarget == target1)
                currentTarget = target2;

            else if(currentTarget == target2)
                currentTarget = target1;
        }
        
    }
}
