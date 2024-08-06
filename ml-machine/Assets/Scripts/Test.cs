using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private bool cats = true;
    void Start()
    {
        if (!cats)
        {
            while (true)
            {
                GameObject cases = GameObject.Find("Cats");
                if (cases == null)
                {
                    break;
                }
                Debug.Log($"{cases.name} 제거!");
                Destroy(cases);
            }
        }
    }
}
