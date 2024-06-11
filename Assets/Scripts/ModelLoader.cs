using System;
using GLTFast;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    private GltfAsset m_Asset;
    private Transform m_Transform;
    private bool m_IsDone;

    void Start()
    {
        m_Asset = gameObject.AddComponent<GltfAsset>();
        m_Asset.StreamingAsset = true;
        m_Asset.Url = "DamagedHelmet.gltf";
    }

    private void Update()
    {
        if (!m_IsDone && m_Asset.IsDone)
        {
            m_IsDone = true;
            m_Transform = transform.GetChild(0).transform;
            m_Transform.localEulerAngles = new Vector3(90f, 180f, 0f);
        }
    }
}