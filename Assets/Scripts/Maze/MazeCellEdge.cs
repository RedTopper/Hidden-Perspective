using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour
{

    private MazeCell cell, otherCell;
    private MazeDirection direction;

    public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;

        float small = 0.001f;
        Vector3 scale = transform.localScale;
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition =  new Vector3(Random.Range(-small, small), Random.Range(-small, small), Random.Range(-small, small));
        transform.localRotation = direction.ToRotation();
        transform.localScale = scale;
    }

    public MazeCell GetCell()
    {
        return cell;
    }

    public MazeCell GetOtherCell()
    {
        return otherCell;
    }

    public MazeDirection GetDirection()
    {
        return direction;
    }
}
