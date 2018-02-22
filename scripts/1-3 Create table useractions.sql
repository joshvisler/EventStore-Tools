CREATE TABLE useractions
(
   actionid uuid PRIMARY KEY,
   userid uuid NOT NULL REFERENCES clients,
   message text NOT NULL,
   date TIMESTAMP WITHOUT TIME ZONE  NOT NULL
)

