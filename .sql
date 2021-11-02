-- Criação do banco de dados

CREATE TABLE data
  (
     id          VARCHAR NOT NULL CONSTRAINT data_pk PRIMARY KEY,
     temperature INTEGER NOT NULL,
     rain        VARCHAR(10) NOT NULL,
     humidity    VARCHAR(10) NOT NULL,
     month       VARCHAR(15) NOT NULL
  ); 