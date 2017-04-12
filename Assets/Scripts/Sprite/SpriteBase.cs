using UnityEngine;
using System.Collections;
/// <summary>
/// 精灵基类
/// </summary>
public class SpriteBase : MonoBehaviour {

    protected SfxManager sfxManager;
    public SfxHandler sfxHandler;
    public PropertyManager propertyManager;
    protected SpriteType spriteType = SpriteType.NONE;
    
    /// <summary>
    /// 获取精灵类型
    /// </summary>
    public SpriteType SpriteType
    {
        get
        {
            return spriteType;
        }
    }
    void Start()
    {
        Initialize();
        propertyManager = new PropertyManager();
        sfxManager = new SfxManager(this);
        sfxHandler = this.gameObject.AddComponent<SfxHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }
    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
    protected virtual void Initialize()
    {
    }

    protected virtual void FSMUpdate()
    {
    }
    protected virtual void FSMFixedUpdate()
    {
    }
    /// <summary>
    /// 播放特效
    /// </summary>
    public void PlaySfx(int id)
    {
        if (sfxManager == null)
        {
            return;
        }
        sfxManager.PlaySfx(id);
    }
}
