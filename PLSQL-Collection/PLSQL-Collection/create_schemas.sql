-- vim: foldmethod=marker  foldmarker={,}

create user from_schema_A  -- {
--
--     This is the schema that is supposed to be
--     cloned.
--
       identified by p
       quota unlimited on users;

grant  create procedure,
       create session,
       create table,
       create trigger,
       create view
   to  from_schema_A;

-- The following grants only used if from_schema_A performs
-- the export:
-- grant  read, write on directory DATAPUMP_DIR to from_schema_A;

-----  }

create user schema_B -- {
--
--     This is a schema that contains objects that
--     are referenced by from_schema_A
--
       identified by p
       quota unlimited on users;

grant  create session,
       create table
   to  schema_B;

-----  }


-----  }

create user exp_imp_admin -- {
--
--     this is the user that will perform the export and the import
--
       identified by p;
grant  exp_full_database,
       imp_full_database
   to  exp_imp_admin;

alter user exp_imp_admin quota unlimited on users;

-----  }

connect schema_b/p
----------------------- {

create table table_b_1 (
       a    number,
       b    varchar2(10)
);


-- from_schema_A will create a view view_a_1 that
-- selects from table_b_1. from_schema_A will also
-- create a trigger that inserts into table_b_1.
--
-- So, we have to grant insert and select on this
-- table to from_schema_A:
grant insert, select on table_b_1 to from_schema_A;


-----  }

connect from_schema_a/p
----------------------- {

create table table_a_1 (
       c     number,
       d     varchar2(20)
);

insert into table_a_1 values (10, 'ten'   );
insert into table_a_1 values (11, 'eleven');

create view view_a_1 as 
select * from schema_b.table_b_1;


create package package_a_1 as

    function count_a return number;
    function count_b return number;

end package_a_1;
/

create package body package_a_1 as -- {

    function count_a return number is -- {
      ret number;
    begin
      select count(*) into ret 
        from  table_a_1;
      
      return ret;
    end count_a; -- }

    function count_b return number is -- {
      ret number;
    begin
      select count(*) into ret 
        from  view_a_1;
      
      return ret;
    end count_b; -- }

end package_a_1; -- }
/


create trigger trigger_a
  before insert on table_a_1
  for each row
begin
    insert into schema_b.table_b_1 values (:new.c, :new.d);
end trigger_a;
/

--     There's a trigger on the table, so the
--     following insersts should fill table_b_1
--    (in schema_b):
insert into table_a_1 values (1, 'one');
insert into table_a_1 values (2, 'two');

@@call_count_procs

-----  }
view_a_1 as 
select * from schema_b.table_b_1;


create package package_a_1 as

    function count_a return number;
    function count_b return number;

end package_a_1;
/

create package body package_a_1 as -- {

    function count_a return number is -- {
      ret number;
    begin
   