CREATE TABLE restorehistory
(
   restorehistoryid uuid PRIMARY KEY,
   userid uuid NOT NULL REFERENCES clients,
   backupid uuid NOT NULL REFERENCES backups,
   date TIMESTAMP WITHOUT TIME ZONE  NOT NULL,
   backuprestoredtime interval,
   state smallint NOT NULL DEFAULT 0 -- 0 not start, 1 - complete, 2 - in progress, 3 - error
);

