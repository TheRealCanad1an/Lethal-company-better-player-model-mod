using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace BetterPlayerModel
{
    [BepInPlugin(modeGUID, modName, modVersion)]
    public class BetterModelBase : BaseUnityPlugin
    {
        const string modeGUID = "TheGooberator.BetterPlayer";
        const string modName = "Better Player Model";
        const string modVersion = "1.0.0.0";

        readonly Harmony harmony = new Harmony(modeGUID);

        static BetterModelBase Instance;

        internal static ManualLogSource nls;

        public static GameObject playerModel;
        public static RuntimeAnimatorController animationController;
        //public static GameObject playerSkellaton;

        public static List<Texture2D> textures = new List<Texture2D>();

        void Awake()
        {
            if (Instance == null) Instance = this;

            nls = BepInEx.Logging.Logger.CreateLogSource(modeGUID);

            nls.LogInfo("BetterPlayerModel Loaded");

            Assets.PopulateAssets();

            nls.LogInfo("Populated Assets");

            //Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("mbundle");
            //AssetBundle val = AssetBundle.LoadFromStream(stream);
            playerModel = Assets.MainAssetBundle.LoadAsset<GameObject>("Assets/Scavenger.fbx");
            animationController = Assets.MainAssetBundle.LoadAsset<RuntimeAnimatorController>("Assets/Scavenger.controller");
            //playerSkellaton = Assets.MainAssetBundle.LoadAsset<GameObject>("Assets/Armature.prefab");
            textures.Add(Assets.MainAssetBundle.LoadAsset<Texture2D>("Assets/Gear_Base_Color.png"));
            textures.Add(Assets.MainAssetBundle.LoadAsset<Texture2D>("Assets/Clothing_Base_Color.png"));
            textures.Add(Assets.MainAssetBundle.LoadAsset<Texture2D>("Assets/Helmet_Base_Color.png"));
            textures.Add(Assets.MainAssetBundle.LoadAsset<Texture2D>("Assets/Gear_Normal_OpenGL.png"));
            textures.Add(Assets.MainAssetBundle.LoadAsset<Texture2D>("Assets/Clothing_Normal_OpenGL.png"));
            textures.Add(Assets.MainAssetBundle.LoadAsset<Texture2D>("Assets/Helmet_Normal_OpenGL.png"));

            nls.LogInfo("Grabed Asset: " + playerModel);
            //ModelReplacementAPI.RegisterSuitModelReplacement("Orange suit", typeof(PlayerModelPatch));

            harmony.PatchAll();
        }

        public static class Assets
        {
            public static string mainAssetBundleName = "newmesh";

            public static AssetBundle MainAssetBundle = null;

            private static string GetAssemblyName()
            {
                return Assembly.GetExecutingAssembly().FullName.Split(',')[0];
            }

            public static void PopulateAssets()
            {
                if ((UnityEngine.Object)(object)MainAssetBundle == (UnityEngine.Object)null)
                {
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetAssemblyName() + "." + mainAssetBundleName))
                    {
                        MainAssetBundle = AssetBundle.LoadFromStream(stream);
                    }
                }
            }
        }
    }
}