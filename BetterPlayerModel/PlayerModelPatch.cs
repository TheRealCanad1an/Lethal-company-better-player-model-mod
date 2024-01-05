using UnityEngine;
using HarmonyLib;
using GameNetcodeStuff;
using System.Linq;

namespace BetterPlayerModel.Patches
{
    [HarmonyPatch]
    internal class PlayerObjects
    {
        //[HarmonyPatch(typeof(PlayerControllerB))]
        /*[HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void ReplaceModel()
        {
            PlayerControllerB[] players = GameObject.FindObjectsByType<PlayerControllerB>(FindObjectsSortMode.None);

            for (int i = 0; i < players.Length; i++)
            {
                SkinnedMeshRenderer playerMesh = players[i].thisPlayerModel.gameObject.GetComponent<SkinnedMeshRenderer>();

                if (playerMesh != null)// && thisPlayerModel.sharedMesh != BetterModelBase.playerModel.sharedMesh)
                {
                    List<Material> materials = new List<Material>(3);
                    for (int o = 0; o < materials.Count; o++)
                    {
                        materials[o] = playerMesh.material;
                        materials[o].mainTexture = BetterModelBase.textures[o];
                    }
                    SkinnedMeshRenderer myMesh = BetterModelBase.playerModel;

                    Dictionary<string, Transform> boneDictionary = new Dictionary<string, Transform>();
                    BetterModelBase.nls.LogInfo("rootBone: " + playerMesh.rootBone);
                    Transform[] rootBoneChildren = playerMesh.rootBone.parent.GetComponentsInChildren<Transform>();
                    BetterModelBase.nls.LogInfo("got rootBone");
                    foreach (Transform child in rootBoneChildren)
                    {
                        boneDictionary[child.name] = child;
                    }

                    Transform[] newBones = new Transform[myMesh.bones.Length];

                    for (int o = 0; o < newBones.Length; o++)
                    {
                        //BetterModelBase.nls.LogInfo("skinnedMeshRenderer.bones[o]: " + myMesh.bones[o]);
                        if (myMesh.bones[o] != null && boneDictionary.TryGetValue(myMesh.bones[o].name, out Transform newBone))
                        {
                            newBones[o] = newBone;
                        }
                    }

                    myMesh.bones = newBones;

                    myMesh.materials = materials.ToArray();

                    players[i].thisPlayerModel.materials = myMesh.materials;

                    BetterModelBase.nls.LogInfo("Changed Player Model: " + players[i]);
                }
            }
        }*/

        public static void InitModels()
        {
            PlayerControllerB localPlayerController = GameNetworkManager.Instance.localPlayerController;
            Debug.Log((object)((Object)((Component)localPlayerController).gameObject).name);
            PlayerControllerB[] array = Object.FindObjectsOfType<PlayerControllerB>();
            PlayerControllerB[] array2 = array;
            foreach (PlayerControllerB val in array2)
            {
                if (!((Object)(object)val == (Object)(object)localPlayerController))
                {
                    SkinnedMeshRenderer val2 = ((Component)val).gameObject.GetComponentsInChildren<SkinnedMeshRenderer>().ToList().Find((SkinnedMeshRenderer x) => ((Object)x).name.Contains("Body"));
                    if (!((Object)(object)val2 != (Object)null))
                    {
                        if (!((Component)val).gameObject.TryGetComponent<LethalCreature.CreatureController>(out LethalCreature.CreatureController ba))
                        {
                            ((Component)val).gameObject.AddComponent<LethalCreature.CreatureController>();
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlayerControllerB), "SpawnPlayerAnimation")]
        [HarmonyPostfix]
        public static void InitModel(ref PlayerControllerB __instance)
        {
            InitModels();
        }

        [HarmonyPatch(typeof(PlayerControllerB), "DisablePlayerModel")]
        [HarmonyPostfix]
        public static void DisablePlayerModel(ref PlayerControllerB __instance, GameObject playerObject, bool enable, bool disableLocalArms)
        {
            PlayerControllerB localPlayerController = GameNetworkManager.Instance.localPlayerController;
            if ((Object)(object)playerObject == (Object)(object)localPlayerController)
            {
                return;
            }
            playerObject.gameObject.GetComponentInChildren<LODGroup>().enabled = false;
            SkinnedMeshRenderer[] componentsInChildren = playerObject.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer currentMeshRenderer in componentsInChildren)
            {
                if (!(((Object)currentMeshRenderer).name == "Body"))
                {
                    ((Renderer)currentMeshRenderer).enabled = false;
                }

                if (__instance.TryGetComponent<LethalCreature.CreatureController>(out LethalCreature.CreatureController ba) && ba.myPlayerModel == currentMeshRenderer) currentMeshRenderer.enabled = enable;
            }
        }
    }
}