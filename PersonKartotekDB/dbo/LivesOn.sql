--
-- Create Table    : 'LivesOn'   
-- PersonID        :  (references Person.PersonID)
-- AddressID       :  (references Address.AddressID)
--
CREATE TABLE LivesOn (
    PersonID       INT NOT NULL,
    AddressID      INT NOT NULL,
CONSTRAINT pk_LivesOn PRIMARY KEY CLUSTERED (PersonID,AddressID),
CONSTRAINT fk_LivesOn FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
CONSTRAINT fk_LivesOn2 FOREIGN KEY (AddressID)
    REFERENCES Address (AddressID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)