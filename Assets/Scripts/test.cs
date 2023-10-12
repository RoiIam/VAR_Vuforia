using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class test : MonoBehaviour
{
    private bool startScaling = false;

    public float amplitude = 0.2f;
    public float startSize = 0.02f;
    public float growthSpeed = 0.5f;
    public float forestGrowthDelay = 15.0f;
    public float forestNextTreeSpawnDelay = 2.0f;
    public Vector3 treeStartScale = new Vector3(0.05f, 0.05f, 0.05f); //since its uniform we could use float too
    public float treeTargetScale =  0.1f; 

    
    public bool forestGrown = false;
    public float targetSize = 0.8f;

    private Vector3 currentScale;

    public GameObject treePrefab;
    public GameObject bottomLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(startScaling)
        {
            float sine = Mathf.Sin(Time.time) * amplitude;
            Vector3 targetScale = new Vector3(sine,sine,sine);
        this.gameObject.transform.localScale= targetScale;
        }   */

        if (Time.realtimeSinceStartup > forestGrowthDelay && !forestGrown)
        {
            StartCoroutine(GrowForest());
            forestGrown = true;
        }
    }

    IEnumerator GrowForest()
    {
        Transform startTransform = bottomLeft.GetComponent<Transform>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //instantiate new tree at grid position, offsets were tested to work nicely
                var gameObject = GameObject.Instantiate(treePrefab,  new Vector3(startTransform.position.x + i * 0.0148f, startTransform.position.y, startTransform.position.z + j * 0.0148f), Quaternion.identity,  this.transform);
                //set the initial scale of tree
                gameObject.transform.localScale = treeStartScale; 
                //start the growth for the new tree
                StartCoroutine(RescaleObjectCoruotine(gameObject, gameObject.transform.localScale, 0.1f));
                yield return new WaitForSeconds(forestNextTreeSpawnDelay);

            }
        }
    }
    public void RescaleObject()
    {
        //startScaling = true;
        currentScale = new Vector3(startSize,startSize,startSize);
        this.gameObject.transform.localScale= currentScale;

        StartCoroutine(RescaleObjectCoruotine(this.gameObject, currentScale));

    }

    IEnumerator RescaleObjectCoruotine(GameObject go,Vector3 startScale,float targetScale =0.8f)
    {

        while (go.transform.localScale.x < targetScale)
        {
            startScale +=  new Vector3(amplitude,amplitude,amplitude)*growthSpeed * Time.deltaTime;
            go.transform.localScale = startScale;
            yield return new WaitForNextFrameUnit();

        }
    }
}
