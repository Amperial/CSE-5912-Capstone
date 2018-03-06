using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterMonoBehaviour : MonoBehaviour {
    public static MasterMonoBehaviour Instance;
    public GameObject loadScreen;
    public Slider slider;
    public Text progressTxt;
    void Awake()
    {
        Instance = this;
    }
}
