--
-- Create Table    : 'EmailAddr'   
-- EmailID         :  
-- PersonID        :  (references Person.PersonID)
-- IsPrimary       :  
-- EmailAddr       :  
--
CREATE TABLE EmailAddr (
    EmailID        INT NOT NULL,
    PersonID       INT NOT NULL,
    IsPrimary      BIT NOT NULL DEFAULT 0,
    EmailAddr      VARCHAR(500) NOT NULL,
CONSTRAINT pk_EmailAddr PRIMARY KEY CLUSTERED (EmailID),
CONSTRAINT fk_EmailAddr FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)