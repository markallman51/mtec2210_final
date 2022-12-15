using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class helper : MonoBehaviour
{
    public GameObject message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && this.tag != "Safety Platform" && this.tag != "Win")
        {
            message.SetActive(false);
        }
    }
}
