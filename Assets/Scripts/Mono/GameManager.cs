using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using cfg;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    public Text playerStatusText;
    public Text monsterStatusText;
    public Text playerLogText;
    public Text monsterLogText;
    public Image dragonIcon;
    public Image heroIcon;
    public Button attackButton;
    public Button equipButton;
    public GameObject gameOverPanel;
    public GameObject splash;
    public GameObject equipmentPanel;
    public Button closeButton;
    public Button closeEquip;
    public Text resultText;
    public Player player;
    public Monster monster;
    public Equipment equipment;
    public Log playerLog;
    public Log monsterLog;
    public ParticleSystem sword;
    public AudioSource slash;
    public Save save;
    public cfg.Tables tables;
    public int language_setting;
    public Language language;
    public Button language_toggle;
    public Image armor;
    public Image helmet;
    public Image glove;
    public Image ring;
    public Image necklace;
    public Image shoes;
    public Image belt;
    public Image weapon;
    int playerHPTemp = 0;
    int playerACBonus = 0;
    bool immuneCriticalHit = false;
    int playerThac0Bonus = 0;
    int playerCriticalRoll = 0;
    int playerAttackBonus = 0;
    int playerAttackPerRound = 1;
    int playerResistance = 0;

    private void Start()
    {
        HideGameOverPanel();

        tables = new cfg.Tables(LoadByteBuf);
        save = new();
        save.LoadByJSON();

        // 初始化
        language = new Language();
        if (!PlayerPrefs.HasKey("language")) {
            language_setting = language.GetLanguage();
            PlayerPrefs.SetInt("language",language_setting);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[language_setting];
        }
        else {
            language_setting = PlayerPrefs.GetInt("language");
            if(language_setting == 0) {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                PlayerPrefs.SetInt("language",0);
                Debug.LogWarning("setting " + language_setting);
            }
            else {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                PlayerPrefs.SetInt("language",1);
                Debug.LogWarning("setting " + language_setting);
            }
        }
        
        EquipmentInit();

        player = new Player
        {
            Name = tables.TbPlayer.Get(save.playerLevel).Name,
            Level = tables.TbPlayer.Get(save.playerLevel).Level,
            Experience = tables.TbPlayer.Get(save.playerLevel).Exp,
            Health = tables.TbPlayer.Get(save.playerLevel).Hp + playerHPTemp,
            Attack = tables.TbPlayer.Get(save.playerLevel).Attack,
            ArmorClass = tables.TbPlayer.Get(save.playerLevel).Ac,
            Thac0 = tables.TbPlayer.Get(save.playerLevel).Thac0,
            equippedEquipment = new(),
        };
        
        heroIcon.sprite = Resources.Load<Sprite>(tables.TbPlayer.Get(save.playerLevel).Icon);

        monster = new Monster
        {
            Name = tables.TbMonster.Get(save.level).Name,
            Health = tables.TbMonster.Get(save.level).Hp,
            Attack = tables.TbMonster.Get(save.level).Attack,
            ArmorClass = tables.TbMonster.Get(save.level).Ac,
            Thac0 = tables.TbMonster.Get(save.level).Thac0,
        };

        dragonIcon.sprite = Resources.Load<Sprite>(tables.TbMonster.Get(save.level).Icon);

        playerLog = new Log
        {
            Text = ""
        };

        monsterLog = new Log
        {
            Text = ""
        };

        // 设置按钮点击事件
        attackButton?.onClick.AddListener(OnAttackButtonClick);
        equipButton?.onClick.AddListener(OnEquipButtonClick);
        language_toggle?.onClick.AddListener(SelectLanguage);
        // 更新界面状态
        UpdateUI();
    }

    public void EquipmentInit() {
        foreach(int equipmentId in save.equippedEquipment) {
            switch(tables.TbEquipment.Get(equipmentId).Type) {
                case 1: //armor
                    if(playerACBonus < tables.TbEquipment.Get(equipmentId).Value) {
                        playerACBonus = tables.TbEquipment.Get(equipmentId).Value;
                    }
                    break;
                case 2: //helmet
                    immuneCriticalHit = true;
                    break;
                case 3: //glove
                    if(playerThac0Bonus < tables.TbEquipment.Get(equipmentId).Value) {
                        playerThac0Bonus = tables.TbEquipment.Get(equipmentId).Value;
                    }
                    break;
                case 4: //ring
                    playerCriticalRoll = tables.TbEquipment.Get(equipmentId).Value;
                    break;
                case 5: //necklace
                    if(playerHPTemp < tables.TbEquipment.Get(equipmentId).Value) {
                        playerHPTemp = tables.TbEquipment.Get(equipmentId).Value;
                    }
                    break;
                case 6: //shoes
                    if(playerAttackPerRound < tables.TbEquipment.Get(equipmentId).Value) {
                        playerAttackPerRound = tables.TbEquipment.Get(equipmentId).Value;
                    }
                    break;
                case 7: //belt
                    if(playerResistance < tables.TbEquipment.Get(equipmentId).Value) {
                        playerResistance = tables.TbEquipment.Get(equipmentId).Value;
                    }
                    break;
                case 8: //weapon
                    if(playerAttackBonus < tables.TbEquipment.Get(equipmentId).Value) {
                        playerAttackBonus = tables.TbEquipment.Get(equipmentId).Value;
                    }
                    break;
                default: 
                    break;
            }
        }
    }

    public void OnEquipButtonClick() {
        foreach(int equipmentId in save.equippedEquipment) {
            switch(tables.TbEquipment.Get(equipmentId).Type) {
                case 1: //armor
                    armor.sprite = Resources.Load<Sprite>(tables.TbEquipment.Get(equipmentId).Icon);
                    break;
                case 2: //helmet
                    helmet.sprite = Resources.Load<Sprite>(tables.TbEquipment.Get(equipmentId).Icon);
                    break;
                case 3: //glove
                    glove.sprite = Resources.Load<Sprite>(tables.TbEquipment.Get(equipmentId).Icon);
                    break;
                case 4: //ring
                    ring.sprite = Resources.Load<Sprite>(tables.TbEquipment.Get(equipmentId).Icon);
                    break;
                case 5: //necklace
                    necklace.sprite = Resources.Load<Sprite>(tables.TbEquipment.Get(equipmentId).Icon);
                    break;
                case 6: //shoes
                    shoes.sprite = Resources.Load<Sprite>(tables.TbEquipment.Get(equipmentId).Icon);
                    break;
                case 7: //belt
                    belt.sprite = Resources.Load<Sprite>(tables.TbEquipment.Get(equipmentId).Icon);
                    break;
                case 8: //weapon
                    weapon.sprite = Resources.Load<Sprite>(tables.TbEquipment.Get(equipmentId).Icon);
                    break;
                default:
                    break;
            }
        }
        equipmentPanel.SetActive(true);
        closeEquip?.onClick.AddListener(OnCloseEquipClick);
    }

    public void OnCloseEquipClick() {
        equipmentPanel.SetActive(false);
    }

    public void SelectLanguage(){
         if(language_setting == 0) {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
            language_setting = 1;
            PlayerPrefs.SetInt("language",1);
        }
        else {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            language_setting = 0;
            PlayerPrefs.SetInt("language",0);
        }
        UpdateUI();
    }

    public string GetWord(string index, int language_setting) {
        return language_setting switch
        {
            0 => tables.TbLanguage.Get(index).En,
            1 => tables.TbLanguage.Get(index).Cn,
            _ => tables.TbLanguage.Get(index).En,
        };
    }

    private void NextLevel()
    {

        player.Name = tables.TbPlayer.Get(save.playerLevel).Name;
        player.Level = tables.TbPlayer.Get(save.playerLevel).Level;
        player.Experience = tables.TbPlayer.Get(save.playerLevel).Exp;
        player.Health = tables.TbPlayer.Get(save.playerLevel).Hp + playerHPTemp;
        player.Attack = tables.TbPlayer.Get(save.playerLevel).Attack;
        player.ArmorClass = tables.TbPlayer.Get(save.playerLevel).Ac;
        player.Thac0 = tables.TbPlayer.Get(save.playerLevel).Thac0;
      
        heroIcon.sprite = Resources.Load<Sprite>(tables.TbPlayer.Get(save.playerLevel).Icon);

        monster.Name = tables.TbMonster.Get(save.level).Name;
        monster.Health = tables.TbMonster.Get(save.level).Hp;
        monster.Attack = tables.TbMonster.Get(save.level).Attack;
        monster.ArmorClass = tables.TbMonster.Get(save.level).Ac;
        monster.Thac0 = tables.TbMonster.Get(save.level).Thac0;

        dragonIcon.sprite = Resources.Load<Sprite>(tables.TbMonster.Get(save.level).Icon);

        UpdateUI();
    }

    private static JSONNode LoadByteBuf(string file)
    {
        return JSON.Parse(Resources.Load("GenerateDatas/json/" + file).ToString());
    }
    private void OnAttackButtonClick()
    {
        int playerAttackRoll = player.RollAttack();
        for(int i = 0; i < playerAttackPerRound; i++) {
            
            // 玩家攻击怪物逻辑
            if (playerAttackRoll >= 20 + playerCriticalRoll)
            {
                int damage = player.RollDamage() + player.RollDamage() + playerAttackBonus * 2; // 根据武器和角色属性计算伤害
                monster.Health -= damage;
                player.GainExperience(10); 
                player.LevelUp();
                save.playerLevel = player.Level;
                save.SaveByJSON(save);
                playerLog.Text = $"{GetWord("攻击检定为", language_setting)} {playerAttackRoll} : {GetWord("致命一击 伤害翻倍", language_setting)}\n" + 
                                 $"{GetWord("造成", language_setting)} {damage + playerAttackBonus} {GetWord("点伤害", language_setting)}";
            }
            else if (playerAttackRoll == 1)
            {
                playerLog.Text = $"{GetWord("攻击检定为", language_setting)} {playerAttackRoll} : {GetWord("严重失误 无法命中", language_setting)}";
            }
            else if (playerAttackRoll + player.Thac0 + playerThac0Bonus >= monster.ArmorClass)
            {
                int damage = player.RollDamage(); // 根据武器和角色属性计算伤害
                monster.Health -= damage + playerAttackBonus;
                player.GainExperience(10); 
                player.LevelUp();
                save.playerLevel = player.Level;
                save.SaveByJSON(save);
                playerLog.Text = $"{GetWord("攻击检定为", language_setting)} {playerAttackRoll} + {player.Thac0} + {playerThac0Bonus} = {playerAttackRoll + player.Thac0 + playerThac0Bonus} : {GetWord("命中", language_setting)}\n" + 
                                 $"{GetWord("造成", language_setting)} {damage} + {playerAttackBonus} = {damage + playerAttackBonus} {GetWord("点伤害", language_setting)}";

            }        
            else
            {
                playerLog.Text = $"{GetWord("攻击检定为", language_setting)} {playerAttackRoll} + {player.Thac0} + {playerThac0Bonus} = {playerAttackRoll + player.Thac0 + playerThac0Bonus} : {GetWord("未命中", language_setting)}";
            }
        }

        // 怪物攻击
        int monsterAttackRoll = monster.RollAttack();
        
        if (monsterAttackRoll == 20 && immuneCriticalHit == false)  
        {
            int damage = monster.RollDamage() + monster.RollDamage(); // 根据武器和角色属性计算伤害
            player.Health -= Math.Max(damage - playerResistance,0);
            monsterLog.Text = $"{GetWord("攻击检定为", language_setting)} {monsterAttackRoll} : {GetWord("致命一击 伤害翻倍", language_setting)}\n" + 
                              $"{GetWord("造成", language_setting)} {damage} - {playerResistance} = {damage - playerResistance} {GetWord("点伤害", language_setting)}";
        }
        else if (monsterAttackRoll == 1)
        {
            monsterLog.Text = $"{GetWord("攻击检定为", language_setting)} {monsterAttackRoll} : {GetWord("严重失误 无法命中", language_setting)}";
        }
        else if (monsterAttackRoll + monster.Thac0 >= player.ArmorClass + playerACBonus)
        {
            int damage = monster.RollDamage(); // 根据武器和角色属性计算伤害
            player.Health -= Math.Max(damage - playerResistance,0);
            monsterLog.Text = $"{GetWord("攻击检定为", language_setting)} {monsterAttackRoll} + {monster.Thac0} = {monsterAttackRoll + monster.Thac0} : {GetWord("命中", language_setting)}\n" + 
                              $"{GetWord("造成", language_setting)} {damage} - {playerResistance} = {damage - playerResistance} {GetWord("点伤害", language_setting)}";        }
        else
        {
            monsterLog.Text = $"{GetWord("攻击检定为", language_setting)} {monsterAttackRoll} + {monster.Thac0} = {monsterAttackRoll + monster.Thac0} :  {GetWord("未命中", language_setting)}";
        }

        sword.Play();
        slash.Play();
        CheckHealth();
        UpdateUI();
    }

    public void UpdateUI()
    {
        language_setting = PlayerPrefs.GetInt("language");
        // 更新玩家状态文本
        playerStatusText.text = $"{GetWord(player.Name, language_setting)}\n" +
                                $"{GetWord("等级", language_setting)}: {player.Level}\n" +
                                $"{GetWord("经验", language_setting)}: {player.Experience}\n" +
                                $"{GetWord("防御等级", language_setting)}: {player.ArmorClass}\n" +
                                $"{GetWord("生命值", language_setting)}: {player.Health}\n" +
                                $"{GetWord("攻击力", language_setting)}: {player.Attack}\n" +
                                $"{GetWord("零级命中率", language_setting)}: {player.Thac0}";

        // 更新怪物状态文本
        monsterStatusText.text = $"{GetWord(monster.Name, language_setting)}\n" +
                                $"{GetWord("防御等级", language_setting)}: {monster.ArmorClass}\n" +
                                $"{GetWord("生命值", language_setting)}: {monster.Health}\n" +
                                $"{GetWord("攻击力", language_setting)}: {monster.Attack}\n" +
                                $"{GetWord("零级命中率", language_setting)}: {monster.Thac0}";

        playerLogText.text = $"{playerLog.Text}";

        monsterLogText.text = $"{monsterLog.Text}";
    }

    void HideSplash()
    {
        // 隐藏游戏结束窗口
        splash.SetActive(false);
    }
    void ShowGameOverPanel(string resultMessage)
    {
        // 显示游戏结束窗口，并设置结果文本
        resultText.text = resultMessage;
        gameOverPanel.SetActive(true);

        closeButton?.onClick.AddListener(OnBackgroundPanelClick);
    }

    void HideGameOverPanel()
    {
        // 隐藏游戏结束窗口
        gameOverPanel.SetActive(false);
    }

    // 调用此方法以处理游戏结束
    public void HandleGameOver(string resultMessage)
    {
        // 显示游戏结束窗口
        ShowGameOverPanel(resultMessage);

        // 在此可以添加其他游戏结束的逻辑，例如禁用玩家控制等

    }
    public void OnBackgroundPanelClick()
    {
        // 隐藏游戏结束窗口和背景面板
        HideGameOverPanel();
        save.playerLevel = player.Level;
        if (monster.Health <= 0)
        {
            save.level ++;
            save.SaveByJSON(save);
        }
        NextLevel();
    }
    void CheckHealth()
    {
        // 判断玩家和怪物的健康值是否小于等于0
        if (monster.Health <= 0)
        {
            
            if (tables.TbMonster.Get(save.level).Reward != 0) {
                int rewardId = tables.TbMonster.Get(save.level).Reward;
                equipment = new Equipment
                {
                    Id = tables.TbEquipment.Get(rewardId).Id,
                    Name = tables.TbEquipment.Get(rewardId).Name,
                    Description = tables.TbEquipment.Get(rewardId).Description,
                    Value = tables.TbEquipment.Get(rewardId).Value,
                };
                save.Equip(equipment.Id);
                save.SaveByJSON(save);
                playerLog.Text = $"{GetWord(player.Name, language_setting)} {GetWord("胜利", language_setting)}\n" +
                                 $"{GetWord("得到了", language_setting)} {GetWord(equipment.Name, language_setting)}\n";
                if(equipment.Value > 0) {
                    playerLog.Text += $"{GetWord(equipment.Description, language_setting)} " + equipment.Value;
                }
                else {
                    playerLog.Text += $"{GetWord(equipment.Description, language_setting)}";
                }
                EquipmentInit();
            }
            else {
                playerLog.Text = $"{GetWord(player.Name, language_setting)} {GetWord("胜利", language_setting)}";
            }
           
            HandleGameOver(playerLog.Text);
        }
        else if (player.Health + playerHPTemp <= 0)
        {
            playerLog.Text = $"{GetWord(player.Name, language_setting)} {GetWord("失败", language_setting)} {GetWord("游戏结束", language_setting)}";
            HandleGameOver(playerLog.Text);
        }
    }

}