using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnPC : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(!Application.isMobilePlatform);
    }
}
