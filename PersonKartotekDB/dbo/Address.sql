--
-- Create Table    : 'Address'   
-- AddressID       :  
-- RoadName        :  
-- HouseNumber     :  
-- Story           :  
-- IsRegisteredAddress :  
-- AddressType     :  
-- CityID          :  (references City.CityID)
--
CREATE TABLE Address (
    AddressID      INT NOT NULL IDENTITY,
    RoadName       VARCHAR(50) NULL,
    HouseNumber    VARCHAR(50) NULL,
    Story          INT NULL,
    IsRegisteredAddress VARCHAR(50) NULL,
    AddressType    VARCHAR(50) NULL,
    CityID         INT NOT NULL,
    CONSTRAINT pk_Address PRIMARY KEY CLUSTERED (AddressID),
CONSTRAINT fk_Address FOREIGN KEY (CityID)
    REFERENCES City (CityID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)