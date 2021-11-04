-- Criação do banco de dados

CREATE TABLE data
  (
    id          VARCHAR NOT NULL CONSTRAINT data_pk PRIMARY KEY,
    temperature INTEGER DEFAULT 0 NOT NULL,
                date TIMESTAMP DEFAULT Now() NOT NULL,
    rain        INTEGER DEFAULT 0,
    humidity    INTEGER DEFAULT 0
  );
  
ALTER TABLE data owner TO cgaxvztm;