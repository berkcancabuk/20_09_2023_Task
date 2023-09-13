using UnityEngine;

namespace View
{
    public class ScrollControl : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        private int _indexDown=1;
        private int _indexUp;

        /// <summary>
        /// Infinite scroll view for 3 objects.
        /// OnValueChange is also used in ScrollRect. 
        /// </summary>
        public void InfiniyScrollView()
        {
            if (content.anchoredPosition.y > (600*_indexDown))
            {
                content.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(content.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x
                        ,content.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y - (600*3));
                content.transform.GetChild(0).gameObject.transform.SetAsLastSibling();
                _indexDown++;
                _indexUp--;
            }
            else if (content.anchoredPosition.y < -(600*_indexUp)-20)
            {
                content.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(content.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.x
                        ,content.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.y + (600*3));
                content.transform.GetChild(2).gameObject.transform.SetAsFirstSibling();
                _indexUp++;
                _indexDown--;
            }
        }
    }
}