using System.Collections.Generic;
using _main.scripts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown mode;
    [SerializeField] private TMP_Dropdown model;
    [SerializeField] private TMP_Dropdown map;

    [SerializeField] private string tutorialSceneName;
    [SerializeField] private string freeSceneName;
    [SerializeField] private string controlSceneName;

    [SerializeField] private InstructionList instructionList;

    [SerializeField] private MapsData mapsData;
    [SerializeField] private AsyncLoad asyncLoad;

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
        DataTransfer.InstructionIndex = instructionList.Instructions[model.value].id;
        var i = mode.value switch
        {
            0 => tutorialSceneName,
            1 => freeSceneName,
            2 =>controlSceneName ,
            _ => ""
        };
        asyncLoad.AsyncLoading(i,mapsData.mapsData[map.value].id);
        // SceneManager.LoadScene(mapsData.mapsData[map.value].id,LoadSceneMode.Additive);
    }
}
