--
-- Create Table    : 'Note'   
-- NoteID          :  
-- PersonID        :  (references Person.PersonID)
-- Note            :  
--
CREATE TABLE Note (
    NoteID         INT NOT NULL,
    PersonID       INT NOT NULL,
    Note           NVARCHAR(MAX) NULL,
CONSTRAINT pk_Note PRIMARY KEY CLUSTERED (NoteID),
CONSTRAINT fk_Note FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)