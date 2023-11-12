using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public FadeController fadeController;
    private Animator anim;//アニメーション

    void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("openDoor");
            PlayerA.Operable = 0;
        }
    }

    public void NextScene()
    {
        fadeController.StartCoroutine(fadeController.FadeOutBlack());
    }
}
