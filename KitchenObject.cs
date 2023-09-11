using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(this.clearCounter!=null)//当前kitchenobject已经有父级柜台
        {
            this.clearCounter.ClearKitchenObject();//清除父级柜台上的物品
        }
        this.clearCounter = clearCounter;
        if(clearCounter.HasKitchenObject())//如果柜台上已经有物品
        {
            Debug.LogError("Trying to set clear counter to kitchen object that already has a kitchen object");
        }
        clearCounter.SetKitchenObject(this);//设置当前物品为父级柜台上的物品

        transform.parent=clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
