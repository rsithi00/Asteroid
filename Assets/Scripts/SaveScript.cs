using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public int score { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}