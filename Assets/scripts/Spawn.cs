using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject ringPrefab;
    private Vector2 screenBounds;
    public GameObject anchorPoint;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(spawnRocks());
        StartCoroutine(spawnRings());
    }

    private void spawnRock()
    {
        GameObject a = Instantiate(rockPrefab) as GameObject;
        a.transform.position = new Vector2(screenBounds.x, anchorPoint.transform.position.y + anchorPoint.GetComponent<Renderer>().bounds.size.y / 2 + 0.25f);
    }

    public void spawnRing()
    {
        GameObject a = Instantiate(ringPrefab) as GameObject;
        a.transform.position = new Vector2(screenBounds.x - 0.4f, anchorPoint.transform.position.y + anchorPoint.GetComponent<Renderer>().bounds.size.y / 2 + 0.25f);
    }

    IEnumerator spawnRocks()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.6f, 2.2f));
            spawnRock();
        }
    }

    IEnumerator spawnRings()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            spawnRing();
        }
    }

}
