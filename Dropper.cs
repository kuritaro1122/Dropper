using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drop {
    [System.Serializable]
    public class Dropper {
        [SerializeField] ItemBox[] itemBoxes;
        [SerializeField] bool reset = true;
        public Dropper(params ItemBox[] itemBoxes) {
            this.itemBoxes = new ItemBox[itemBoxes.Length];
            for (int i = 0; i < itemBoxes.Length; i++) this.itemBoxes[i] = itemBoxes[i];
        }
        public Dropper(params (GameObject, int)[] itemBoxes) {
            this.itemBoxes = new ItemBox[itemBoxes.Length];
            for (int i = 0; i < itemBoxes.Length; i++) {
                (GameObject item, int num) = itemBoxes[i];
                this.itemBoxes[i] = new ItemBox(item, num);
            }
        }
        public Dropper(bool reset = true, params ItemBox[] itemBoxes) : this(itemBoxes) {
            this.reset = reset;
        }
        public Dropper(bool reset = true, params (GameObject, int)[] itemBoxes) : this(itemBoxes) {
            this.reset = reset;
        }
        public GameObject GetDropItem() {
            int total = 0;
            foreach (var box in itemBoxes) total += box.RemainNum;
            int index = Random.Range(0, total);
            int total2 = 0;
            foreach (var box in itemBoxes) {
                if (index < box.RemainNum + total2) return box.GetItem();
                total2 += box.RemainNum;
            }
            if (reset) {
                foreach (var box in itemBoxes) box.Reset();
                return GetDropItem();

            } else return null;
        }
        [System.Serializable]
        public class ItemBox {
            [SerializeField] GameObject item;
            [SerializeField] int num;
            private int remainNum;
            public int RemainNum { get { return this.remainNum; } }
            public ItemBox(GameObject item, int num) {
                this.item = item;
                this.remainNum = this.num = num;
            }
            public GameObject GetItem() {
                if (this.remainNum > 0) {
                    this.remainNum--;
                    return this.item;
                } else return null;
            }
            public void Reset() {
                this.remainNum = num;
            }
        }
    }

    [System.Serializable]
    public class Dropper<T> where T : class {
        [SerializeField] ItemBox<T>[] itemBoxes;
        [SerializeField] bool reset = true;
        public Dropper(params ItemBox<T>[] itemBoxes) {
            this.itemBoxes = new ItemBox<T>[itemBoxes.Length];
            for (int i = 0; i < itemBoxes.Length; i++) this.itemBoxes[i] = itemBoxes[i];
        }
        public Dropper(params (T, int)[] itemBoxes) {
            this.itemBoxes = new ItemBox<T>[itemBoxes.Length];
            for (int i = 0; i < itemBoxes.Length; i++) {
                (T item, int num) = itemBoxes[i];
                this.itemBoxes[i] = new ItemBox<T>(item, num);
            }
        }
        public Dropper(bool reset = true, params ItemBox<T>[] itemBoxes) : this(itemBoxes) {
            this.reset = reset;
        }
        public Dropper(bool reset = true, params (T, int)[] itemBoxes) : this(itemBoxes) {
            this.reset = reset;
        }
        public T GetDropItem() {
            int total = 0;
            foreach (var box in itemBoxes) total += box.RemainNum;
            int index = Random.Range(0, total);
            int total2 = 0;
            foreach (var box in itemBoxes) {
                if (index < box.RemainNum + total2) return box.GetItem();
                total2 += box.RemainNum;
            }
            if (reset) {
                foreach (var box in itemBoxes) box.Reset();
                return GetDropItem();

            } else return null;
        }
        [System.Serializable]
        public class ItemBox<T> where T : class {
            [SerializeField] T item;
            [SerializeField] int num;
            private int remainNum;
            public int RemainNum { get { return this.remainNum; } }
            public ItemBox(T item, int num) {
                this.item = item;
                this.remainNum = this.num = num;
            }
            public T GetItem() {
                if (this.remainNum > 0) {
                    this.remainNum--;
                    return this.item;
                } else return null;
            }
            public void Reset() {
                this.remainNum = num;
            }
        }
    }
}
