using System.Collections;
using UnityEngine;

public class HideOnTouch : MonoBehaviour
{

    public GameObject playerB;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("moveObj"))
        {
            StartCoroutine(DeactivateAndReactivate());
        }
    }

    IEnumerator DeactivateAndReactivate()
    {
        playerB.SetActive(false);
        yield return new WaitForSeconds(3f);
        playerB.SetActive(true);
    }
    
}
