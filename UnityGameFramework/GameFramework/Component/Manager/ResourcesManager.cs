using UnityEngine;
using System.Collections.Generic;
using UnityGameFramework.Component;

namespace UnityGameFramework
{
    public class ResourcesManager : BaseComponent
    {
        private static ResourcesManager _instance;

        protected override void Awake()
        {
            base.Awake();
            _resDic = new Dictionary<string, Object>();
        }

        public static ResourcesManager GetInstance()
        {
            return ResourcesManager._instance;
        }

        /// <summary>
        /// 界面预制体路径
        /// </summary>
        public string UILayerPrefabPath = "";
        /// <summary>
        /// 物品图标路径
        /// </summary>
        public string ItemIconPath = "";

        private Dictionary<string, Object> _resDic = null;

        public Object GetRes(string pathKey, System.Type type)
        {
            if (_resDic.ContainsKey(pathKey))
            {
                return _resDic[pathKey];
            }
            else
            {
                _resDic[pathKey] = Resources.Load(pathKey, type);
                return _resDic[pathKey];
            }
        }

        public Sprite GetItemIcon(int itemId)
        {
            Sprite itemIcon = this.GetRes(ItemIconPath + "/" + itemId, typeof(Sprite)) as Sprite;
            return itemIcon;
        }

        public GameObject GetUILayerPrefab(string layerName)
        {
            GameObject layerPrefab = this.GetRes(UILayerPrefabPath + "/" + layerName, typeof(GameObject)) as GameObject;
            return layerPrefab;
        }

        public void ReleaseUnusedRes()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}
