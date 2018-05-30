using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelExpression : MonoBehaviour {

    [System.Serializable]
    public class MorphParam
    {
        public int index;
        public float param;
    }

    [System.Serializable]
    public class ListMorphParam
    {
        public string name;
        public List<MorphParam> list;
    }

    public SkinnedMeshRenderer targetSkinnedMeshRenderer;

    [SerializeField] public List<ListMorphParam> listMorphParam;

    public void SetMorph(int index)
    {
        Mesh mesh = targetSkinnedMeshRenderer.sharedMesh;
        int blendShapeNum = mesh.blendShapeCount;

        for (int i = 0; i < blendShapeNum; i++)
        {
            targetSkinnedMeshRenderer.SetBlendShapeWeight(i, 0f);
        }

        if (index != -1)
        {
            for (int i = 0; i < listMorphParam[index].list.Count; i++)
            {
                targetSkinnedMeshRenderer.SetBlendShapeWeight(listMorphParam[index].list[i].index, listMorphParam[index].list[i].param);
            }
        }
    }

    [ContextMenu("AddNowMorphParam")]
    void AddNowMorphParam()
    {
        ListMorphParam morphParam = new ListMorphParam();
        morphParam.list = new List<MorphParam>();

        Mesh mesh = targetSkinnedMeshRenderer.sharedMesh;
        int blendShapeNum = mesh.blendShapeCount;

        for (int i = 0; i < blendShapeNum; i++)
        {
            var weight = targetSkinnedMeshRenderer.GetBlendShapeWeight(i);
            if (weight != 0)
            {
                MorphParam param = new MorphParam();
                param.index = i;
                param.param = weight;
                morphParam.list.Add(param);
            }
            
        }

        listMorphParam.Add(morphParam);
    }

    [ContextMenu("ClearModelMorph")]
    void ClearModelMorph()
    {
        Mesh mesh = targetSkinnedMeshRenderer.sharedMesh;
        int blendShapeNum = mesh.blendShapeCount;

        for (int i = 0; i < blendShapeNum; i++)
        {
            targetSkinnedMeshRenderer.SetBlendShapeWeight(i, 0f);
        }
    }
}
