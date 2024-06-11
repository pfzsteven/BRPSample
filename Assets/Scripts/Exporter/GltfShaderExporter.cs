using GLTFast;
using GLTFast.Export;
using GLTFast.Logging;
using UnityEngine;

namespace Scripts.Exporter
{
    public class GltfShaderExporter : MonoBehaviour
    {
        [SerializeField] string path;

        async void AdvancedExport()
        {
            // CollectingLogger lets you programmatically go through
            // errors and warnings the export raised
            var logger = new CollectingLogger();

            // ExportSettings and GameObjectExportSettings allow you to configure the export
            // Check their respective source for details

            // ExportSettings provides generic export settings
            var exportSettings = new ExportSettings
            {
                Format = GltfFormat.Binary,
                FileConflictResolution = FileConflictResolution.Overwrite,
                // Export everything except cameras or animation
                ComponentMask = ~(ComponentType.Camera | ComponentType.Animation),
                // Boost light intensities 
                LightIntensityFactor = 100f,
            };

            // GameObjectExportSettings provides settings specific to a GameObject/Component based hierarchy
            var gameObjectExportSettings = new GameObjectExportSettings
            {
                // Include inactive GameObjects in export
                OnlyActiveInHierarchy = false,
                // Also export disabled components
                DisabledComponents = true,
                // Only export GameObjects on certain layers
                LayerMask = LayerMask.GetMask("Default", "MyCustomLayer"),
            };

            // GameObjectExport lets you create glTFs from GameObject hierarchies
            var export = new GameObjectExport(exportSettings, gameObjectExportSettings, logger: logger);

            // Example of gathering GameObjects to be exported (recursively)
            var rootLevelNodes = GameObject.FindGameObjectsWithTag("ExportMe");

            // Add a scene
            export.AddScene(rootLevelNodes, "My new glTF scene");

            // Async glTF export
            var success = await export.SaveToFileAndDispose(path);

            if (!success)
            {
                Debug.LogError("Something went wrong exporting a glTF");
                // Log all exporter messages
                logger.LogAll();
            }
        }
    }
}