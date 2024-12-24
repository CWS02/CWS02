use CPC
go

DROP TABLE IF EXISTS INTRA;
CREATE TABLE INTRA (
    INT000 VARCHAR(36) NOT NULL,        -- 隨機碼
    INT001 NVARCHAR(50) NOT NULL,       -- 客戶名稱
    INT002 VARCHAR(2) ,                 -- 型態別
    INT003 NVARCHAR(50),                -- 地址
    INT004 VARCHAR(20),                 -- TEL
    INT005 VARCHAR(20),                 -- FAX
    INT006 VARCHAR(20),                 -- 國家
    INT007 VARCHAR(20),                 -- 區域
    INT008 VARCHAR(20),                 -- 訪談時間
    INT009 NVARCHAR(50),                -- 接洽人1
    INT010 NVARCHAR(50),                -- 職稱1
    INT011 NVARCHAR(50),                -- 分機1
    INT012 NVARCHAR(50),                -- 接洽人2
    INT013 NVARCHAR(50),                -- 職稱2
    INT014 NVARCHAR(50),                -- 分機2
    INT015 NVARCHAR(50),                -- 接洽人3
    INT016 NVARCHAR(50),                -- 職稱3
    INT017 NVARCHAR(50),                -- 分機3
    INT018 TEXT,                        -- 內容
    IP NVARCHAR(20),                    -- IP
    status int,                  -- 狀態 0=啟用 1=刪除
    CreateTime DATETIME DEFAULT GETDATE(),-- 建立時間(自動帶入)
    PRIMARY KEY(INT000)
);

DROP TABLE IF EXISTS INTRB;
CREATE TABLE INTRB (
    INT000 VARCHAR(36) NOT NULL,             -- 隨機碼
    INT001 NVARCHAR(50) NOT NULL,           -- 主題
    INT002 INT,                             -- 訪談記錄別 1=到訪 2=電話 3=通信
    INT003 NVARCHAR(150),                   -- 檔案網址
    INT004 TEXT,                            -- 內容
    INT999 VARCHAR(36) NOT NULL,            -- INTRA-INT000關聯
    IP NVARCHAR(20),                        -- IP
    status INT,                             -- 狀態 0=啟用 1=刪除
    CreateTime DATETIME DEFAULT GETDATE(),  -- 建立時間(自動帶入)
    PRIMARY KEY (INT000)                    -- 設置主鍵
);
