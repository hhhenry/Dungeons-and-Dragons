
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg.language
{
public partial class TbLanguage
{
    private readonly System.Collections.Generic.Dictionary<string, Languase> _dataMap;
    private readonly System.Collections.Generic.List<Languase> _dataList;
    
    public TbLanguage(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<string, Languase>();
        _dataList = new System.Collections.Generic.List<Languase>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            Languase _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = Languase.DeserializeLanguase(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<string, Languase> DataMap => _dataMap;
    public System.Collections.Generic.List<Languase> DataList => _dataList;

    public Languase GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Languase Get(string key) => _dataMap[key];
    public Languase this[string key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}