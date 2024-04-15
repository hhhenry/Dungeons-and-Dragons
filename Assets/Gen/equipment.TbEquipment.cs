
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg.equipment
{
public partial class TbEquipment
{
    private readonly System.Collections.Generic.Dictionary<int, Equipment> _dataMap;
    private readonly System.Collections.Generic.List<Equipment> _dataList;
    
    public TbEquipment(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, Equipment>();
        _dataList = new System.Collections.Generic.List<Equipment>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            Equipment _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = Equipment.DeserializeEquipment(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, Equipment> DataMap => _dataMap;
    public System.Collections.Generic.List<Equipment> DataList => _dataList;

    public Equipment GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Equipment Get(int key) => _dataMap[key];
    public Equipment this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}
