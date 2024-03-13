using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        
    }


    //public void DestroyFire()
    //{
    //    Destroy(gameObject);
    //}
    //private void Update()
    //{
    //if (Input.GetMouseButtonDown(0))
    //{
    //    gameObject.GetComponent<SpriteRenderer>().enabled = true;
    //}
    //else
    //{
    //    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //}
    //if (Input.GetMouseButtonDown(0))
    //{
    //    Instantiate(gameObject, transform.position, transform.rotation);
    //}
    //else
    //{
    //    gameObject.SetActive(false);
    //}
    //}
}
