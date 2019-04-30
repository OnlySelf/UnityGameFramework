using PureMVC.Patterns;
using System.Collections.Generic;
using PureMVC.Patterns.Mediator;
using PureMVC.Interfaces;

namespace UnityGameFramework
{
    public class BaseUILayerMediator : Mediator
    {
        public BaseUILayerMediator(BaseUILayer layer) : base(layer.LayerName + "Mediator" + layer.GetHashCode(), layer){ }

        public override string[] ListNotificationInterests()
        {
            return ((BaseUILayer)this.ViewComponent).ListNotificationInterests();
        }

        public override void HandleNotification(INotification notification)
        {
            ((BaseUILayer)this.ViewComponent).HandleNotification(notification);
        }
    }
}
