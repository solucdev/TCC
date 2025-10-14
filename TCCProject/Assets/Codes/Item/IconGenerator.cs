#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IconGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Item Icons")]
    public static void GenerateIcons()
    {
        // Local onde os ícones serão salvos
        string folderPath = "Assets/Images/ItemIcons/";

        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        // Pega todos os prefabs de itens (ajuste o caminho conforme necessário)
        string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { "Assets/Prefabs/" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            // Gera a textura de preview
            Texture2D texture = AssetPreview.GetAssetPreview(prefab);

            // Espera até que a preview seja gerada
            while (AssetPreview.IsLoadingAssetPreview(prefab.GetInstanceID()))
            {
                System.Threading.Thread.Sleep(50);
            }

            if (texture != null)
            {
                // Cria um novo sprite
                string spritePath = folderPath + prefab.name + ".png";
                byte[] bytes = texture.EncodeToPNG();
                System.IO.File.WriteAllBytes(spritePath, bytes);

                // Atualiza o banco de dados de assets
                AssetDatabase.ImportAsset(spritePath);

                // Configura as import settings para Sprite
                TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(spritePath);
                if (importer != null)
                {
                    importer.textureType = TextureImporterType.Sprite;
                    importer.SaveAndReimport();
                }
            }
        }

        AssetDatabase.Refresh();
    }
}
#endif