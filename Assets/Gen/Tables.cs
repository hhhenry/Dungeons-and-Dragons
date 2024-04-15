
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
public partial class Tables
{
    public monster.TbMonster TbMonster {get; }
    public player.TbPlayer TbPlayer {get; }
    public language.TbLanguage TbLanguage {get; }
    public equipment.TbEquipment TbEquipment {get; }

    public Tables(System.Func<string, JSONNode> loader)
    {
        TbMonster = new monster.TbMonster(loader("monster_tbmonster"));
        TbPlayer = new player.TbPlayer(loader("player_tbplayer"));
        TbLanguage = new language.TbLanguage(loader("language_tblanguage"));
        TbEquipment = new equipment.TbEquipment(loader("equipment_tbequipment"));
        ResolveRef();
    }
    
    private void ResolveRef()
    {
        TbMonster.ResolveRef(this);
        TbPlayer.ResolveRef(this);
        TbLanguage.ResolveRef(this);
        TbEquipment.ResolveRef(this);
    }
}

}
