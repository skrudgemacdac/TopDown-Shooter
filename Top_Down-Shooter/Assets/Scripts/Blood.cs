using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.25f);
    }
}
