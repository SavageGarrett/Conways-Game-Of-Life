using System.Collections.Generic;
using System;
using System.Text.Json;
using UnityEngine;

namespace CarderGarrett.Lab6 
{
    public class JSONReader : MonoBehaviour 
    {

        public TextAsset textJSON;
        
        [Serializable]
        private class GridRawData
        {
            public int size;
            public List<bool> state;
        }

        private GridRawData rawData = new GridRawData();

        public List<List<bool>> getInitialDataRep() {
            rawData = JsonUtility.FromJson<GridRawData>(textJSON.text);

            List<List<bool>> initialData = new List<List<bool>>();
            for (int i = 0; i < rawData.size; i++) {
                List<bool> row = new List<bool>();
                for (int j = 0; j < rawData.size; j++) {
                    int index = i * rawData.size + j;
                    row.Add(rawData.state[index]);
                }
                initialData.Add(row);
            }

            // Translate the JSON data to Unity's coordinate system
            return RotateClockwise(initialData);
        }

        public static List<List<bool>> RotateClockwise(List<List<bool>> matrix)
        {
            int numRows = matrix.Count;
            int numCols = matrix[0].Count;
            List<List<bool>> result = new List<List<bool>>();

            for (int j = 0; j < numCols; j++)
            {
                List<bool> newRow = new List<bool>();
                for (int i = numRows - 1; i >= 0; i--)
                {
                    newRow.Add(matrix[i][j]);
                }
                result.Add(newRow);
            }
            return result;
        }




    }
}