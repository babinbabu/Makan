SELECT 
    CASE TYPE 
        WHEN 'U' 
            THEN 'User Defined Tables' 
        WHEN 'S'
            THEN 'System Tables'
        WHEN 'IT'
            THEN 'Internal Tables'
        WHEN 'P'
            THEN 'Stored Procedures'
        WHEN 'PC'
            THEN 'CLR Stored Procedures'
        WHEN 'X'
            THEN 'Extended Stored Procedures'
    END, 
    COUNT(*)     
FROM SYS.OBJECTS
WHERE TYPE IN ('U', 'P', 'PC', 'S', 'IT', 'X')
GROUP BY TYPE

Sip login: 533286
Sip password: 764844Change SIP password
Server: call2friends.com
dbo.AppManualCurrencyList'
9/8/2016 5:08:49 PM
 --> Uploading data to LuckyDrawRewards
 --> BCP Command: bcp.exe "db_Mcms2.dbo.LuckyDrawRewards" in "c:\SQLAzureMW\BCPData\dbo.LuckyDrawRewards.dat" -E -n -C RAW -b 1000 -a 4096 -q -S Agmo-PC\SQLEXPRESS -U "sa" -P "123456"
*
BCP output file: c:\SQLAzureMW\BCPData\dbo.LuckyDrawRewards.dat.txt*
BCP output file: c:\SQLAzureMW\BCPData\dbo.LuckyDrawRewards.dat_2.txt*
BCP output file: c:\SQLAzureMW\BCPData\dbo.LuckyDrawRewards.dat_3.txt*
BCP output file: c:\SQLAzureMW\BCPData\dbo.LuckyDrawRewards.dat_4.txt
9/8/2016 5:09:35 PM --> Sorry, but BCP upload process failed:

SQLState = S0002, NativeError = 208
Error = [Microsoft][ODBC Driver 11 for SQL Server][SQL Server]Invalid object name 'db_Mcms2.dbo.LuckyDrawRewards'.
SQLState = 37000, NativeError = 11529
Error = [Microsoft][ODBC Driver 11 for SQL Server][SQL Server]The metadata could not be determined because every code path results in an error; see previous errors for some of these.




Wall Post - Get List
