DROP TABLE Url

CREATE TABLE Url (
	    Id INT IDENTITY(1000000,1) NOT NULL, --seed the database from 1000000 to start short urls with 8 characters
	    LongUrl VARCHAR(2005) NULL,
	    shortUrl VARCHAR(255) NULL,
	    CreatedDate DATETIME NULL,
	    CONSTRAINT Id PRIMARY KEY CLUSTERED (Id ASC)	
)

INSERT INTO Url (LongUrl, shortUrl) VALUES ('https://www.google.com','lfls')