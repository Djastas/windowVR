using System;
using System.Collections.Generic;
using UnityEngine;
namespace _main.scripts
{
    [CreateAssetMenu]
    public class InstructionList : ScriptableObject
    {
        public List<Instruction> Instructions;

    }
[Serializable]
    public class Instruction
    {
        public GameObject instructionPrefab;
        public string id;
    }
}