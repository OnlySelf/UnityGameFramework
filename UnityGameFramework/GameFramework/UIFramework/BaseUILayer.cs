using UnityEngine;
using System.Collections.Generic;
using PureMVC.Interfaces;
using UnityGameFramework.Utility;

namespace UnityGameFramework
{
    [RequireComponent(typeof(AutoScalePushLayer))]
    public abstract class BaseUILayer : MonoBehaviour
    {
        protected BaseUILayerMediator layerMediator = null;

        public UIIndex z_Index = UIIndex.UILayer1;

        private string _layerName = "";

        public string LayerName
        {
            get
            {
                return _layerName;
            }
            set
            {
                this._layerName = value;
            }
        }

        private object _customdata = null;

        public object Customdata
        {
            get
            {
                return _customdata;
            }
            set
            {
                this._customdata = value;
            }
        }

        public bool isRemoveOther = false;
        public bool isActionPushLayer = false;

        protected virtual void Awake()
        {
            this.layerMediator = new BaseUILayerMediator(this);
        }

        protected virtual void OnEnable()
        {
            UnityGameFrameworkFacade.Get().RegisterMediator(this.layerMediator);
            if (isActionPushLayer)
                this.PushLayerAction();
        }

        public virtual void OnAddToStage() { }

        public virtual void OnRemoveToStage() { }

        protected virtual void OnDisable()
        {
            UnityGameFrameworkFacade.Get().RemoveMediator(this.layerMediator.MediatorName);
            if (isActionPushLayer)
                this.RemoveLayerAction();
        }

        protected virtual void OnDestroy()
        {
            UIManager.GetInstance().RemoveLayer(this.LayerName);
        }

        public virtual void PushLayerAction() { }

        public virtual void RemoveLayerAction() { }

        public virtual string[] ListNotificationInterests()
        {
            return new string[0];
        }

        protected void SendNotification(string eventName)
        {
            this.layerMediator.SendNotification(eventName);
        }

        protected void SendNotification(string eventName, object body)
        {
            this.layerMediator.SendNotification(eventName, body);
        }

        protected void SendNotification(string eventName, object body, string type)
        {
            this.layerMediator.SendNotification(eventName, body, type);
        }

        public virtual void HandleNotification(INotification notification) { }
    }
}