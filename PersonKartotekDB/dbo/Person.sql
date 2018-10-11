--
-- Create Table    : 'Person'   
-- PersonID        :  
-- FirstName       :  
-- MiddleName      :  
-- LastName        :  
-- ContactType     :  
--
CREATE TABLE Person (
    PersonID       INT NOT NULL IDENTITY,
    FirstName      varchar(500) NOT NULL,
    MiddleName     varchar(500) NULL,
    LastName       varchar(500) NOT NULL,
    ContactType    varchar(500) NULL,
    CONSTRAINT pk_Person PRIMARY KEY CLUSTERED (PersonID))