using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using MelonLoader;
using UnityEngine;

namespace PPDuckSim_Unstuck_Mod
{
    public class Mod : MelonMod
    {
        public static Mod Instance { get { return Mod._instance; } }
        private static Mod _instance { get;  set; }

        private static KeyCode unstuck;

        private static GeneralManager generalManager = null;


        public override void OnEarlyInitializeMelon()
        {
            _instance = this;
            unstuck = KeyCode.U;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            LoggerInstance.Msg($"Scene {sceneName} with build index {buildIndex} has been loaded!");
        }

        public override void OnLateUpdate()
        {
            GeneralManager manager = GetGeneralManager();
            if (manager == null) return;
            if (Input.GetKeyDown(unstuck))
            {
                GameObject duck = manager.GetCurrentDuck();
                if (duck == null) return;
                duck.transform.position = manager.SpawnPoint.position;
            }
        }

        private GeneralManager GetGeneralManager()
        {
            if (Mod.generalManager != null) return Mod.generalManager;
            Type type = typeof(GeneralManager);
            FieldInfo field = type.GetField("generalManager", BindingFlags.NonPublic | BindingFlags.Static);
            GeneralManager generalManager = (GeneralManager)field.GetValue(null);
            Mod.generalManager = generalManager;
            return generalManager;
        }
    }
}
