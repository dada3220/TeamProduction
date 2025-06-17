using UnityEditor;

public static class BuildApp
{
    [MenuItem("Build/BuildApp")]
    public static void Build()
    {
        //windows64のプラットフォームでアプリをビルドする
        BuildPipeline.BuildPlayer(
            new string[] {
                 "Assets/MyAssets/Scenes/Title.unity",
                "Assets/MyAssets/Scenes/GameScene.unity",
                "Assets/MyAssets/Scenes/GameOverScene.unity",
                 "Assets/MyAssets/Scenes/Clear.unity"
            },
            "Builds/App/SampleApp.exe",
            BuildTarget.StandaloneWindows64,
            BuildOptions.None
        );
    }
}
