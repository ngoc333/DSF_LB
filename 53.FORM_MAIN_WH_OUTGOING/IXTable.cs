using System;
using System.Drawing;

namespace COM
{


    /// <summary> 
    /// SCM_TABLE 테이블 인덱스 Enum 
    /// </summary> 
    public enum TBSCM_TABLE : int
    {
        IxMaxCt = 31,		// 인덱스 Count 
        IxPG_ID = 0,			// 프로그램 아이디	:VARCHAR2(20) 
        IxPG_SEQ = 1,			// 프로그램 SEQ	:NUMBER(22) 
        IxCOL_NAME = 2,			// 컬럼명 (디비필드명)	:VARCHAR2(20) 
        IxCOL_ORDER = 3,			// 컬럼 순번 (표시순번)	:NUMBER(22) 
        IxTABLE_NAME = 4,			// 테이블명	:VARCHAR2(20) 
        IxHEAD_DESC1 = 5,			// 헤더명(1)	:VARCHAR2(100) 
        IxHEAD_DESC2 = 6,			// 헤더명(2)	:VARCHAR2(100) 
        IxHEAD_DESC3 = 7,			// 헤더명(3)	:VARCHAR2(100) 
        IxHEAD_DESC4 = 8,			// 헤더명(4)	:VARCHAR2(100) 
        IxLAN_HEAD_DESC1 = 9,			// 언어 헤더명(1)	:VARCHAR2(100) 
        IxLAN_HEAD_DESC2 = 10,			// 언어 헤더명(2)	:VARCHAR2(100) 
        IxLAN_HEAD_DESC3 = 11,			// 언어 헤더명(3)	:VARCHAR2(100) 
        IxLAN_HEAD_DESC4 = 12,			// 언어 헤더명(4)	:VARCHAR2(100) 
        IxWIDTH = 13,			// 컬럼 너비	:NUMBER(22) 
        IxLOCK_YN = 14,			// 에디트 가능 여부	:VARCHAR2(1) 
        IxVISIBLE_YN = 15,			// VISIBLE 여부	:VARCHAR2(1) 
        IxAUTOSORT_YN = 16,			// 자동소트 여부	:VARCHAR2(1) 
        IxHALIGN = 17,			// 수평 정렬	:VARCHAR2(10) 
        IxVALIGN = 18,			// 수직 정렬	:VARCHAR2(10) 
        IxMAXROW = 19,			// 최대 행 수 : 처음 표시될 때 보여지는 행수 지정	:NUMBER(22) 
        IxFROZENCOL = 20,			// FROZEN COLUMN	:NUMBER(22) 
        IxFROZENROW = 21,			// FROZEN ROW	:NUMBER(22) 
        IxBACKCOLOR = 22,			// 배경색	:VARCHAR2(10) 
        IxFORECOLOR = 23,			// 글자색	:VARCHAR2(10) 
        IxCELLTYPE = 24,			// 셀타입	:VARCHAR2(10) 
        IxDATA_LIST_TYPE = 25,			// 셀타입이 콤보박스일때 공통코드 또는 쿼리 이용 여부 설정 (공통코드 : 0, 쿼리 : 1)	:VARCHAR2(1) 
        IxDATA_LIST_CD = 26,			// DATA_LIST_TYPE = 0 일때 공통코드 기재	:VARCHAR2(10) 
        IxDATA_LIST_QUERY = 27,			// DATA_LIST_TYPE = 1 일때 쿼리 기재	:VARCHAR2(500) 
        //이정한 추가
        IxESSENTIAL_YN = 28,
        IxCHAR_CASE = 29,
        IxMAX_NUMBER = 30,
        IxMIN_NUMBER = 31,
        IxMAX_WIDTH = 32,
        IxREMARKS = 33,			// 비고	:VARCHAR2(100) 
        IxUPD_USER = 34,			// 작성자	:VARCHAR2(10) 
        IxUPD_YMD = 35,			// 작성일자	:DATE(7) 
    }

    /// <summary> 
    /// SPC_CODE 테이블 인덱스 Enum 
    /// </summary> 
    public enum TBSCM_CODE : int
    {
        IxCOM_NAME = 0,			// 코드명	:VARCHAR2(60)  
        IxCOM_VALUE1 = 1,			// 코드값 1	:VARCHAR2(20) 
        IxCOM_DESC1 = 2,			// 코드 설명 1	:VARCHAR2(50) 
        IxCOM_VALUE2 = 3,			// 코드값 2	:VARCHAR2(20) 
        IxCOM_DESC2 = 4,			// 코드 설명 2	:VARCHAR2(50) 
        IxCOM_VALUE3 = 5,			// 코드값 3	:VARCHAR2(20) 
        IxCOM_DESC3 = 6,			// 코드 설명 3	:VARCHAR2(50) 
        IxCOM_VALUE4 = 7,			// 코드값 4	:VARCHAR2(20) 
        IxCOM_DESC4 = 8,			// 코드 설명 4	:VARCHAR2(50) 
        IxREMARKS = 9,			// 비고	:VARCHAR2(100)  
    }


    /// <summary> 
    /// SPC_CODE 테이블 인덱스 Enum 
    /// </summary> 
    public enum TBSCM_CODE_TABLE : int
    {
        IxMaxCt = 16,		// 인덱스 Count 
        IxFACTORY = 1,			// 공장	:VARCHAR2(5) 
        IxCOM_CD = 2,			// 공통 코드	:VARCHAR2(10) 
        IxCOM_SEQ = 3,			// 코드 일련번호	:NUMBER(22) 
        IxCOM_NAME = 4,			// 코드명	:VARCHAR2(60) 
        IxSYSTEM_YN = 5,			// 시스템 코드 여부	:VARCHAR2(1) 
        IxCOM_VALUE1 = 6,			// 코드값 1	:VARCHAR2(20) 
        IxCOM_DESC1 = 7,			// 코드 설명 1	:VARCHAR2(50) 
        IxCOM_VALUE2 = 8,			// 코드값 2	:VARCHAR2(20) 
        IxCOM_DESC2 = 9,			// 코드 설명 2	:VARCHAR2(50) 
        IxCOM_VALUE3 = 10,			// 코드값 3	:VARCHAR2(20) 
        IxCOM_DESC3 = 11,			// 코드 설명 3	:VARCHAR2(50) 
        IxCOM_VALUE4 = 12,			// 코드값 4	:VARCHAR2(20) 
        IxCOM_DESC4 = 13,			// 코드 설명 4	:VARCHAR2(50) 
        IxREMARKS = 14,			// 비고	:VARCHAR2(100) 
        IxUPD_USER = 15,			// 작성자	:VARCHAR2(10) 
        IxUPD_YMD = 16,			// 작성일자	:DATE(7) 
    }


    /// <summary> 
    /// TBSPC_PROC_PROG  테이블 인덱스 Class 
    /// </summary> 
    public enum TBSPC_PROC_PROG : int
    {
        IxMaxCt = 10,
        IxDIVISION = 0,
        IxRUN = 1,
        IxPROC_NAME = 2,
        IxPROC_VALUE = 3,
        IxTARGET_NAME = 4,
        IxTARGET_NAME1 = 5,
        IxTARGET_NAME2 = 6,
        IxTARGET_NAME3 = 7,
        IxTARGET_NAME4 = 8,
        IxSTATUS = 9,
    }



    /// <summary> 
    /// TBSPM_ERR  테이블 인덱스 Class 
    /// </summary> 
    public enum TBSPM_ERR : int
    {
        IxMaxCt = 13,
        IxDIVISION = 0,
        IxFACTORY = 1,
        IxERR_YMD = 2,
        IxSP_NAME = 3,
        IxJOB_CD = 4,
        IxFROM_NAME = 5,
        IxERR_DIV = 6,
        IxERR_NUM = 7,
        IxERR_MSG = 8,
        IxUSR_MSG = 9,
        IxUPD_USER = 10,
        IxUPD_YMD = 11,
        IxTemp = 12,
        IxTemp_User = 13,
    }



}
