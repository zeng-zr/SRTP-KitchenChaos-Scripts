using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    // Start is called before the first frame update

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {//BUG：玩家无法拿起物品
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab);//在场景中创建新的对象,并返回该对象的引用.original参数是要实例化的对象，parent参数是新对象的父级
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
      

    }

}
