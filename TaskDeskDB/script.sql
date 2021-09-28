create table table_priority
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    priority TEXT NOT NULL
);

create table table_status
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    status TEXT NOT NULL
);

create table table_task
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name           TEXT NOT NULL,
    description    TEXT,
    creation_date  TEXT NOT NULL,
    execution_date TEXT NOT NULL,
    status_id      INTEGER NOT NULL,
    priority_id    INTEGER NOT NULL,
    is_deleted     INTEGER NOT NULL
);


