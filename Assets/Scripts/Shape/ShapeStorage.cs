using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<GameObject> shapePrefabs;
    public GameObject shapeOfMe, shapeOfYou;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestRandomShapeGenerator());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TestRandomShapeGenerator()
    {
        while (true)
        {
            var i = Random.Range(0, shapePrefabs.Count);
            GameObject temp = Instantiate(shapePrefabs[i], shapeOfMe.transform);
            yield return new WaitForSeconds(2f);
            Destroy(temp);
        }
    }
}
