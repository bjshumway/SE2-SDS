using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollbarEnabler : MonoBehaviour
{
    [SerializeField]
    RectTransform container;
    [SerializeField]
    RectTransform content;
    [SerializeField]
    Scrollbar scrollbar;

    bool enableScrollbar = false;

    void Update()
    {

    }

    //Updates the content so that adding items gives us a scrollbar if the size is big enough.
    void UpdateContentSizeAndScrollbar(int numItems)
    {
        if ( /*place code here to do gameObj Size * height + <distance between go's>*/ 1 < container.rect.height)
        {
            content.gameObject.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            content.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(512, 386);
            scrollbar.gameObject.SetActive(false);
        }
        else
        {
            content.gameObject.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            scrollbar.gameObject.SetActive(true);
        }
    }
}