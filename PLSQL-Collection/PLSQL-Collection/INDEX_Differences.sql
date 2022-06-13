--COmposite Index Examples and performance compared.
CREATE TABLE IDX_TST
(
C1 VARCHAR2(10),
C2 VARCHAR2(10),
C3 VARCHAR2(10),
C4 VARCHAR2(10)
);
CREATE INDEX TDX_TST_IDX1 ON IDX_TST(C1,C2,C3,C4);


DECLARE

lc NUMBER(10) := 30000;

BEGIN

INSERT INTO IDX_TST ( C1,C2,C3,C4 )
SELECT TO_CHAR(LEVEL) C1,'HELLO'||LEVEL C2 , NULL,NULL
FROM DUAL CONNECT BY LEVEL < lc;

END;
/

EXPLAIN PLAN FOR SELECT * from IDX_TST WHERE C1 = '27743'and C2 = 'HELLO27741';

select * from table(dbms_xplan.display);

--DROP INDEX TDX_TST_IDX1;



NO Index
-----------------------------------------------------------------------------
| Id  | Operation         | Name    | Rows  | Bytes | Cost (%CPU)| Time     |
-----------------------------------------------------------------------------
|   0 | SELECT STATEMENT  |         |     3 |    84 |   102   (0)| 00:00:02 |
|*  1 |  TABLE ACCESS FULL| IDX_TST |     3 |    84 |   102   (0)| 00:00:02 |



C1

| Id  | Operation                   | Name         | Rows  | Bytes | Cost (%CPU)| Time     |
--------------------------------------------------------------------------------------------
|   0 | SELECT STATEMENT            |              |     3 |    84 |    24   (0)| 00:00:01 |
|*  1 |  TABLE ACCESS BY INDEX ROWID| IDX_TST      |     3 |    84 |    24   (0)| 00:00:01 |
|*  2 |   INDEX RANGE SCAN          | TDX_TST_IDX1 |   120 |       |     1   (0)| 00:00:01 |



C1,C2
--------------------------------------------------------------------------------------------
|   0 | SELECT STATEMENT            |              |     3 |    84 |     2   (0)| 00:00:01 |
|   1 |  TABLE ACCESS BY INDEX ROWID| IDX_TST      |     3 |    84 |     2   (0)| 00:00:01 |
|*  2 |   INDEX RANGE SCAN          | TDX_TST_IDX1 |     1 |       |     1   (0)| 00:00:01 |
--------------------------------------------------------------------------------------------


C1,C2,C3

| Id  | Operation                   | Name         | Rows  | Bytes | Cost (%CPU)| Time     |
--------------------------------------------------------------------------------------------
|   0 | SELECT STATEMENT            |              |     3 |    84 |     3   (0)| 00:00:01 |
|   1 |  TABLE ACCESS BY INDEX ROWID| IDX_TST      |     3 |    84 |     3   (0)| 00:00:01 |
|*  2 |   INDEX RANGE SCAN          | TDX_TST_IDX1 |     1 |       |     2   (0)| 00:00:01 |
--------------------------------------------------------------------------------------------


C1,C2,C3,C4
---------------------------------------------------------------------------------
| Id  | Operation        | Name         | Rows  | Bytes | Cost (%CPU)| Time     |
---------------------------------------------------------------------------------
|   0 | SELECT STATEMENT |              |     3 |    84 |     2   (0)| 00:00:01 |
|*  1 |  INDEX RANGE SCAN| TDX_TST_IDX1 |     3 |    84 |     2   (0)| 00:00:01 |
---------------------------------------------------------------------------------
ws  | Bytes | Cost (%CPU)| Time     |
----------