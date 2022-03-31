using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BundledObjectLoader : MonoBehaviour
{
    public string bundleName = "testbundle";
    public string assetName = "Formal_Body_Thick_Close_Glamor";
    
    // Start is called before the first frame update
    void Start()
    {
        string bundlePath =
            @"D:\0_Projects\V_cafe_maid\Experiments\FirebaseStorageIO\Assets\AssetBundles\testbundle";
        
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(bundlePath);
        GameObject asset = localAssetBundle.LoadAsset<GameObject>(assetName);
        Instantiate(asset);
    }
}
        
