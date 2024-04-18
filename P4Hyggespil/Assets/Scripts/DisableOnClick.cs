using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnClick : MonoBehaviour
{
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
