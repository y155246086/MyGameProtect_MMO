//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: light framework (CF/Silverlight) enabled
    
// Generated from: protobuf/role.proto
namespace com.kz.game.message.proto
{
  [global::ProtoBuf.ProtoContract(Name=@"RoleBaseDataPro")]
  public partial class RoleBaseDataPro : global::ProtoBuf.IExtensible
  {
    public RoleBaseDataPro() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private long _userId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"userId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long userId
    {
      get { return _userId; }
      set { _userId = value; }
    }
    private string _roleName;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"roleName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string roleName
    {
      get { return _roleName; }
      set { _roleName = value; }
    }
    private int _money;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"money", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int money
    {
      get { return _money; }
      set { _money = value; }
    }
    private int _rmb;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"rmb", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int rmb
    {
      get { return _rmb; }
      set { _rmb = value; }
    }
    private int _energy;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"energy", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int energy
    {
      get { return _energy; }
      set { _energy = value; }
    }
    private int _vipLevel;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"vipLevel", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int vipLevel
    {
      get { return _vipLevel; }
      set { _vipLevel = value; }
    }
    private int _areaId;
    [global::ProtoBuf.ProtoMember(8, IsRequired = true, Name=@"areaId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int areaId
    {
      get { return _areaId; }
      set { _areaId = value; }
    }
    private int _spriteId;
    [global::ProtoBuf.ProtoMember(9, IsRequired = true, Name=@"spriteId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int spriteId
    {
      get { return _spriteId; }
      set { _spriteId = value; }
    }
    private int _level;
    [global::ProtoBuf.ProtoMember(10, IsRequired = true, Name=@"level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int level
    {
      get { return _level; }
      set { _level = value; }
    }
    private int _exp;
    [global::ProtoBuf.ProtoMember(11, IsRequired = true, Name=@"exp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int exp
    {
      get { return _exp; }
      set { _exp = value; }
    }
    private long _lastLoginTime;
    [global::ProtoBuf.ProtoMember(12, IsRequired = true, Name=@"lastLoginTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long lastLoginTime
    {
      get { return _lastLoginTime; }
      set { _lastLoginTime = value; }
    }
    private long _lastLogoutTime;
    [global::ProtoBuf.ProtoMember(13, IsRequired = true, Name=@"lastLogoutTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long lastLogoutTime
    {
      get { return _lastLogoutTime; }
      set { _lastLogoutTime = value; }
    }
    private int _hairId;
    [global::ProtoBuf.ProtoMember(14, IsRequired = true, Name=@"hairId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int hairId
    {
      get { return _hairId; }
      set { _hairId = value; }
    }
    private int _gangcai;
    [global::ProtoBuf.ProtoMember(15, IsRequired = true, Name=@"gangcai", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int gangcai
    {
      get { return _gangcai; }
      set { _gangcai = value; }
    }
    private int _lvcai;
    [global::ProtoBuf.ProtoMember(16, IsRequired = true, Name=@"lvcai", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int lvcai
    {
      get { return _lvcai; }
      set { _lvcai = value; }
    }
    private int _danyao;
    [global::ProtoBuf.ProtoMember(17, IsRequired = true, Name=@"danyao", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int danyao
    {
      get { return _danyao; }
      set { _danyao = value; }
    }
    private int _ranliao;
    [global::ProtoBuf.ProtoMember(18, IsRequired = true, Name=@"ranliao", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int ranliao
    {
      get { return _ranliao; }
      set { _ranliao = value; }
    }
    private int _rank;
    [global::ProtoBuf.ProtoMember(19, IsRequired = true, Name=@"rank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int rank
    {
      get { return _rank; }
      set { _rank = value; }
    }
    private int _shipExpItemNum;
    [global::ProtoBuf.ProtoMember(20, IsRequired = true, Name=@"shipExpItemNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int shipExpItemNum
    {
      get { return _shipExpItemNum; }
      set { _shipExpItemNum = value; }
    }
    private int _jlCurrency1;
    [global::ProtoBuf.ProtoMember(21, IsRequired = true, Name=@"jlCurrency1", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int jlCurrency1
    {
      get { return _jlCurrency1; }
      set { _jlCurrency1 = value; }
    }
    private int _jlCurrency2;
    [global::ProtoBuf.ProtoMember(22, IsRequired = true, Name=@"jlCurrency2", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int jlCurrency2
    {
      get { return _jlCurrency2; }
      set { _jlCurrency2 = value; }
    }
    private int _jlCurrency3;
    [global::ProtoBuf.ProtoMember(23, IsRequired = true, Name=@"jlCurrency3", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int jlCurrency3
    {
      get { return _jlCurrency3; }
      set { _jlCurrency3 = value; }
    }
    private int _jlCurrency4;
    [global::ProtoBuf.ProtoMember(24, IsRequired = true, Name=@"jlCurrency4", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int jlCurrency4
    {
      get { return _jlCurrency4; }
      set { _jlCurrency4 = value; }
    }
    private int _shipSpeedUpItemNum;
    [global::ProtoBuf.ProtoMember(25, IsRequired = true, Name=@"shipSpeedUpItemNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int shipSpeedUpItemNum
    {
      get { return _shipSpeedUpItemNum; }
      set { _shipSpeedUpItemNum = value; }
    }
    private int _invitationBlueNum;
    [global::ProtoBuf.ProtoMember(26, IsRequired = true, Name=@"invitationBlueNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int invitationBlueNum
    {
      get { return _invitationBlueNum; }
      set { _invitationBlueNum = value; }
    }
    private int _invitationPurpleNum;
    [global::ProtoBuf.ProtoMember(27, IsRequired = true, Name=@"invitationPurpleNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int invitationPurpleNum
    {
      get { return _invitationPurpleNum; }
      set { _invitationPurpleNum = value; }
    }
    private int _arenaCurrency;
    [global::ProtoBuf.ProtoMember(28, IsRequired = true, Name=@"arenaCurrency", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int arenaCurrency
    {
      get { return _arenaCurrency; }
      set { _arenaCurrency = value; }
    }
    private int _pointX;
    [global::ProtoBuf.ProtoMember(29, IsRequired = true, Name=@"pointX", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int pointX
    {
      get { return _pointX; }
      set { _pointX = value; }
    }
    private int _pointY;
    [global::ProtoBuf.ProtoMember(30, IsRequired = true, Name=@"pointY", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int pointY
    {
      get { return _pointY; }
      set { _pointY = value; }
    }
    private int _pointZ;
    [global::ProtoBuf.ProtoMember(31, IsRequired = true, Name=@"pointZ", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int pointZ
    {
      get { return _pointZ; }
      set { _pointZ = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"CurrencyChangeLogPro")]
  public partial class CurrencyChangeLogPro : global::ProtoBuf.IExtensible
  {
    public CurrencyChangeLogPro() {}
    
    private int _currencyType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"currencyType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int currencyType
    {
      get { return _currencyType; }
      set { _currencyType = value; }
    }
    private int _addNum;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"addNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int addNum
    {
      get { return _addNum; }
      set { _addNum = value; }
    }
    private int _finalNum;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"finalNum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int finalNum
    {
      get { return _finalNum; }
      set { _finalNum = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"PointPro")]
  public partial class PointPro : global::ProtoBuf.IExtensible
  {
    public PointPro() {}
    
    private int _pointX;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"pointX", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int pointX
    {
      get { return _pointX; }
      set { _pointX = value; }
    }
    private int _pointY;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pointY", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int pointY
    {
      get { return _pointY; }
      set { _pointY = value; }
    }
    private int _pointZ;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"pointZ", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int pointZ
    {
      get { return _pointZ; }
      set { _pointZ = value; }
    }
    private int _towardX;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"towardX", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int towardX
    {
      get { return _towardX; }
      set { _towardX = value; }
    }
    private int _towardY;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"towardY", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int towardY
    {
      get { return _towardY; }
      set { _towardY = value; }
    }
    private int _towardZ;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"towardZ", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int towardZ
    {
      get { return _towardZ; }
      set { _towardZ = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"RoleMovePro")]
  public partial class RoleMovePro : global::ProtoBuf.IExtensible
  {
    public RoleMovePro() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private com.kz.game.message.proto.PointPro _pointData;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pointData", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public com.kz.game.message.proto.PointPro pointData
    {
      get { return _pointData; }
      set { _pointData = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"RoleInScenePro")]
  public partial class RoleInScenePro : global::ProtoBuf.IExtensible
  {
    public RoleInScenePro() {}
    
    private readonly global::System.Collections.Generic.List<com.kz.game.message.proto.RoleBaseDataPro> _datas = new global::System.Collections.Generic.List<com.kz.game.message.proto.RoleBaseDataPro>();
    [global::ProtoBuf.ProtoMember(1, Name=@"datas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<com.kz.game.message.proto.RoleBaseDataPro> datas
    {
      get { return _datas; }
    }
  
    private readonly global::System.Collections.Generic.List<com.kz.game.message.proto.NpcBaseDataPro> _npcDatas = new global::System.Collections.Generic.List<com.kz.game.message.proto.NpcBaseDataPro>();
    [global::ProtoBuf.ProtoMember(2, Name=@"npcDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<com.kz.game.message.proto.NpcBaseDataPro> npcDatas
    {
      get { return _npcDatas; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"NpcBaseDataPro")]
  public partial class NpcBaseDataPro : global::ProtoBuf.IExtensible
  {
    public NpcBaseDataPro() {}
    
    private int _npcId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"npcId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int npcId
    {
      get { return _npcId; }
      set { _npcId = value; }
    }
    private int _SpriteId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"SpriteId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int SpriteId
    {
      get { return _SpriteId; }
      set { _SpriteId = value; }
    }
    private com.kz.game.message.proto.PointPro _pointData;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"pointData", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public com.kz.game.message.proto.PointPro pointData
    {
      get { return _pointData; }
      set { _pointData = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"RoomListPro")]
  public partial class RoomListPro : global::ProtoBuf.IExtensible
  {
    public RoomListPro() {}
    
    private readonly global::System.Collections.Generic.List<com.kz.game.message.proto.RoomDataPro> _datas = new global::System.Collections.Generic.List<com.kz.game.message.proto.RoomDataPro>();
    [global::ProtoBuf.ProtoMember(1, Name=@"datas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<com.kz.game.message.proto.RoomDataPro> datas
    {
      get { return _datas; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"RoomDataPro")]
  public partial class RoomDataPro : global::ProtoBuf.IExtensible
  {
    public RoomDataPro() {}
    
    private int _roomId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roomId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int roomId
    {
      get { return _roomId; }
      set { _roomId = value; }
    }
    private string _roomName;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"roomName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string roomName
    {
      get { return _roomName; }
      set { _roomName = value; }
    }
    private int _curNumber;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"curNumber", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int curNumber
    {
      get { return _curNumber; }
      set { _curNumber = value; }
    }
    private int _maxNumber;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"maxNumber", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int maxNumber
    {
      get { return _maxNumber; }
      set { _maxNumber = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"RoomItemPoint")]
  public partial class RoomItemPoint : global::ProtoBuf.IExtensible
  {
    public RoomItemPoint() {}
    
    private int _point;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"point", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int point
    {
      get { return _point; }
      set { _point = value; }
    }
    private int _itemType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"itemType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemType
    {
      get { return _itemType; }
      set { _itemType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"RoomItemPointList")]
  public partial class RoomItemPointList : global::ProtoBuf.IExtensible
  {
    public RoomItemPointList() {}
    
    private readonly global::System.Collections.Generic.List<com.kz.game.message.proto.RoomItemPoint> _points = new global::System.Collections.Generic.List<com.kz.game.message.proto.RoomItemPoint>();
    [global::ProtoBuf.ProtoMember(1, Name=@"points", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<com.kz.game.message.proto.RoomItemPoint> points
    {
      get { return _points; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"RoomBattleResultPro")]
  public partial class RoomBattleResultPro : global::ProtoBuf.IExtensible
  {
    public RoomBattleResultPro() {}
    
    private readonly global::System.Collections.Generic.List<com.kz.game.message.proto.RoomKillDataPro> _datas = new global::System.Collections.Generic.List<com.kz.game.message.proto.RoomKillDataPro>();
    [global::ProtoBuf.ProtoMember(1, Name=@"datas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<com.kz.game.message.proto.RoomKillDataPro> datas
    {
      get { return _datas; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"RoomKillDataPro")]
  public partial class RoomKillDataPro : global::ProtoBuf.IExtensible
  {
    public RoomKillDataPro() {}
    
    private string _sourceName;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"sourceName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string sourceName
    {
      get { return _sourceName; }
      set { _sourceName = value; }
    }
    private string _targetName;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"targetName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string targetName
    {
      get { return _targetName; }
      set { _targetName = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"NpcMovePro")]
  public partial class NpcMovePro : global::ProtoBuf.IExtensible
  {
    public NpcMovePro() {}
    
    private int _npcId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"npcId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int npcId
    {
      get { return _npcId; }
      set { _npcId = value; }
    }
    private com.kz.game.message.proto.PointPro _pointData;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pointData", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public com.kz.game.message.proto.PointPro pointData
    {
      get { return _pointData; }
      set { _pointData = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"BattleResultPro")]
  public partial class BattleResultPro : global::ProtoBuf.IExtensible
  {
    public BattleResultPro() {}
    
    private long _sourceId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"sourceId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long sourceId
    {
      get { return _sourceId; }
      set { _sourceId = value; }
    }
    private long _targetId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"targetId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long targetId
    {
      get { return _targetId; }
      set { _targetId = value; }
    }
    private long _damage;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"damage", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long damage
    {
      get { return _damage; }
      set { _damage = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::ProtoBuf.ProtoContract(Name=@"TransferControlPro")]
  public partial class TransferControlPro : global::ProtoBuf.IExtensible
  {
    public TransferControlPro() {}
    
    private int _npcId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"npcId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int npcId
    {
      get { return _npcId; }
      set { _npcId = value; }
    }
    private int _state;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"state", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int state
    {
      get { return _state; }
      set { _state = value; }
    }
    private long _targetId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"targetId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long targetId
    {
      get { return _targetId; }
      set { _targetId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}