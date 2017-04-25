using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleFramework.Data;

public class TestAddEquid : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DataCenter.Instance();
        Addobj(104001);
        //Addobj(104002);
        //Addobj(104003);
        //Addobj(104004);
	}
    private void Addobj(int id)
    {
        Debug.LogError("ID:" + id);
        EquipData equipData = EquipData.GetByID(id);

        EquipObjectData data = new EquipObjectData();
        data.goList = new List<GameObject>();
        GameObject meshObj = null;
        if(id != 104001)
        {
            data.mat = Resources.Load<Material>("Characters/Avatar/104/Materials/" + equipData.material);

            meshObj = Resources.Load<GameObject>("Characters/Avatar/104/suit/" + equipData.mesh);
        }
        else
        {
            data.mat = Resources.Load<Material>(equipData.material);

            meshObj = Resources.Load<GameObject>(equipData.mesh);
        }
        

        data.smr = meshObj.GetComponentInChildren<SkinnedMeshRenderer>();
        m_equipGoDic.Add(id, data);


        AddEquidMethod2(equipData);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //身上已穿装备<部位类型，装备id>
    private Dictionary<int, int> m_equipOnDic = new Dictionary<int, int>();
    ////裸体时的“装备”（外形显示）
    private List<int> m_nakedEquipList = new List<int>();
    private Dictionary<int, EquipObjectData> m_equipGoDic = new Dictionary<int, EquipObjectData>();
    //已装备中的用挂载方法装备的装备
    private List<GameObject> m_equipList = new List<GameObject>();
    //已装备中的Mesh或Material
    private List<Object> m_equipMeshOrMaterialList = new List<Object>();
    private List<SkinnedMeshRenderer> m_smrList = new List<SkinnedMeshRenderer>();
    private void AddEquidMethod2(EquipData equipData)
    {
        if (!m_equipGoDic.ContainsKey(equipData.id))
        {
            return;
        }
        Material material = m_equipGoDic[equipData.id].mat;
        //GameObject instance = m_equipGoDic[equipData.id].goList[0];
        if (transform == null) return;
        Transform equipPart = GameCommonUtils.GetChild(transform, equipData.slot[0]);

        if (equipPart == null)
        {
            return;
        }

        SkinnedMeshRenderer smr = equipPart.GetComponent<SkinnedMeshRenderer>();
        if (!smr)//安全检查
            return;
        m_smrList.Add(smr);
        m_equipMeshOrMaterialList.Add(material);
        smr.sharedMaterial = material;
        smr.castShadows = false;
        smr.receiveShadows = false;
        smr.useLightProbes = true;
        SkinnedMeshRenderer smrTemp = m_equipGoDic[equipData.id].smr;

        //if (equipData.type.Count > 1) ClearOriginalModel();

        smr.sharedMesh = smrTemp.sharedMesh;
        //CombineInstance ci = new CombineInstance();
        //ci.mesh = smrTemp.sharedMesh;
        //m_combineInstances.Add(ci);

        List<Transform> bones = new List<Transform>();
        for (int i = 0; i < smrTemp.bones.Length; i++)
        {
            bones.Add(GameCommonUtils.GetChild(this.transform, smrTemp.bones[i].name));
        }
        //m_bones.AddRange(bones);
        smr.bones = bones.ToArray();
        //m_equipMeshOrMaterialList.Add(instance);


    }
}
class EquipObjectData
{
    public int type;//1挂载，2mesh
    public List<GameObject> goList;
    public Material mat;
    public SkinnedMeshRenderer smr;
}
