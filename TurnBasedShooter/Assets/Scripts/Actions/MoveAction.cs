using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int moveDistance = 4;

    private Vector3 targetPosition;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }
        float stoppingDistanceTolerance = 0.1f;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if (Vector3.Distance(targetPosition, transform.position) >= stoppingDistanceTolerance)
        {

            float moveSpeed = 4f;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
            isActive = false;
            onActionComplete();
        }
        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        this.isActive = true;
        this.onActionComplete = onActionComplete;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -moveDistance; x <= moveDistance; x++)
        {
            for (int z = -moveDistance; z <= moveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    // Same grid position where the unit is already at
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid position is occupied with another unit
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
