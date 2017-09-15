Create procedure dbo.sp_GetSqlSchema
as
SELECT [Schema].name [name], DB_NAME() DBName, @@SERVERNAME DBServer,
        (SELECT [TABLE].[name]
              ,[TABLE].[object_id]
              ,(SELECT p.class as ep_class, p.class_desc as ep_class_desc, p.major_id as ep_major_id, p.minor_id as ep_minor_id,
                        p.name as ep_name, p.value as ep_value, t.*
                from sys.extended_properties p
                inner join sys.tables t on p.major_id = t.[object_id]
                where class = 1 and t.object_id = [TABLE].[object_id]   
                FOR XML PATH('ExtendedProperty'), TYPE)
              ,(SELECT [name], object_id, (select top 1 column_default 
											from INFORMATION_SCHEMA.COLUMNS isc 
											where [Table].name = isc.Table_Name and 
												  [column].name = isc.column_name and 
												  isc.table_catalog=DB_NAME()) as DefaultValue,
                            (SELECT TOP 1 DataType.name
                             FROM sys.types [DataType]
                             WHERE DataType.system_type_id = [COLUMN].system_type_id
                             FOR XML AUTO, TYPE), 
                            CASE WHEN (SELECT TOP 1 DataType.name
                             FROM sys.types [DataType]
                             WHERE DataType.system_type_id = [COLUMN].system_type_id) in ('nvarchar', 'nchar') THEN [max_length] / 2
                                ELSE [max_length]
                            END AS [max_length], 
                            [PRECISION], [scale], [collation_name], [is_nullable], [is_identity], [column_id],
                             (SELECT fk.name KeyName,
                                                pt.name [ParentTableName], 
                                                cr.name [ParentColumnName],   
                                                rt.name [ReferencedObjectDef],  
                                                crx.name [ReferencedPropertyDef]
                                FROM sys.foreign_keys fk
                                    JOIN sys.tables pt ON fk.parent_object_id = pt.object_id
                                    JOIN sys.tables rt ON fk.referenced_object_id = rt.object_id
                                    JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
                                    JOIN sys.columns crx ON fk.referenced_object_id = crx.object_id AND fkc.referenced_column_id = crx.column_id
                                    JOIN sys.columns cr ON fk.parent_object_id = cr.object_id AND fkc.parent_column_id = cr.column_id
                                WHERE pt.name = [TABLE].name AND cr.name = [COLUMN].name
                            FOR XML PATH('Relationship'), TYPE),
                            CASE WHEN EXISTS (SELECT t.name, c.name
                                FROM sys.key_constraints kc
                                    JOIN sys.tables t ON kc.parent_object_id = t.object_id
                                    JOIN sys.index_columns ic ON t.object_id = ic.object_id
                                    JOIN sys.indexes i ON t.object_id = i.object_id AND ic.index_id = i.index_id AND i.is_primary_key = 1
                                    JOIN sys.columns c ON t.object_id = c.object_id AND ic.column_id = c.column_id
                                WHERE t.object_id = [TABLE].object_id AND c.column_id = [COLUMN].column_id
                                    )
                            THEN 1 ELSE 0 END [IsPrimaryKey]
 
             FROM sys.columns [COLUMN]
             WHERE [TABLE].object_id = [COLUMN].object_id
             ORDER BY [COLUMN].column_id
             FOR XML PATH('Column'), TYPE)
         FROM sys.tables [TABLE]
         WHERE ([Schema].Schema_id = [TABLE].Schema_id) AND ([TABLE].name != 'sysdiagrams')
         ORDER BY [TABLE].name
         FOR XML PATH('Table'), TYPE)
FROM sys.Schemas [Schema]
WHERE [Schema].name = 'dbo'
ORDER BY [Schema].name
FOR XML AUTO, ROOT('MsSql');
