CREATE TABLE connections
(
   connectionid uuid PRIMARY KEY,
   name text NOT NULL,
   userid uuid NOT NULL REFERENCES clients,
   connectionstring text NOT NULL
);

