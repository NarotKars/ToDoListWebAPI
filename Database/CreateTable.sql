USE ToDoDB

GO

CREATE TABLE ToDo
(
	Id int identity(1,1) PRIMARY KEY,
	WhatToDo varchar(200),
	Edit varchar(2),
	Completed varchar(2)
)