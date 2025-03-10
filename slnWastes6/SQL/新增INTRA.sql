use CPC
go

CREATE TABLE INTRA (
    INT000 VARCHAR(36) NOT NULL,        -- 隨機碼
    INT001 NVARCHAR(50) NOT NULL,       -- 客戶名稱
    INT002 VARCHAR(2) ,                 -- 型態別
    INT003 NVARCHAR(50),                -- 地址
    INT004 VARCHAR(20),                 -- TEL
    INT005 VARCHAR(20),                 -- FAX
    INT006 VARCHAR(20),                 -- 國家
    INT007 VARCHAR(20),                 -- 區域
    INT008 NVARCHAR(50),                -- 接洽人1
    INT009 NVARCHAR(50),                -- 職稱1
    INT010 NVARCHAR(50),                -- 分機1
    INT011 NVARCHAR(50),                -- 接洽人2
    INT012 NVARCHAR(50),                -- 職稱2
    INT013 NVARCHAR(50),                -- 分機2
    INT014 NVARCHAR(50),                -- 接洽人3
    INT015 NVARCHAR(50),                -- 職稱3
    INT016 NVARCHAR(50),                -- 分機3
    INT017 TEXT,                        -- 業務範圍
    INT018 NVARCHAR(100),  -- 信用額度
    INT019 NVARCHAR(100),  -- 成立時間
    INT020 NVARCHAR(100),  -- 公司網站
    INT021 NVARCHAR(100),  -- 公司營業額
    INT022 NVARCHAR(100),  -- 年營業額
    INT023 NVARCHAR(100),  -- 員工人數
    INT024 NVARCHAR(100),  -- 統編
    INT025 NVARCHAR(100),  -- 公司負責人
    INT026 NVARCHAR(100),  -- 付款條件
    INT027 NVARCHAR(100),  -- Email
    IP NVARCHAR(20),                    -- IP
    status int,                         -- 狀態 0=啟用 1=刪除
    CreateTime DATETIME DEFAULT GETDATE(),-- 建立時間(自動帶入)
    PRIMARY KEY(INT000)
);

CREATE TABLE INTRB (
    INT000 VARCHAR(36) NOT NULL,            -- 隨機碼
    INT001 NVARCHAR(50) NOT NULL,           -- 主題
    INT002 INT,                             -- 訪談記錄別 1=到訪 2=電話 3=通信
    INT003 NVARCHAR(150),                   -- 檔案網址
    INT004 TEXT,                            -- 內容
    INT005 VARCHAR(20),                     -- 訪談時間
    INT006 TEXT,                            -- 主管回覆欄
    INT007 NVARCHAR(50),                    -- 專案名稱

    INT999 VARCHAR(36) NOT NULL,            -- INTRA-INT000關聯
    IP NVARCHAR(20),                        -- IP
    status INT,                             -- 狀態 0=未審核,1=已審核 2=刪除
    CreateTime DATETIME DEFAULT GETDATE(),  -- 建立時間(自動帶入)
    Level INT,
    PRIMARY KEY (INT000)                    -- 設置主鍵
);
drop table if EXISTS INTRD;
CREATE TABLE INTRD (
    INT000 UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL, -- 自動生成 GUID
    INT001 NVARCHAR(100) NOT NULL,           -- 名稱
    INT002 NVARCHAR(100) NOT NULL,           -- 國家
    INT003 NVARCHAR(100),                    -- 區間
    status int,                              -- 顯示 0=追蹤中 1=已結案 2=正式訂單
    CreateTime DATETIME DEFAULT GETDATE(),   -- 建立時間(自動帶入)
    PRIMARY KEY (INT000)                     -- 設置主鍵
);


CREATE TABLE INTRE (
    INT000 VARCHAR(36) DEFAULT NEWID() PRIMARY KEY, -- 唯一值
    INT001 NVARCHAR(20),          -- 追蹤日期
    INT002 NVARCHAR(MAX),         -- 內容
    INT003 INT,                   -- 聯絡方式
    INT004 NVARCHAR(MAX),         -- 後續步驟
    IP NVARCHAR(20),              -- IP
    status INT,                   -- 狀態 (0=啟用, 1=刪除)
    CreateTime DATETIME DEFAULT GETDATE(), -- 建立時間 (自動帶入)
    INT999 VARCHAR(36),      -- 與 INTRB 關聯
);

