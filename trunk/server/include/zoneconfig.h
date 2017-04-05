#ifndef _ZONECONFIG_H
#define _ZONECONFIG_H

/*
    ZONE²ã¸÷³ÌĞò:zoneconnect zoneserver zoneconfig
    Ê¹ÓÃµÄÒ»Ğ©¹«¹²¶¨Òå
*/
#include    "define.h"
#include    "work_dir.h"
#include    "LogServerCfg.h"
#include    "shmkeydefine.h"


#define DFT_CONFIG_FILE    "zone/config/zone.conf"

typedef enum
{
    SESSION_TYPE_INIT = 1,      // ZoneConnect·¢¸øZoneServerµÄµÚÒ»¸ö°üµÄSession,¿ÉÒÔÌØÊâ´¦ÀíÒ»Ğ©ĞÅÏ¢
    SESSION_TYPE_RUN  = 2,      // Á¬½Ó³É¹¦½¨Á¢ºóËùÓĞµÄSession¾ùÎª¸ÃÀàĞÍ
    SESSION_TYPE_STOP = 3,      // ZoneConnectÍ¨ÖªZoneServer Client¶ÏÏß£¬»òZoneServerÍ¨ÖªZoneConnect¹Ø±ÕÁ¬½ÓÊ±
    SESSION_TYPE_QUEUE = 4,     //ÅÅ¶ÓÏà¹ØĞÅÏ¢session
} EOnlineSessionType;

// ZoneConnectÓëZoneServer½øĞĞ½»»¥µÄSessionĞÅÏ¢
struct STZoneConnOnlineSession
{
	char    m_cType;         	// 	Session see EOnlineSessionType
	UINT    m_dwClientPos;    	// 	zoneConnect	: index of CCltMng
	UINT    m_dwClientSeqNo; 	//  	zoneConnect	: timestamp of connect,not for search.For check only
	UINT    m_dwObjID;       	//  	zoneServer	: index of PlayerMgr

	void clone(const STZoneConnOnlineSession& from)
	{
		this->m_cType 		= from.m_cType;
		this->m_dwClientPos	= from.m_dwClientPos;
		this->m_dwClientSeqNo=from.m_dwClientSeqNo;
		this->m_dwObjID		= from.m_dwObjID;
	}

	void Reset()
	{
		this->m_cType 		= -1;
		this->m_dwClientPos	= 0;
		this->m_dwClientSeqNo=0;
		this->m_dwObjID		= 0;
	}

};

// ÓëZoneÓĞ½»»¥µÄ¸÷Ó¦ÓÃ½ø³ÌÊµÌåid
typedef struct
{
	int     m_iZoneConnect;
	int     m_iZoneServer;
	int     m_iZoneCache;
	int     m_iWorldCenter;
	
} STServerEntity;

// ZoneConnect IO½ÓÈë²ã×¨ÓĞÅäÖÃ
typedef struct
{
	char    m_sListenIP[16];
	int     m_iListenPort;

	int     m_iMaxPkgCntInSec;          // Ã¿Ãë×î¶àClientÔÊĞí°üÊı
	int     m_iMaxIdleSec;              // Client×î´ó¿ÕÏĞÊ±¼ä
	BYTE    m_bBindToCPU;               //½«½ø³Ì°ó¶¨µ½cpuÖ¸¶¨ºËÉÏ

} STZoneConnectConf;

// Zone²ãshm keyÅäÖÃ
typedef struct
{
	int     m_iZoneConfigShmKey;        // Zone²ãÅäÖÃĞÅÏ¢shm key
	int     m_iConnContextShmKey;       // ZoneConnectÁ¬½ÓÃèÊöĞÅÏ¢´æ·ÅµÄshm key
	int     m_iPlayerInfoShmKey;        // ÔÚÏß½ÇÉ«ĞÅÏ¢shm key

	int     m_iStatOfCmdShmKey;         // Í³¼Æ - ÃüÁî×ÖÍ³¼Æshm key
	int     m_iStatCommShmKey;          // LogÍ¨ÓÃÍ³¼ÆĞÅÏ¢
	int     m_iConnectQueueShmKey;      //Á¬½ÓÅÅ¶Ó¶ÓÁĞshm key
} STShmKeyConf;

typedef struct
{
	BYTE   	    m_bLogicStatOn;             // logicÍ³¼Æ¿ª¹ØÊÇ·ñ´ò¿ª
	BYTE    	m_bIsQueueNeed;             //ÅÅ¶Ó¶ÓÁĞ¿ª¹ØÊÇ·ñ´ò¿ª
	int     	m_iConnQueueLen;            //ÅÅ¶Ó¶ÓÁĞ³¤¶È
	BYTE    	m_bBindToCPU;               //½«½ø³Ì°ó¶¨µ½cpuÖ¸¶¨ºËÉÏ
	BYTE        m_bPrintDebugLog;           //ÊÇ·ñ´òÓ¡µ÷ÊÔÈÕÖ¾
	BYTE        m_bNewUserGuid;             //ĞÂÊÖÒıµ¼¿ª¹
	

	//kongfu 's conf
	int     m_iZoneServerID;
	int 	m_iPermitCntPerSecond;
	int		m_MaxPlayer;
	int     m_iSupportApple;
	int     m_iSupportAppleSandbox;

	//db's conf
	char	m_sDbIP[16];
	int		m_iDbPort;
	char	m_sDbName[64];
	char	m_sDbUser[16];
	char	m_sDbPassword[16];
	char	m_sDbCharset[16];
	char	m_sDbUnixSocket[128];
	int     m_iCompressBlob;

	//php's conf
	char    m_sPhpIP[16];

} STZoneServerConf;


// Zone²ãÍ³Ò»ÅäÖÃĞÅÏ¢
typedef struct
{
	STServerEntity      m_stSvrEntity;
	char                m_sRes1[1024 - sizeof(STServerEntity)];

	STZoneConnectConf   m_stZoneConnConf;
	char                m_sRes2[1024 - sizeof(STZoneConnectConf)];

	STShmKeyConf        m_stShmKeyConf;
	char                m_sRes3[1024 - sizeof(STShmKeyConf)];

	STZoneServerConf    m_stZoneSvrConf;
	char                m_sRes4[1024 - sizeof(STZoneServerConf)];

} STZoneConfig;

#endif

