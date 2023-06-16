Create table Company
(
	Id int not null auto_increment,
    CreateDate Datetime not null, 
    Status int not null,
    Document varchar(50) not null, 
    LegalName varchar(200) not null, 
    FantasyName varchar(200) not null,
    Name varchar(200) not null,
    Site varchar(500),
    Email varchar(500),
    Phone varchar(20),
	PlaystoreLink varchar(500),
	AppleStoreLink varchar(500),
    LinkedInLink varchar(500),
    InstagramLink varchar(500),
    FacebookLink varchar(500),
    TwitterLink varchar(500),
    YoutubeChannelLink varchar(500),
    SolvedProblemDescription text,
    Mission text,
    Vision text,
    CompanyValues text,
    DisrupIdeia text,
    SocietyContribuition text,
    WorkEnvironment text,
    Category1 varchar(50),
    Category2 varchar(50),
    Category3 varchar(50),
    Category4 varchar(50),
    Category5 varchar(50),
    primary key (Id)
);

Create Table Photo
(
	Id int not null auto_increment primary key,
    CompanyId int not null,
    Name varchar(100) not null,
    Url varchar(300) not null,
    IsThumb bit not null,
    CreateDate datetime,
    foreign key (CompanyId) references Company(Id)
);