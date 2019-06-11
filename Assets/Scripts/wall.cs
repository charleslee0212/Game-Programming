using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{

    public GameObject otherObject;
    Animator otherAnim;
    // Start is called before the first frame update
    void Start()
    {
        otherAnim = otherObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (otherAnim.GetBool("isDead"))
        {
            Destroy(gameObject);
        }
    }
}
