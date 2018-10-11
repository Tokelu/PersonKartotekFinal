--
-- Create Table    : 'City'   
-- CityID          :  
-- CityName        :  
-- PostalCode      :  
--
CREATE TABLE City (
    CityID         INT NOT NULL IDENTITY,
    CityName       VARCHAR(500) NULL,
    PostalCode     VARCHAR(10) NULL,
CONSTRAINT pk_City PRIMARY KEY CLUSTERED (CityID))