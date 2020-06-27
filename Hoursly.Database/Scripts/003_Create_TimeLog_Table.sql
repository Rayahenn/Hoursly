﻿CREATE TABLE IF NOT EXISTS TimeLogs(
	[Id] INTEGER PRIMARY KEY,
	[PublicId] TEXT NOT NULL UNIQUE,
	[StartDate] DATETIME NOT NULL,
	[EndDate] DATETIME NOT NULL,
	[ProjectId] INTEGER NOT NULL,
	CONSTRAINT FK_ProjectTimeLogs FOREIGN KEY (ProjectId)
    REFERENCES Projects(ProjectId)
);