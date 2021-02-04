using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturitoProductionsPresentsController : MonoBehaviour
{
    void EndAnimation()
    {
        LevelLoaderController.instance.LoadScene("Menu");
    }
}
