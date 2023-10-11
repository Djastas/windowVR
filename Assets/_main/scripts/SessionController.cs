using System.Collections.Generic;
using _main.scripts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown mode;
    [SerializeField] private TMP_Dropdown map;

    [SerializeField] private string tutorialSceneName;
    [SerializeField] private string freeSceneName;
    [SerializeField] private string controlSceneName;

    [SerializeField] private MapsData mapsData;

    private void Start()
    {
        map.options = new List<TMP_Dropdown.OptionData>();

        foreach (var mapData in mapsData.mapsData)
        {
            var tmp = new TMP_Dropdown.OptionData(mapData.name,mapData.preview);
            map.options.Add(tmp);
        }
    }
[Button]
    public void Load()
    {
        var i = mode.value switch
        {
            1 => tutorialSceneName,
            2 => freeSceneName,
            3 => controlSceneName,
            _ => ""
        };
        SceneManager.LoadScene(i);
        SceneManager.LoadScene(mapsData.mapsData[map.value].id,LoadSceneMode.Additive);
    }
}
