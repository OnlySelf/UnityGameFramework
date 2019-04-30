using PureMVC.Patterns;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityGameFramework.Utility;
using UnityGameFramework.Component;

namespace UnityGameFramework
{
    public class UIManager : BaseComponent
    {
        private static UIManager _instance = null;

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
            this.Initialize();
        }

        public static UIManager GetInstance()
        {
            return UIManager._instance;
        }

        public GameObject canvas = null;
        public GameObject[] blackImgs = null;
        public int width = 1366;
        public int height = 768;
        public AdaptationType adaptationType = AdaptationType.NON;
        public bool isUseBlack = false;

        private Dictionary<string, BaseUILayer> _layerDic;
        private Dictionary<UIIndex, GameObject> _UIIndexDic;

        /// <summary>
        /// 缩放比例
        /// </summary>
        private float _adaptationRatio = 0;
        public float AdaptationRatio { get { return _adaptationRatio; } }

        public void Initialize()
        {
            _layerDic = new Dictionary<string, BaseUILayer>();
            _UIIndexDic = new Dictionary<UIIndex, GameObject>();
            AdaptationBlackImage();
        }

        private BaseUILayer GetLayer(string layerName)
        {
            BaseUILayer UILayer = null;
            if (this._layerDic.ContainsKey(layerName))
            {
                UILayer = _layerDic[layerName];
            }
            else
            {
                GameObject gobj = GameObject.Instantiate(ResourcesManager.GetInstance().GetUILayerPrefab(layerName));
                UILayer = gobj.GetComponent<BaseUILayer>();
                UILayer.LayerName = layerName;
                this._layerDic[layerName] = UILayer;
            }

            if (!_UIIndexDic.ContainsKey(UILayer.z_Index))
            {
                _UIIndexDic[UILayer.z_Index] = new GameObject(UILayer.z_Index.ToString(), typeof(RectTransform));
                _UIIndexDic[UILayer.z_Index].transform.SetParent(canvas.transform, false);
            }

            if (UILayer.isRemoveOther)
            {
                for (int i = 0; i < _UIIndexDic[UILayer.z_Index].transform.childCount; i++)
                {
                    this.RemoveLayer(_UIIndexDic[UILayer.z_Index].transform.GetChild(i).name.Replace("(Clone)", ""));
                }
            }

            UILayer.transform.SetParent(_UIIndexDic[UILayer.z_Index].transform, false);
            UILayer.transform.SetAsLastSibling();
            return UILayer;
        }

        public void PushLayer(string layerName)
        {
            BaseUILayer UILayer = this.GetLayer(layerName);
            UILayer.gameObject.SetActive(true);
            UILayer.OnAddToStage();
        }

        public void PushLayer(string layerName, object customdata)
        {
            BaseUILayer UILayer = this.GetLayer(layerName);
            UILayer.Customdata = customdata;
            UILayer.gameObject.SetActive(true);
            UILayer.OnAddToStage();
        }

        public void PushLayer(string layerName, UnityAction call)
        {
            this.PushLayer(layerName);
            call();
        }

        public void PushLayer(string layerName, object customdata, UnityAction call)
        {
            this.PushLayer(layerName, customdata);
            call();
        }

        public void PushLayer<T>(string layerName, UnityAction<T[]> call, params T[] arg0s)
        {
            this.PushLayer(layerName);
            call(arg0s);
        }

        public void PushLayer<T>(string layerName, object customdata, UnityAction<T[]> call, params T[] arg0s)
        {
            this.PushLayer(layerName, customdata);
            call(arg0s);
        }

        public void RemoveLayer(string layerName)
        {
            if (this._layerDic.ContainsKey(layerName))
            {
                this._layerDic[layerName].OnRemoveToStage();
                this._layerDic[layerName].gameObject.SetActive(false);
                this._layerDic[layerName].transform.SetAsFirstSibling();
                this._layerDic[layerName].Customdata = null;
            }
        }

        public void OnLayerDestroy(string layerName)
        {
            if (this._layerDic.ContainsKey(layerName))
            {
                this._layerDic.Remove(layerName);
            }
        }

        private void AdaptationBlackImage()
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize * (width * 1.0f / height * 1.0f / (Screen.width * 1.0f / Screen.height * 1.0f));
            switch (this.adaptationType)
            {
                case AdaptationType.HEIGHT:
                    //IPhoneX 黑边
                    if (isUseBlack && height < width)
                    {
                        float scalingWidth = width * Screen.height / height;
                        float blackWidth = (Screen.width - scalingWidth) / 2;
                        blackImgs = new GameObject[2];
                        GameObject blackImg = Resources.Load<GameObject>("Prefabs/UI/Adaptation/blackImage");
                        if (blackImg != null)
                        {
                            blackImgs[0] = GameObject.Instantiate(blackImg, canvas.transform, false) as GameObject;
                            blackImgs[1] = GameObject.Instantiate(blackImg, canvas.transform, false) as GameObject;
                            RectTransform rect0 = (RectTransform)blackImgs[0].transform;
                            RectTransform rect1 = (RectTransform)blackImgs[1].transform;
                            rect0.sizeDelta = new Vector2(blackWidth, Screen.height);
                            rect1.sizeDelta = new Vector2(blackWidth, Screen.height);
                            rect0.localPosition = new Vector3(scalingWidth / 2 + blackWidth / 2, 0, 0);
                            rect1.localPosition = new Vector3(-(scalingWidth / 2 + blackWidth / 2), 0, 0);
                        }
                    }
                    break;
                case AdaptationType.WIDTH:
                    //IPhoneX 黑边
                    if (isUseBlack && height > width)
                    {
                        float scalingHeight = height * Screen.width / width;
                        float blackHeight = (Screen.height - scalingHeight) / 2;
                        blackImgs = new GameObject[2];
                        GameObject blackImg = Resources.Load<GameObject>("Prefabs/UI/Adaptation/blackImage");
                        if (blackImg != null)
                        {
                            blackImgs[0] = GameObject.Instantiate(blackImg, canvas.transform, false) as GameObject;
                            blackImgs[1] = GameObject.Instantiate(blackImg, canvas.transform, false) as GameObject;
                            RectTransform rect0 = (RectTransform)blackImgs[0].transform;
                            RectTransform rect1 = (RectTransform)blackImgs[1].transform;
                            rect0.sizeDelta = new Vector2(Screen.width, blackHeight);
                            rect1.sizeDelta = new Vector2(Screen.width, blackHeight);
                            rect0.localPosition = new Vector3(0, scalingHeight / 2 + blackHeight / 2, 0);
                            rect1.localPosition = new Vector3(0, -(scalingHeight / 2 + blackHeight / 2), 0);
                        }
                    }
                    break;
            }

        }
    }
}

