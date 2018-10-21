using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maze : MonoBehaviour
{
    [Range(0f, 1f)]
    public float archProb;

    [Range(0f, 1f)]
    public float fakeProb;

    public IntVector2 size;
    public MazeCell cellPrefab;
    public float generationStepDelay; 

    public MazePassage passagePrefab;
    public MazeArch archPrefab;

    public MazeWall wallPrefab;
    public MazeFakeWall fakePrefab;

    public MazeObjective objectivePrefab;
    public NavMeshSurface surface;

    private int step;
    private int objectiveCount = 0;
    private MazeCell[,] cells;

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public void Generate()
    {
        //WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];

        //generate cells
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            //yield return delay;
            DoNextGenerationStep(activeCells);
        }

        //Surface
        surface.BuildNavMesh();
    }

    public void DoFirstGenerationStep(List<MazeCell> activecells)
    {
        activecells.Add(CreateCell(RandomCoordinates));
    }

    public void DoNextGenerationStep(List<MazeCell> activecells)
    {
        int currentIndex = activecells.Count - 1;
        MazeCell currentCell = activecells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activecells.RemoveAt(currentIndex);
            return;
        }

        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if(ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activecells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        //Create objective every once in a while
        if (step % 80 == 0)
        {
            MazeObjective objective = Instantiate(objectivePrefab);
            Vector3 size = objective.transform.localScale;
            objective.transform.parent = cell.transform;
            objective.transform.localPosition = new Vector3(0, 0.5f, 0);
            objective.transform.localScale = size;
            objective.name = "Objective";
            objectiveCount++;
        }

        //Create passages either in the form of archways or passages
        MazePassage prefab = Random.value < archProb ? archPrefab : passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;
        Vector3 scale = passage.transform.localScale; //All passages have same scale
        passage.Initialize(cell, otherCell, direction);
        passage.transform.localScale = scale;

        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        passage.transform.localScale = scale;
        
        step++;
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall prefab = Random.value < fakeProb ? fakePrefab : wallPrefab;

        MazeWall wall = Instantiate(prefab) as MazeWall;
        Vector3 scale = wall.transform.localScale; //All walls have same scale
        wall.Initialize(cell, otherCell, direction);
        wall.transform.localScale = scale;

        if (otherCell != null)
        {
            wall = Instantiate(prefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
            wall.transform.localScale = scale;

        }
    }

    public MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        Vector3 scale = newCell.transform.localScale;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localScale = scale;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    public int GetObjectiveCount()
    {
        return objectiveCount;
    }
}
