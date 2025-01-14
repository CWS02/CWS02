    use ESG
    go
    drop TABLE if EXISTS WD40A
    CREATE TABLE WD40A (
    WD000 VARCHAR(50) NOT NULL UNIQUE,      -- 唯一碼
    WD001 VARCHAR(50),                      -- 採購單
    WD002 VARCHAR(50),                      -- 進貨單
    WD003 NVARCHAR(50),                     -- 供應廠
    WD004 NVARCHAR(50),                     -- 日期
    WD005 NVARCHAR(50),                     -- 品號
    WD006 NVARCHAR(50),                     -- 品名
    WD007 NVARCHAR(50),                     -- 已交量
    WD008 NVARCHAR(50),                     -- 交期
    WD009 NVARCHAR(50),                     -- 內容量(克/罐)
    WD010 NVARCHAR(50),                     -- 重量(克)
    WD011 NVARCHAR(50),                     -- 二氧化碳占比%
    WD012 NVARCHAR(50),                     -- 二氧化碳重量(克)
    IP NVARCHAR(20),                        -- IP
    status INT,                             -- 狀態 0=未審核,1=刪除
    CreateTime DATETIME DEFAULT GETDATE(),  -- 建立時間(自動帶入)
    PRIMARY KEY (WD000)                     -- 設置主鍵
);
