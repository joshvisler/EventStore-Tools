CREATE TABLE backups
(
   backupid uuid PRIMARY KEY,
   userid uuid NOT NULL REFERENCES clients,
   path text NOT NULL,
   created TIMESTAMP WITHOUT TIME ZONE  NOT NULL,
   backupcreatedtime interval
);

