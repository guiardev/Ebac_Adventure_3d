using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ItemCollectableCoin : ItemCollectableBase{

    public Collider2D collider;

    protected override void Collect(){
        base.Collect();
        ItemManager.Instance.AddByType(ItemType.COIN);
        collider.enabled = false;
    }
}