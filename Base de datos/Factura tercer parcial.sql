-- Table: public.factura

-- DROP TABLE IF EXISTS public.factura;

CREATE TABLE IF NOT EXISTS public.factura
(
    id_cliente integer NOT NULL,
    nro_factura character varying COLLATE pg_catalog."default",
    fecha_hora character varying COLLATE pg_catalog."default",
    total_letras character varying COLLATE pg_catalog."default" NOT NULL,
    sucursal character varying COLLATE pg_catalog."default",
    total_iva5 integer NOT NULL,
    total integer NOT NULL,
    total_iva10 integer NOT NULL,
    total_iva integer NOT NULL,
    id integer NOT NULL DEFAULT nextval('factura_id_seq'::regclass),
    CONSTRAINT pk_factura PRIMARY KEY (id)
        INCLUDE(id),
    CONSTRAINT fk_cliente FOREIGN KEY (id_cliente)
        REFERENCES public.cliente (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.factura
    OWNER to postgres;
-- Index: fki_fk_cliente

-- DROP INDEX IF EXISTS public.fki_fk_cliente;

CREATE INDEX IF NOT EXISTS fki_fk_cliente
    ON public.factura USING btree
    (id_cliente ASC NULLS LAST)
    TABLESPACE pg_default;