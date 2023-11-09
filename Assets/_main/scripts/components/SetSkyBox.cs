using System;
using UnityEngine;

namespace _main.scripts.components
{
    public class SetSkyBox : MonoBehaviour
    {
        [SerializeField] private Material skybox;

        private void Start()
        {
            RenderSettings.skybox = skybox;
        }
    }
}