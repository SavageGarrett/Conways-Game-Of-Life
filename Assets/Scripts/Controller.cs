using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField]
    GameObject camera;

    [SerializeField]
    int simulationSize = 10;

    [Header("Dead Cells")]

    [SerializeField]
    GameObject objectParent;

    [SerializeField]
    GameObject deadPrefab;

    List<List<GameObject>> deadCells;

    [Header("Live Cells")]
    [SerializeField]
    GameObject liveParent;

    [SerializeField]
    GameObject livePrefab;

    List<List<GameObject>> liveCells;

    // Start is called before the first frame update
    void Start()
    {
        PrimeCells(deadCells, deadPrefab, 0f);
        PrimeCells(liveCells, livePrefab, -1.0f);

        // Place and rotate the camera
        camera.transform.position = new Vector3(simulationSize * 0.5f, simulationSize * 0.5f, simulationSize * 0.5f);
        camera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void PrimeCells(List<List<GameObject>> cells, GameObject prefab, float yPos)
    {
        // Initialize the cell list
        cells = new List<List<GameObject>>();
        for (int row = 0; row < simulationSize; row++)
        {
            List<GameObject> columnList = new List<GameObject>();
            for (int col = 0; col < simulationSize; col++)
            {
                GameObject cell = Instantiate(prefab, objectParent.transform);
                cell.transform.position = new Vector3(row + 0.5f, yPos, col + 0.5f);
                columnList.Add(cell);
            }
            cells.Add(columnList);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
