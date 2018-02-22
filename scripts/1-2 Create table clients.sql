CREATE TABLE clients
(
   clientid uuid PRIMARY KEY,
   login text NOT NULL,
   password text NOT NULL,
   roleid SMALLINT NOT NULL REFERENCES roles
);

