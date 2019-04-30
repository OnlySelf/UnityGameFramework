using UnityEngine;
using UnityEngine.UI;

namespace UnityGameFramework.Utility
{
    public class AutoScalePushLayer : MonoBehaviour
    {
        public Image[] backImgs = null;

        private RectTransform _rectTrasnform = null;
        private RectTransform rootNode = null;

        protected void Awake()
        {
            this._rectTrasnform = this.GetComponent<RectTransform>();
            this.rootNode = (RectTransform)this.transform.Find("rootNode");
            this.SetAdaptationModeWithAspectRatio();
        }

        public void SetAdaptationModeWithAspectRatio()
        {
            for (int i = 0; i < backImgs.Length; i++)
            {
                RectTransform rectTrans = this.backImgs[i].GetComponent<RectTransform>();
                rectTrans.sizeDelta = new Vector2(Screen.width, rectTrans.sizeDelta.y * (Screen.width / rectTrans.sizeDelta.x));
            }
            if (rootNode != null)
                rootNode.localScale *= UIManager.GetInstance().AdaptationRatio;
            switch (UIManager.GetInstance().adaptationType)
            {
                case AdaptationType.WIDTH:
                    {
                        if (!UIManager.GetInstance().isUseBlack)
                            rootNode.sizeDelta = new Vector2(rootNode.sizeDelta.x, Screen.height / UIManager.GetInstance().AdaptationRatio);
                    }
                    break;
                case AdaptationType.HEIGHT:
                    {
                        if (!UIManager.GetInstance().isUseBlack)
                            rootNode.sizeDelta = new Vector2(Screen.width / UIManager.GetInstance().AdaptationRatio, rootNode.sizeDelta.y);
                    }
                    break;
            }
        }
    }
}
