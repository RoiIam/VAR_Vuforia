using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    private Camera mainCam;
    private Ray ray;

    private float r;
    public GameObject cubeRB;
    void Start()
    {
        mainCam = Camera.main;
        //Random a = Random();
        //r =  *1.05f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) //RMB is 1
        {
            ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if  (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.gameObject.name);

            }
            Debug.DrawRay(mainCam.transform.position,ray.direction,Color.cyan,1.0f,false);

            StartCoroutine(SpawnCubes(3));
        }
        
    }

    IEnumerator SpawnCubes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var gameObject = GameObject.Instantiate(cubeRB,  new Vector3(transform.parent.position.x, transform.parent.position.y+1f, transform.parent.position.z), Quaternion.identity,  this.transform);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
