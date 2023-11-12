using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameChange : MonoBehaviour
{
    private Animator anim;//アニメーション


    //private Camera mainCamera;

    //private void Awake()
    //{
    //    mainCamera = Camera.main;
    //}
    void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        anim.SetInteger("spellNumber", PlayerA.CurrentTriggerIndex);
        
    }
}
