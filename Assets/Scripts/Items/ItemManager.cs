using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using UnityEngine;
using TMPro;


namespace Itens{

    public enum ItemType{
        COIN, LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>{

        public List<ItemSetup> itemSetups;

        private void Start(){
            Reset();
        }

        private void Reset(){
            //coins.value = 0;
            //UpdateUI();

            foreach (var i in itemSetups){
                i.soInt.value = 0;
            }
        }

        public ItemSetup GetItemByType(ItemType itemType){

            return itemSetups.Find(i => i.itemType == itemType);
        }

        public void AddByType(ItemType itemType, int amount = 1){

            if(amount < 0) return; // se numero for menor 0 ele para aqui

            itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = 1){        

            var item = itemSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            //checagem para verificar se valor esta menor que 0 para resetar valor
            if(item.soInt.value < 0) item.soInt.value = 0;
        }

        [NaughtyAttributes.Button]
        private void AddCoin(){
            AddByType(ItemType.COIN);
        }

        [NaughtyAttributes.Button]
        private void AddLifePack    (){
            AddByType(ItemType.LIFE_PACK);
        }

        /*
        private void UpdateUI(){ // usamos SOUIIntUpdate para atualizar UI coins
          // uiTextCoins.text = coins.ToString(); // método clássico para atualizar UI coins
          //UIInGameManager.Instance.UpdateTextCoins(coins.ToString());
          //UIInGameManager.UpdateTextCoins(coins.value.ToString());
        }
        */
    }

    [System.Serializable]
    public class ItemSetup{

        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;

    }
}