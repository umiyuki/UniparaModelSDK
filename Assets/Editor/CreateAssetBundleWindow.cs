using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundleWindow : ScriptableWizard
{
    [SerializeField] GameObject[] targets;
    [SerializeField] BuildTarget[] buildtargetplatforms = new BuildTarget[1] { BuildTarget.iOS };
    [SerializeField] bool encrypt = true;

    [MenuItem("Custom/CreateAssetBundle")]
    static void Open()
    {
        DisplayWizard<CreateAssetBundleWindow>("Create Asset Bundle");       
    }

    //Createボタンが押された
    private void OnWizardCreate()
    {
        string[] modelNames = new string[targets.Length];
        string[] modelOwners = new string[targets.Length];
        string[] modelVers = new string[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            //ModelDescriptionを引っ張る
            var modelDescription = targets[i].GetComponent<ModelDescription>();
            modelNames[i] = "";
            modelOwners[i] = "";
            modelVers[i] = "";
            if (modelDescription != null)
            {
                modelNames[i] = modelDescription.modelName;
                modelOwners[i] = modelDescription.modelOwner;
                modelVers[i] = modelDescription.modelVersion;
            }
        }

        CreateAssetBundle.Create(targets, buildtargetplatforms, encrypt, modelNames, modelOwners, modelVers);
    }
}
