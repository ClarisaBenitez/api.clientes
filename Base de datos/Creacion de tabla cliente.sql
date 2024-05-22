-- Table: public.cliente

-- DROP TABLE IF EXISTS public.cliente;

CREATE TABLE IF NOT EXISTS public.cliente
(
    id integer NOT NULL,
    id_banco integer NOT NULL,
    nombre character varying COLLATE pg_catalog."default" NOT NULL,
    apellido character varying COLLATE pg_catalog."default" NOT NULL,
    documento character(10) COLLATE pg_catalog."default" NOT NULL,
    direccion character varying COLLATE pg_catalog."default",
    mail character varying COLLATE pg_catalog."default",
    celular character varying(10) COLLATE pg_catalog."default",
    estado character varying COLLATE pg_catalog."default",
    CONSTRAINT pk_idcliente PRIMARY KEY (id)
        INCLUDE(id),
    CONSTRAINT un_idcliente UNIQUE (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.cliente
    OWNER to postgres;