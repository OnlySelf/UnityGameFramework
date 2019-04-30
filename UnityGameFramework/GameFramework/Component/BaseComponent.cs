using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFramework.Component
{
    public class BaseComponent : MonoBehaviour
    {
        protected BaseComponentMediator componentMediator = null;

        protected virtual void Awake()
        {
            this.componentMediator = new BaseComponentMediator(this);
        }

        protected virtual void OnEnable()
        {
            UnityGameFrameworkFacade.Get().RegisterMediator(this);
        }

        protected virtual void OnDisenable()
        {
            UnityGameFrameworkFacade.Get().RemoveMediator(this.MediatorName);
        }

        protected virtual void OnDisable()
        {

        }

        protected virtual void OnDestroy()
        {
            
        }

        public virtual string[] ListNotificationInterests()
        {
            return new string[0];
        }

        protected virtual void SendNotification(string notificationName)
        {
            this.componentMediator.SendNotification(notificationName);
        }

        protected virtual void SendNotification(string notificationName, object body)
        {
            this.componentMediator.SendNotification(notificationName, body);
        }

        protected virtual void SendNotification(string notificationName, object body, string type)
        {
            this.componentMediator.SendNotification(notificationName, body, type);
        }

        public virtual void HandleNotification(INotification notification) { }
    }
}
