using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Patterns.Mediator;

namespace UnityGameFramework.Component
{
    public class BaseComponentMediator : Mediator
    {
        public BaseComponentMediator(BaseComponent component) : base(component.name + "Mediator" + component.GetHashCode(), component)
        {

        }

        public override void OnRegister()
        {
            
        }

        public override void OnRemove()
        {
            
        }

        public override string[] ListNotificationInterests()
        {
            return ((BaseComponent)this.ViewComponent).ListNotificationInterests();
        }

        public override void HandleNotification(INotification notification)
        {
            ((BaseComponent)this.ViewComponent).HandleNotification(notification);
        }
    }

}
