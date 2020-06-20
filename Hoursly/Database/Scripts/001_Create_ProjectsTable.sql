﻿CREATE TABLE IF NOT EXISTS Projects(
	[Id] INTEGER PRIMARY KEY,
	[PublicId] TEXT  NOT NULL UNIQUE,
	[Name] TEXT NOT NULL,
	[StartDate] DATETIME NOT NULL,
	[EndDate] DATETIME NULL,
	[Priority] INTEGER NOT NULL,
	[TaskLimit] INTEGER NULL
);