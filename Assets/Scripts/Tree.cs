using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject[] treePrefabs;
    public int numberOfTrees;
    public float width;
    public float height;
    public Terrain terrain;
    float xpos, zpos;
    public float x_intit, y_init;

    void Start()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            xpos = Random.Range(x_intit, width);
            zpos = Random.Range(y_init, height);
            int randomIndex = Random.Range(0, treePrefabs.Length);
            Vector3 position = new Vector3(xpos, 0f, zpos);
            position.y = terrain.SampleHeight(position) + terrain.transform.position.y;
            GameObject tree = Instantiate(treePrefabs[randomIndex], position, Quaternion.identity);
        }
    }
}

