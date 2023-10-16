using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BookController : MonoBehaviour
{
    [SerializeField] private List<GameObject> pages;
    
    private int _pageIndex;
    private int _pageMax;

    private void Start()
    {
        _pageMax = pages.Count;
        UpdateUI();
    }

    [Button]
    public void Next()
    {
        if (_pageIndex >= _pageMax - 1) return;
        _pageIndex++;
        UpdateUI();
    }

    [Button]
    public void Back()
    {
        if (_pageIndex <= 0) return;
        _pageIndex--;
        UpdateUI();
    }
    private void UpdateUI()
    {
        for (var index = 0; index < pages.Count; index++)
        {
            var page = pages[index];
            page.SetActive(index == _pageIndex);
        }
    }
}
