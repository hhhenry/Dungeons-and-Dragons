
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg
{
public sealed partial class Monster : Luban.BeanBase
{
    public Monster(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { if(!_buf["name"].IsString) { throw new SerializationException(); }  Name = _buf["name"]; }
        { if(!_buf["level"].IsNumber) { throw new SerializationException(); }  Level = _buf["level"]; }
        { if(!_buf["exp"].IsNumber) { throw new SerializationException(); }  Exp = _buf["exp"]; }
        { if(!_buf["hp"].IsNumber) { throw new SerializationException(); }  Hp = _buf["hp"]; }
        { if(!_buf["attack"].IsString) { throw new SerializationException(); }  Attack = _buf["attack"]; }
        { if(!_buf["ac"].IsNumber) { throw new SerializationException(); }  Ac = _buf["ac"]; }
        { if(!_buf["thac0"].IsNumber) { throw new SerializationException(); }  Thac0 = _buf["thac0"]; }
        { if(!_buf["icon"].IsString) { throw new SerializationException(); }  Icon = _buf["icon"]; }
        { if(!_buf["reward"].IsString) { throw new SerializationException(); }  Reward = _buf["reward"]; }
    }

    public static Monster DeserializeMonster(JSONNode _buf)
    {
        return new Monster(_buf);
    }

    /// <summary>
    /// 这是id
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 名字
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// 等级
    /// </summary>
    public readonly int Level;
    /// <summary>
    /// 经验
    /// </summary>
    public readonly int Exp;
    /// <summary>
    /// HP
    /// </summary>
    public readonly int Hp;
    /// <summary>
    /// 攻击力
    /// </summary>
    public readonly string Attack;
    /// <summary>
    /// 防御等级
    /// </summary>
    public readonly int Ac;
    /// <summary>
    /// 0级命中率
    /// </summary>
    public readonly int Thac0;
    /// <summary>
    /// 立绘
    /// </summary>
    public readonly string Icon;
    /// <summary>
    /// 对应装备表的id
    /// </summary>
    public readonly string Reward;
   
    public const int __ID__ = -1393696838;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "name:" + Name + ","
        + "level:" + Level + ","
        + "exp:" + Exp + ","
        + "hp:" + Hp + ","
        + "attack:" + Attack + ","
        + "ac:" + Ac + ","
        + "thac0:" + Thac0 + ","
        + "icon:" + Icon + ","
        + "reward:" + Reward + ","
        + "}";
    }
}

}
