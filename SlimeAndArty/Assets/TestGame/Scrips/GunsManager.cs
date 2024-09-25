using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("Respon", 0.5f);
        Invoke("Saraju", 0.2f);
        
    }
    void Respon()
    {
        gameObject.SetActive(true);
    }
    void Saraju()
    {
        gameObject.SetActive(false);
    }
}
