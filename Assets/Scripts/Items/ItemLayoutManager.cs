using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens{
    
    public class ItemLayoutManager : MonoBehaviour{

        public List<ItemLayout> itemLayouts;
        public ItemLayout prefabLayout;
        public Transform container;

        private void Start() {
            CreateItens();
        }

        private void CreateItens(){

            foreach (var setup in ItemManager.Instance.itemSetups){

                //criando item para da setup
                var item = Instantiate(prefabLayout, container);
                item.Load(setup);
                itemLayouts.Add(item); // add um referencia do item
            }
        }
    }
}