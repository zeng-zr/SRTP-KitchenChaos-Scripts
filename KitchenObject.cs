using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)//当前kitchenobject已经有父级柜台
        {
            this.kitchenObjectParent.ClearKitchenObject();//清除父级柜台上的物品
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())//如果柜台上已经有物品
        {
            Debug.LogError("Trying to set clear counter to kitchen object that already has a kitchen object");
        }
        kitchenObjectParent.SetKitchenObject(this);//设置当前物品为父级柜台上的物品

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
}
