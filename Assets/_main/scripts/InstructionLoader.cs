using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _main.scripts
{
    public class InstructionLoader : MonoBehaviour
    {
        [SerializeField] private InstructionList instructionList;
        private BookController _bookController;
        private void Start()
        {
            _bookController = GetComponent<BookController>();
            Instruction i = null;

            foreach (var instruction in instructionList.Instructions.Where(instruction => instruction.id == DataTransfer.InstructionIndex))
            {
                i = instruction;
            }

            var tmp = Instantiate(i.instructionPrefab, gameObject.transform);
            
            List<GameObject> children = new();

            foreach (Transform child in tmp.transform)
            {
                Debug.Log(child.name);
                children.Add(child.gameObject);
            }

            _bookController.pages = children;
        }
    }
}