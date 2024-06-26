
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
public sealed partial class Equipment : Luban.BeanBase
{
    public Equipment(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { if(!_buf["name"].IsString) { throw new SerializationException(); }  Name = _buf["name"]; }
        { if(!_buf["description"].IsString) { throw new SerializationException(); }  Description = _buf["description"]; }
        { if(!_buf["type"].IsNumber) { throw new SerializationException(); }  Type = _buf["type"]; }
        { if(!_buf["value"].IsNumber) { throw new SerializationException(); }  Value = _buf["value"]; }
        { if(!_buf["icon"].IsString) { throw new SerializationException(); }  Icon = _buf["icon"]; }
    }

    public static Equipment DeserializeEquipment(JSONNode _buf)
    {
        return new Equipment(_buf);
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
    /// 描述
    /// </summary>
    public readonly string Description;
    /// <summary>
    /// 类型
    /// </summary>
    public readonly int Type;
    /// <summary>
    /// 效果值
    /// </summary>
    public readonly int Value;
    /// <summary>
    /// 立绘
    /// </summary>
    public readonly string Icon;
   
    public const int __ID__ = -1214642834;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "name:" + Name + ","
        + "description:" + Description + ","
        + "type:" + Type + ","
        + "value:" + Value + ","
        + "icon:" + Icon + ","
        + "}";
    }
}

}
