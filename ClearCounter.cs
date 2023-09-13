using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClearCounter : MonoBehaviour,IKitchenObjectParent
{
  
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    [SerializeField] private IKitchenObjectParent secondClearCounter;
    [SerializeField] private bool testing;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }

    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);//在场景中创建新的对象,并返回该对象的引用.original参数是要实例化的对象，parent参数是新对象的父级
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
