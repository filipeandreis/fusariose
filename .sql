-- Criação do banco de dados

CREATE TABLE data
  (
    id          VARCHAR NOT NULL CONSTRAINT data_pk PRIMARY KEY,
    temperature INTEGER NOT NULL,
    rain        BOOLEAN DEFAULT false NOT NULL,
    humidity    BOOLEAN DEFAULT false NOT NULL,
                date TIMESTAMP DEFAULT Now() NOT NULL
  );ALTER TABLE data owner TO cgaxvztm;