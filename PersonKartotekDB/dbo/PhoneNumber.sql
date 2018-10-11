--
-- Create Table    : 'PhoneNumber'   
-- PhoneID         :  
-- PersonID        :  (references Person.PersonID)
-- PhoneNumber     :  
-- Provider        :  
-- PhoneType       :  
--
CREATE TABLE PhoneNumber (
    PhoneID        INT NOT NULL,
    PersonID       INT NOT NULL,
    PhoneNumber    BIGINT NULL,
    Provider       VARCHAR(500) NULL,
    PhoneType      VARCHAR(500) NULL,
CONSTRAINT pk_PhoneNumber PRIMARY KEY CLUSTERED (PhoneID),
CONSTRAINT fk_PhoneNumber FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)