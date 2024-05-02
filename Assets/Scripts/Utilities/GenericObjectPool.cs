﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utilities
{
    public class GenericObjectPool<T> where T : class
    {
        protected List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        protected T GetItem()
        {
            if (pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(item => !item.IsInUse);
                if (item != null)
                {
                    item.IsInUse = true;
                    return item.Item;
                }
            }
            return CreateNewPooledItem();
        }

        protected virtual T CreateItem()
        {
            throw new NotImplementedException("CreateItem() method not implemented in derived class");
        }

        protected void ReturnItem(T item)
        {
            PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
            pooledItem.IsInUse = false;
        }

        private T CreateNewPooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>();
            newItem.Item = CreateItem();
            newItem.IsInUse = true;
            pooledItems.Add(newItem);
            return newItem.Item;
        }


        public class PooledItem<U>
        {
            public U Item;
            public bool IsInUse;
        }
    }
}
