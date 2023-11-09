
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _main.scripts
{
    public class Socket : MonoBehaviour
    {
        [SerializeField] private List<GameObject> slots;
        private int _currentIndex;

        private void Start()
        {
            StartCoroutine(Wait());

        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.1f);
            UpdateSlots();
        }
        

        [Button]
        public void Next()
        {
            _currentIndex++;
            UpdateSlots();
        }

        private void UpdateSlots()
        {
            for (var index = 0; index < slots.Count; index++)
            {
                var slot = slots[index];
                slot.SetActive(index == _currentIndex);
            }
        }
    }
}
