using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFileLocal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(@"D:\Unity\Test Project\Test\Assets\thick.loli");
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("FormalBodyThickCloseLoli");
        Instantiate(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
