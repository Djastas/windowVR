using _main.scripts.managers;
using TMPro;
using UnityEngine;

namespace _main.scripts
{
  public class RateMeter : MonoBehaviour
  {
    [SerializeField] private TMP_Text rate;
    [SerializeField] private TMP_Text speed;
    [SerializeField] private int detailCount;
    public void Rate()
    {
      var connect = 0;
      var disconnect = 0;

      foreach (var connectData in SessionManager.Instance._connectsData)
      {
        switch (connectData.type)
        {
          case "con":
            connect++;
            break;
          case "des":
            disconnect--;
            break;
        }
      }

      rate.text =(10 - (connect - disconnect - detailCount)).ToString();
      
      var time = SessionManager.Instance._connectsData[SessionManager.Instance._connectsData.Count].Time;
      speed.text = $"час:{time.Hour}мин:{time.Minutes} сек:{time.Seconds}";
    }
  
  }
}
