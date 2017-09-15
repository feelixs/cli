#!/bin/bash




export DB="SPECDB_SSoTme"

sqlcmd -i AddGetSchemaSP.sql -d $DB
bcp "exec dbo.sp_GetSqlSchema" queryout SqlSChema.xml -d $DB -T -w -r 
