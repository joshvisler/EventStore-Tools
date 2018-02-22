CREATE TABLE subscribes
(
   subscribeid uuid PRIMARY KEY,
   userid uuid NOT NULL REFERENCES clients,
   streamid text NOT NULL,
   date TIMESTAMP WITHOUT TIME ZONE  NOT NULL,
   connectionid uuid NOT NULL REFERENCES connections,
   isactive bool DEFAULT true,
   laststreamevent integer NOT NULL DEFAULT 0
);

