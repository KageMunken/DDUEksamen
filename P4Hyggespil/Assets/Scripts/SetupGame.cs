using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    [SerializeField] GameObject NotDestroyedOnLoad;

    public void Setup()
    {
        NotDestroyedOnLoad.SetActive(true);
    }
}
