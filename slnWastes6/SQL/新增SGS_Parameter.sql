use ESG
go
create table SGS_Parameter(
    PAR000 NVARCHAR(40)not null, --隨機
    PAR001 NVARCHAR(20)not null, --項目
    PAR002 NVARCHAR(30)not null, --細數
    PAR003 NVARCHAR(20),         --單位
    PAR004 NVARCHAR(6),          --公告年份
    PAR005 NVARCHAR(20),         --IP
    PAR006 NVARCHAR(20),         --時間
    PAR007 smallint,             --啟用
)