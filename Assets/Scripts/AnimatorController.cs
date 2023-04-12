using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0))
        {
            anim.Play("Base Layer.MiningLoop");
        }
      if(Input.GetMouseButtonUp(0))
        {
            anim.Play("Base Layer.tpose"); 
        }
    }
}
