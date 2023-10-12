using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherButton : MonoBehaviour
{
    public List<GameObject> models;

    public int startIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        SwitchModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchModel()
    {
        models[ startIndex].SetActive(false);
        startIndex += 1;
        startIndex = startIndex % models.Count;
        models[ startIndex].SetActive(true);
    }
}
