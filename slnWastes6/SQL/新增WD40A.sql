    use ESG
    go
    drop TABLE if EXISTS WD40A
    CREATE TABLE WD40A (
    WD000 CHAR(32) NOT NULL DEFAULT (REPLACE(NEWID(), '-', '')),  -- 唯一碼
    WD001 VARCHAR(50),                      -- 採購單
    WD002 NVARCHAR(50),                     -- 供應廠
    WD003 NVARCHAR(8),                      -- 日期
    WD004 NVARCHAR(50),                     -- 品號
    WD005 NVARCHAR(50),                     -- 品名
    WD006 Decimal(10),                      -- 已交量
    WD007 NVARCHAR(8),                      -- 交期
    WD008 Decimal(10),                      -- 內容量(克/罐)
    WD009 Decimal(10),                      -- 重量(克)
    WD010 Decimal(10),                      -- 二氧化碳占比%
    WD011 Decimal(10,2),                    -- 二氧化碳重量(克)
    status INT,                             -- 狀態 0=未審核,1=刪除
    IP NVARCHAR(20),                        -- IP
    CreateTime DATETIME DEFAULT GETDATE(),  -- 建立時間(自動帶入)
    PRIMARY KEY (WD000)                     -- 設置主鍵
);
