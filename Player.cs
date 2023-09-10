using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private ClearCounter selectedCounter;
    private bool isWalking;
    private Vector3 lastInteractDir;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {

        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();

    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        // transform.position += moveDir * Time.deltaTime * moveSpeed;
        //设计合适的旋转速度

        float rotateSpeed = 15f;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        isWalking = (inputVector != Vector2.zero);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            // 当不能向moveDir方向移动时

            // 尝试沿x轴移动
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized; // 归一化让速度和直接左右移动相同
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // 可以沿x轴移动
                moveDir = moveDirX;
            }
            else
            {
                // 不能向x轴方向移动，尝试向z轴方向移动
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized; // 归一化让速度和直接左右移动相同
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // 可以向z轴方向移动
                    moveDir = moveDirZ;
                }
                else
                {
                    // 不能朝任何方向移动
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;
        }
    }
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }

            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else // 没有射线击中任何东西
        {
            SetSelectedCounter(null);
        }


    }

    public bool Is_Walking()
    {
        return isWalking;
    }
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        //将原先HandleInteractions()中的内容暂时放到了这里，并且暂时不再调用HandleInteractions()中的clearCounter.Interact()
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
            //Debug.Log("")
        }
    }
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}