using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public enum C2SProtocol
{    
    //account
    c2s_post_create_acc,
    c2s_post_login,

    //msg
    c2s_post_sendmsg,

    c2s_total,
    c2s_post_getUserMsg
}

public class C2S_SYNC_COLLECTION_UNLOCK
{
    public object diamond;
    public C2S_COLLECTION_INFO cjdinfo;
}

public class C2S_COLLECTION_INFO
{
    public object No;
    public object level;
    public object status;
    public object speed;
    public object maxGen;
    public object posX;
    public object posY;
    public object repair;
}

public class C2S_SYNC_RECORD_LIST
{
    public object error;
    public List<C2S_SYNC_RECORD> result = new List<C2S_SYNC_RECORD>();
}

public class C2S_SYNC_RECORD
{
    public object fightername;
    public object fighterlevel;
    public object heroId;
    public object liquid1;
    public object num1;
    public object liquid2;
    public object num2;
    public object liquid3;
    public object num3;
    public object liquid4;
    public object num4;
    public object battery;
    public object diamond;
    public object status;
}

public class C2S_SYNC_RANK
{
    public List<C2S_RANK_META> rank = new List<C2S_RANK_META>();
    public C2S_RANK_META my;
    public object change;
}

public class C2S_RANK_META
{
    public object rank;
    public object eva;
    public object lvl;
    public object name;
    public object res;
}

public class C2S_SYNC_MAIL
{
    public object error;
    public List<C2S_SYNC_MAIL_LIST> result = new List<C2S_SYNC_MAIL_LIST>();
}

public class C2S_SYNC_MAIL_LIST
{
    public object id;
    public object title;
    public object content;
    public object expiry;
    public List<C2S_SYNC_MAIL_REWARD> reward = new List<C2S_SYNC_MAIL_REWARD>();
}

public class C2S_SYNC_MAIL_REWARD
{
    public object type;
    public object id;
    public object num;
}

public class C2S_SYNC_BASE
{

}

public class C2S_SYNC_BASE_DATA : C2S_SYNC_BASE
{
    public C2S_SYNC_STATUS userStatus;
    public List<C2S_SYNC_USER_EX_ITEM> item = new List<C2S_SYNC_USER_EX_ITEM>();
    public List<C2S_SYNC_USER_HERO> userHero = new List<C2S_SYNC_USER_HERO>();
    public List<C2S_SYNC_USER_TOWER> userTower = new List<C2S_SYNC_USER_TOWER>();
    public C2S_SYNC_MAPLIST city;
    public object pvpflag;
    public List<C2S_SYNC_SKILL_ITEM> liquid;
    public List<C2S_SYNC_EQUIP> allEquip;
    public C2S_SYNC_PLAYERCONFIG config;
    public object pvprefresh;
}

public class C2S_SYNC_STATUS
{
    public object userLevel;
    public object name;
    public object userExp;
    public object userPower;
    public object userCans;
    public object userDiamond;
    public object sandBag;
    public object heroLimitd;
    public object nextLvlExp;
}

public class C2S_SYNC_RECEIVE_MAIL
{
    public C2S_SYNC_STATUS userStatus;
    public List<C2S_SYNC_MAIL_REWARD> reward = new List<C2S_SYNC_MAIL_REWARD>();
}

public class C2S_SYNC_PLAYERCONFIG
{
    public object tutorial;
    public object pvptutorial;
    public object lang;
}

public class C2S_SYNC_USER_EX_ITEM
{
    public object itemid;
    public object remain;
}

public class C2S_SYNC_USER_HERO
{
    public object heroId;
    public object crystal;
    public List<C2S_SYNC_CARD_EQUIPMENT> equip;
    public object deckNum;
    public object liquidNum;
}

public class C2S_SYNC_USER_TOWER
{
    public object towerId;
    public object level;
    public object crystal;
    public List<C2S_SYNC_CARD_EQUIPMENT> equip;
    public object deckNum;
}

public class C2S_SYNC_CARD_EQUIPMENT
{
    public object did;
    public object eid;
}

public class C2S_SYNC_MAPLIST
{
    public List<C2S_SYNC_WALL> sandbag;
    public List<C2S_SYNC_BUILDING> building;
    public List<C2S_SYNC_TOWER> tower;
}

public class C2S_SYNC_WALL
{
    public object x;
    public object y;
}

public class C2S_SYNC_BUILDING
{
    public object type;
    public object x;
    public object y;
    public object index;
    public object status;
    public object level;
}

public class C2S_SYNC_TOWER
{
    public object towerId;
    public object x;
    public object y;
}

public class C2S_SYNC_SKILL_ITEM
{
    public object sid;
    public object level;
    public object amount;
    public object producing;
    public object endtime;
    public object cost;
    public object now;
}

public class C2S_SYNC_WAVELIST {
    public object error;
    public List<C2S_SYNC_WAVE_SETTTING> result = new List<C2S_SYNC_WAVE_SETTTING>();
}

public class C2S_SYNC_WAVE_SETTTING {
    public object id;
    public object remain;
    public object hid;
    public object hnum;
    public object bid;
    public object bnum;
    public object ring;
    public object tlimit;
    public object fdam;
    public object mdesc;
    public object bdesc;
    public object interval;
}

public class C2S_SYNC_HELPLIST
{
    public object error;
    public List<C2S_SYNC_HELP_SETTTING> result = new List<C2S_SYNC_HELP_SETTTING>();
}

public class C2S_SYNC_HELP_SETTTING
{
    public object id;
    public object hid;
    public object hnum;
    public object bid;
    public object bnum;
    public object tlimit;
    public object fdam;
    public object interval;
    public object rype;
    public object rnum;
    public object mdesc;
    public object bdesc;
}


public class C2S_SYNC_HELPMAPINFO {
    public object error;
    public List<C2S_SYNC_BUILDING> result;
}

public class C2S_SYNC_UPGRADE_SKILL
{
    public object cans;
    public C2S_SYNC_SKILL_ITEM Info;
}

public class C2S_SYNC_GEN_RESULT
{
    public object cans;
    public object sid;
    public object endAt;
    public object num;
    public object now;
    public object cost;
}


public class C2S_SYNC_STAGELIST {
    public List<object> history = new List<object>();
    public List<object> list = new List<object>();
    public List<C2S_SYNC_OCCUPYSTAGEINFO> info = new List<C2S_SYNC_OCCUPYSTAGEINFO>();
}

public class C2S_SYNC_PVPLIST{
    public object error;
    public List<C2S_SYNC_PVPSTAGEINFO> result;    
}

public class C2S_SYNC_OCCUPYSTAGEINFO
{
    public object stageNo;
    public object Name;
    public object Forces;
    public object Desc;
    public object DropEquip1;
    public object DropEquip2;
    public object DropEquip3;
    public object DropCard;    
}

[Serializable]
public class C2S_SYNC_PVPSTAGEINFO{
    public object stageNo;
    public object enemyId;
    public object rank;
    public object length;
    public object eva;
    public object name;
    public object level;
}

    public class C2S_SYNC_EQUIP
{
    public object eid;
    public object amount;
    public object remain;
}

public class C2S_SYNC_HERO_EVOLUTION_RESULT
{
    public object diamond;
    public C2S_SYNC_EVOLUTION_ATT heroInfo;
}

public class C2S_SYNC_EVOLUTION_ATT
{
    public object heroId;
    public object equipNum;
    public object liquidNum;
}

public class C2S_SYNC_TOWER_EVOLUTION_RESULT
{
    public object diamond;
    public C2S_SYNC_EVOLUTION_ATT_TOWER towerInfo;
}

public class C2S_SYNC_EVOLUTION_ATT_TOWER
{
    public object towerId;
    public object level;
    public object deckNum;
    public object crystal;
    public List<C2S_SYNC_CARD_EQUIPMENT> equip = new List<C2S_SYNC_CARD_EQUIPMENT>();
}

public class C2S_SYNC_LOAD_EQUIP
{
    public C2S_SYNC_USER_EX_ITEM item;
    public C2S_SYNC_OLD_EQUIP oldEquip;
    public C2S_SYNC_OLD_EQUIP newEquip;
    public object cid;
    public object did;
    public object eid;
}

public class C2S_SYNC_UNLOAD_EQUIP
{
    public C2S_SYNC_USER_EX_ITEM item;
    public C2S_SYNC_OLD_EQUIP oldEquip;
    public object cid;
    public object did;
    public object eid;
}

public class C2S_SYNC_OLD_EQUIP
{
    public object eid;
    public object remain;
}

public class C2S_SYNC_COMPOUND_EQUIP
{
    public List<C2S_SYNC_EQUIP> equipInfo = new List<C2S_SYNC_EQUIP>();
    public object battery;
}

public class C2S_SYNC_HELPBATTLEEND_DATA : C2S_SYNC_BASE
{
    public object rate;
    public object type;
    public object num;
    public C2S_SYNC_STATUS userStatus;
    
}


public class C2S_SYNC_BATTLEEND_DATA : C2S_SYNC_BASE
{
    public C2S_SYNC_STATUS userStatus;
    public C2S_SYNC_BATTLE_REWARD reward;
    public C2S_SYNC_BATTLE_DROP drop;
    public object pvprefresh;
    public object pvpflag;
    public List<C2S_SYNC_BATTLE_END_LIQUID> liquid = new List<C2S_SYNC_BATTLE_END_LIQUID>();
}

public class C2S_SYNC_BATTLEEND_FAIL : C2S_SYNC_BASE
{
    public C2S_SYNC_STATUS userStatus;
    public List<C2S_SYNC_BATTLE_END_LIQUID> liquid = new List<C2S_SYNC_BATTLE_END_LIQUID>();
}

public class C2S_SYNC_BATTLE_END_LIQUID
{
    public object lid;
    public object remain;
}

public class C2S_SYNC_PVP_BATTLEEND_DATA : C2S_SYNC_BASE
{
    public C2S_SYNC_PVP_REWARD_DATA reward;
    public List<C2S_SYNC_BATTLE_END_LIQUID> liquid = new List<C2S_SYNC_BATTLE_END_LIQUID>();
    public C2S_SYNC_PVP_NowRank_DATA nowRank;
    public object next;
    public C2S_SYNC_STATUS userStatus;
}

public class C2S_SYNC_PVP_REWARD_DATA
{
    public C2S_SYNC_PVP_RE_FIGHTER_DATA fighter;
    public C2S_SYNC_PVP_RE_FIGHTER_DATA defenser;
    public object rate;
    
}

public class C2S_SYNC_PVP_RE_FIGHTER_DATA
{
    public object power;
    public object exDiamond;
}

public class C2S_SYNC_PVP_NowRank_DATA
{
    public object fr;
    public object dr;
    public object exchange;
}


public class C2S_SYNS_TDFAIL {
    public object userLevel;
    public object nextLvlExp;
    public object name;
    public object userExp;
    public object userPower;
    public object userCans;
    public object userDiamond;
    public object sandBag;
    public object heroLimitd;
}

public class C2S_SYNC_BATTLE_REWARD {
    public object rate;
    public object exDiamond;
    public object can;
    public object power;
    public object exp;
    public object wl;
    public object hl;
}

public class C2S_SYNC_BATTLE_DROP
{
    public List<object> equip = new List<object>();
    public object card;
    public object count;
    public object battery;
}


// 应该 还需要一个 tower Id 
public class C2S_SYNC_REWARD_HERO
{
    public object heroId;
    public object crystal;    
}

public class C2S_SYNC_REWARD_TOWER
{
    public object heroId;
    public object level;
    public object crystal;
}

public class C2S_SYNC_GEN_ACC
{
    public object diamond;
    public C2S_SYNC_LIQUID_ACC_RESULT liquid;
}

public class C2S_SYNC_LIQUID_ACC_RESULT
{
    public object sid;
    public object amount;
}

public class C2S_SYNC_GEN_STOP
{
    public object sid;
    public object amount;
    public object userCans;
}

public class C2S_SYNC_GEN_TIME
{
    public List<C2S_SYNC_SKILL_ITEM> liquid;
}

public class C2S_SYNC_STORE_INFO
{ 
    public C2S_SYNC_STORE_HERO_INFO heroShop;
    public C2S_SYNC_STORE_BOX_INFO box;
    public C2S_SYNC_STORE_DIAMOND_INFO charge;
}

public class C2S_SYNC_STORE_HERO_INFO
{
    public C2S_SYNC_STORE_HERO_META normal;
    public C2S_SYNC_STORE_HERO_META medium;
    public C2S_SYNC_STORE_HERO_META advanced;
}

public class C2S_SYNC_STORE_HERO_META
{
    public object cid;
    public object nowcost;
    public object remian;
    public object costincr;
    public object id;
}

public class C2S_SYNC_STORE_BOX_INFO
{
    public object nbox;
    public object mbox;
    public object abox;
}

public class C2S_SYNC_STORE_DIAMOND_INFO
{
    public C2S_SYNC_STORE_DIAMOND_META diamond100;
    public C2S_SYNC_STORE_DIAMOND_META diamond200;
    public C2S_SYNC_STORE_DIAMOND_META diamond300;
    public C2S_SYNC_STORE_DIAMOND_META diamond400;
    public C2S_SYNC_STORE_DIAMOND_META diamond500;
}

public class C2S_SYNC_STORE_DIAMOND_META
{
    public object num;
    public object price;
}

public class C2S_SYNC_BUY_HERO_RESULT
{
    public C2S_SYNC_STORE_HERO_INFO heroShop;
    public C2S_SYNC_USER_HERO heroInfo;
    public object nowDiamond;
}

public class C2S_SYNC_BUY_BOX_RESULT
{
    public object nowDiamond;
    public List<C2S_SYNC_STORE_BOX_META> boxInfo = new List<C2S_SYNC_STORE_BOX_META>();
    public List<C2S_SYNC_REWARD_HERO> towerInfo = new List<C2S_SYNC_REWARD_HERO>();
}

public class C2S_SYNC_STORE_BOX_META
{
    public object cid;
    public object category;
}

public class C2S_SYNC_OCCUPY_BEGIN
{
    public List<object> equip = new List<object>();
    public object card;
    public object count;
    public object battery;
}
