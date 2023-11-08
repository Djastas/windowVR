using System.Collections;
using System.Linq;
using _main.scripts.ConnectSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _main.scripts
{
    public class TipsComponent : MonoBehaviour
    {
        [SerializeField] private int maxLoop;
        [SerializeField] private float onTime;
        [SerializeField] private float offTime;
        [SerializeField] private Material lightMat;

        private Coroutine _activeCoroutine;
        private Material _normalMat;
        
        [Button]
        public void Tips(string id)
        {
            var detail = GetDetail(id);
            var render = detail.visual.GetComponent<Renderer>();
            _normalMat = render.material;
            if (_activeCoroutine == null)
            {
                StartCoroutine(Flash(render));
            }
        }

        private IEnumerator Flash(Renderer render)
        {
            for (int i = 0; i < maxLoop; i++)
            {
                render.material = lightMat;
                yield return new WaitForSeconds(onTime);
                render.material = _normalMat;
                yield return new WaitForSeconds(offTime);
            }

            yield return null;
        }
        
        public Detail GetDetail(string id)
        {
            var i = FindObjectsOfType<Detail>();
            var trueDetail = i.FirstOrDefault(detail => detail.id == id);
            return trueDetail;
        }
        
    }
}
