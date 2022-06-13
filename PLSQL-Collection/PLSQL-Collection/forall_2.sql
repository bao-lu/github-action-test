SET SERVEROUTPUT ON;

DROP TABLE VISA_REJECT;
DROP TABLE VISA_DETAILS;

CREATE TABLE VISA_DETAILS (
VIS_TYPE_CODE VARCHAR2(4),
COUNTRY VARCHAR2(25),
NO_OF_DAYS_VALID NUMBER(4),
EXPIRY_DATE VARCHAR2(8),
CONSTRAINT VIS_PK PRIMARY KEY(VIS_TYPE_CODE)
);


CREATE TABLE VISA_REJECT (
VIS_TYPE_CODE VARCHAR2(4) ,
CONSTRAINT VIS_FK FOREIGN KEY(VIS_TYPE_CODE) REFERENCES VISA_DETAILS(VIS_TYPE_CODE)
);


DECLARE

TYPE visaCodeType IS TABLE OF VISA_DETAILS.VIS_TYPE_CODE%TYPE;
TYPE visaCountryType IS TABLE OF VISA_DETAILS.COUNTRY%TYPE;
TYPE visaValiddaysType IS TABLE OF VISA_DETAILS.NO_OF_DAYS_VALID%TYPE;

visacode1 visaCodeType := visaCodeType('0934','2345','3543'); 
visacountry1 visaCountryType := visaCountryType('BRAZIL','DENMARK','ISRAEL'); 
visavalid1 visaValiddaysType  := visaValiddaysType(4,720,200);


TYPE visarejectIdxType IS TABLE OF PLS_INTEGER;
visrej1 visarejectIdxType := visarejectIdxType();

BEGIN

FOR i IN visacode1.FIRST..visacode1.LAST 
LOOP
IF visavalid1(i) < 10 THEN
visrej1.extend();
visrej1(i)  := i;
END IF;
END LOOP;

FORALL i IN INDICES OF visacode1
INSERT INTO VISA_DETAILS VALUES ( visacode1(i), visacountry1(i),visavalid1(i),TO_CHAR( (SYSDATE + visavalid1(i)),'YYYYMMDD' ) );

FOR i IN visacode1.FIRST..visacode1.LAST 
LOOP
DBMS_OUTPUT.PUT_LINE(SQL%BULK_ROWCOUNT(i) || ' row(s) INSERTED FOR VISA CODE ' || visacode1(i) );
END LOOP;

FORALL i IN VALUES OF visrej1
INSERT INTO VISA_REJECT VALUES (visacode1(i));

END;
/E_CODE%TYPE;
TYPE visaCountryType IS TABLE OF VISA_DETAILS.COUNTRY%TYPE;
TYPE visaValiddaysType IS TABLE OF VISA_DETAILS.NO_OF_DAYS_VALID%TYPE;

visacode1 visaCodeType := visaCodeType('0934','2345','3543'); 
visacountry1 visaCountryType := visaCountryType('BRAZIL','DENMARK','ISRAEL'); 
visavalid1 visaValiddaysType  := visaValiddaysType(4,720,200);


TYPE visarejectIdxType IS TABLE OF PLS_INTEGER;
visrej1 visarejectIdxType := visarejectIdxType();

BEGIN

FOR i IN visacode1.FIRST..visacode1.LAST 
LOOP
IF visavalid1(i) < 10 THEN