using System.Collections;
using System.Collections.Generic;

namespace CarderGarrett.Lab6 {
    public class Grid
    {

        public List<List<bool>> currentState;
        private List<List<bool>> nextState;
        int size;

        public Grid(List<List<bool>> initialState)
        {
            this.size = initialState.Count;
            currentState = initialState;
            nextState = WipeGrid();
        }

        // Wipe the Grid to be Completely Empty
        List<List<bool>> WipeGrid()
        {
            List<List<bool>> grid = new List<List<bool>>();

            for (int row = 0; row < size; row++)
            {
                List<bool> columnList = new List<bool>();
                for (int col = 0; col < size; col++)
                {
                    columnList.Add(false);
                }
                grid.Add(columnList);
            }

            return grid;
        }
        
        // Return Current State
        public List<List<bool>> getCurrentState()
        {
            return currentState;
        }

        // Update the State to Conway's Game of Life
        public List<List<bool>> updateState()
        {
            // Wipe the Grid
            nextState = WipeGrid();

            // Iterate over each cell
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // Get the number of neighbors and determine whether the cell lives or dies
                    int liveNeighbors = getNumberOfNeighbors(row, col);

                    nextState[row][col] = getNextState(currentState[row][col], liveNeighbors);
                }
            }

            // Set the state active and return
            currentState = nextState;
            return currentState;
        }


        bool getNextState(bool currentState, int numNeighbors)
        {
            if (currentState)
            {
                // Any live cell with fewer than two live neighbors dies, as if by underpopulation.
                // Any live cell with more than three live neighbors dies, as if by overpopulation.
                if (numNeighbors < 2 || numNeighbors > 3)
                {
                    return false;
                }
                // Any live cell with two or three live neighbors lives on to the next generation.
                else
                {
                    return true;
                }
            }
            else
            {
                // Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                if (numNeighbors == 3)
                {
                    return true;
                }
            }

            // Leave in Current State if no State is Determined
            return currentState;
        }

        int getNumberOfNeighbors(int row, int col)
        {
            int liveNeighbors = 0;

            // Iterate over the neighboring cells
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    // Skip the current cell
                    if (i == row && j == col)
                    {
                        continue;
                    }

                    // Check if the neighboring cell is within the bounds of the grid
                    if (i >= 0 && i < size && j >= 0 && j < size)
                    {
                        // If the neighboring cell is alive, increment the live neighbor count
                        if (currentState[i][j])
                        {
                            liveNeighbors++;
                        }
                    }
                }
            }

            return liveNeighbors;
        }

    }
}
