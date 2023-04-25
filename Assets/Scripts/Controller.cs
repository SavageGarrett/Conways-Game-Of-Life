using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CarderGarrett.Lab6 {
    public class Controller : MonoBehaviour
    {
        [Header("Scene")]
        [SerializeField]
        Camera camera;
        [SerializeField]
        float simulationStep = 1f;

        [Header("Data")]
        [SerializeField]
        JSONReader initialData;
        int simulationSize;

        [Header("Dead Cells")]

        [SerializeField]
        GameObject deadParent;

        [SerializeField]
        GameObject deadPrefab;

        List<List<GameObject>> deadCells;

        [Header("Live Cells")]
        [SerializeField]
        GameObject liveParent;

        [SerializeField]
        GameObject livePrefab;

        List<List<GameObject>> liveCells;

        Grid dataRep;

        void Start()
        {
            // Load the JSON File
            List<List<bool>> initialRep = initialData.getInitialDataRep();
            simulationSize = initialRep.Count;

            // Create the procedurally generated objects
            PrimeCells(ref deadCells, deadPrefab, deadParent, 0f);
            PrimeCells(ref liveCells, livePrefab, liveParent, -1.0f);

            // Initialize the Simulation
            dataRep = new Grid(initialRep);

            // Place and rotate the camera
            camera.transform.position = new Vector3(simulationSize * 0.5f, simulationSize * 0.5f, simulationSize * 0.5f);
            camera.transform.rotation = Quaternion.Euler(90, 0, 0);

            // Adjust size to fit all squares
            camera.orthographicSize = ((float) simulationSize) / 2f;

            // Show Initial State
            UpdateVisualGrid(dataRep.getCurrentState());
        }

        void PrimeCells(ref List<List<GameObject>> cells, GameObject prefab, GameObject parent, float yPos)
        {
            // Initialize the cell list
            cells = new List<List<GameObject>>();

            for (int row = 0; row < simulationSize; row++)
            {
                List<GameObject> columnList = new List<GameObject>();
                for (int col = 0; col < simulationSize; col++)
                {
                    // Prime Empty Prefabs
                    GameObject cell = Instantiate(prefab, parent.transform);
                    cell.transform.position = new Vector3(row + 0.5f, yPos, col + 0.5f);
                    columnList.Add(cell);
                }
                cells.Add(columnList);
            }
        }

        float timer = 0f;
        void Update()
        {
            timer += Time.deltaTime;

            // Only update the simulation based on the step size
            if (timer >= simulationStep)
            {
                // Get the next state and show it
                List<List<bool>> currentSimState = dataRep.updateState();
                PrintGridDebug(currentSimState);
                UpdateVisualGrid(currentSimState);
                timer = 0f;
            }
        }

        void UpdateVisualGrid(List<List<bool>> currentSimState) {
            for (int i = 0; i < currentSimState.Count; i++) {
                for (int j = 0; j < currentSimState[i].Count; j++) {
                    // If the current element is true, hide the live cell plane
                    if (currentSimState[i][j]) {
                        deadCells[i][j].SetActive(false);
                    }
                    // If the current element is false, show the dead cell plane
                    else {
                        deadCells[i][j].SetActive(true);
                    }
                }
            }
        }


        void PrintGridDebug(List<List<bool>> currentSimState) {
            // Initialize the StringBuilder to concatenate the row strings
            StringBuilder gridStringBuilder = new StringBuilder();

            // Build a debug grid representation
            for (int i = 0; i < currentSimState.Count; i++) {
                StringBuilder rowStringBuilder = new StringBuilder();

                for (int j = 0; j < currentSimState[i].Count; j++) {
                    // If the current element is true, add '█' to the row StringBuilder
                    if (currentSimState[i][j]) {
                        rowStringBuilder.Append("█");
                    }
                    // If the current element is false, add '░' to the row StringBuilder
                    else {
                        rowStringBuilder.Append("░");
                    }
                }

                gridStringBuilder.AppendLine(rowStringBuilder.ToString());
            }

            // Log the Grid
            Debug.Log(gridStringBuilder.ToString());
        }

    }
}