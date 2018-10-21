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
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
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
