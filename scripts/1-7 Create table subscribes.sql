CREATE TABLE subscribes
(
   subscribeid uuid PRIMARY KEY,
   userid uuid NOT NULL REFERENCES clients,
   streamid text NOT NULL,
   date TIMESTAMP WITHOUT TIME ZONE  NOT NULL,
   isactive bool DEFAULT true,
   lastevent integer NOT NULL DEFAULT 0
);

