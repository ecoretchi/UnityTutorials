using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Controls
{
    /// <summary>
    /// If you use TabViewerToggle, you do not need TabViewer, it`s dublicating functionality
    /// </summary>
    public class TabViewer : ToggleGroup
    {
        public RectTransform contentView;

        // Start is called before the first frame update
        protected override void Start()
        {
            if (m_Toggles.Count == 0)
                return;

            var toggleParent = m_Toggles[0].transform.parent;
            var toggles = toggleParent.GetComponentsInChildren<Toggle>();

            var pages = contentView.GetComponentsInChildren<TabViewerPage>(true);
  
            Debug.LogFormat("m_Toggles = {0} , pages = {1}, toggles = {2}", m_Toggles.Count, pages.Length, toggles.Length);

            List<Toggle> togglesInGroup = new List<Toggle>();
            foreach (var toggle in toggles)
            {
                if (m_Toggles.Contains(toggle))
                    togglesInGroup.Add(toggle);
            }

            for(int i=0;i<pages.Length;++i)
            {
                var page = pages[i];
                page.gameObject.SetActive(false);

                if (i >= togglesInGroup.Count)
                    break;

                var toggle = togglesInGroup[i];
      
                toggle.onValueChanged.AddListener((bool v) =>
                {
                    Debug.LogFormat("Page {0} is Toggled: {1}", i, v);
                    page.gameObject.SetActive(v);
                });

                if (toggle.isOn)
                    page.gameObject.SetActive(true);
                  
            }
        }
    }

}