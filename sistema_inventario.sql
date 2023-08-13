create database sistema_inventario;
use sistema_inventario;

CREATE TABLE categoria_producto (
  id int identity(1,1) primary key,
  tipo varchar(40) NOT NULL
);

CREATE TABLE producto (
  id int identity(1,1) primary key,
  nombre varchar(40),
  descripcion varchar(120),
  precio smallmoney ,
  stock int ,
  marca varchar(40) ,
  idcategoria int  ,
  CONSTRAINT FK_CategoriaP_Producto FOREIGN KEY (idcategoria) REFERENCES categoria_producto (id)
);

CREATE TABLE usuario (
  id int identity(1,1) primary key,
  nombre varchar(45),
  username varchar(20) unique,
  password varchar(20),
  estado tinyint DEFAULT 1,
) ;

CREATE TABLE entrada_producto (
  id int identity(1,1) primary key,
  idproducto int,
  descripcion varchar(120),
  stockE int,
  id_usuario int,

  CONSTRAINT FK_Producto_Entradaproducto FOREIGN KEY (idproducto) REFERENCES producto (id),
  CONSTRAINT FK_Usuario_Entradaproducto FOREIGN KEY (id_usuario) REFERENCES usuario (id)
) ;



CREATE TABLE salida_producto (
  id int identity(1,1) primary key,
  idproducto int,
  descripcion varchar(120),
  stockS int ,
  id_usuario int,

  CONSTRAINT FK_Producto_Salidaproducto FOREIGN KEY (idproducto) REFERENCES producto (id),
  CONSTRAINT FK_Usuario_Salidaproducto FOREIGN KEY (id_usuario) REFERENCES usuario (id)
) ;

insert into categoria_producto values ('Teclados');
insert into categoria_producto values ('Mouses');
insert into categoria_producto values ('Parlantes');
insert into categoria_producto values ('Monitores');
insert into categoria_producto values ('Audifonos');

insert into producto values ('Teclado rojo','Gamers hasta el final',50,20,'SUPERAZ',1);
insert into producto values ('Mouse calavera','Sin miedo al exito',30,10,'HALION',2);
insert into producto values ('Parlante grande','A todo volumen',120,20,'SONY',3);
insert into producto values ('Monitor led','Pantalla 32 pulgadas',330,5,'LG',4);
insert into producto values ('Audifono comodo','Audifono a precio bajo',15,30,'SONY',5);


--Esto comentado es recomendable agregarlo desde el sistema 

--insert into entrada_producto values (1,'Gamers hasta el final',1,1);
--insert into entrada_producto values (2,'Sin miedo al exito',2,2);
--insert into entrada_producto values (3,'A todo volumen',3,3);
--insert into entrada_producto values (4,'Pantalla 32 pulgadas',4,4);
--insert into entrada_producto values (5,'Audifono a precio bajo',5,5);

--insert into salida_producto values (1,'Gamers hasta el final',1,1);
--insert into salida_producto values (2,'Sin miedo al exito',2,2);
--insert into salida_producto values (3,'A todo volumen',3,3);
--insert into salida_producto values (4,'Pantalla 32 pulgadas',4,4);
--insert into salida_producto values (5,'Audifono a precio bajo',5,5);


insert into usuario values ('Jose Antonio Iberico Cabezas','jiberico','1234',1);
insert into usuario values ('Carlos Edwin Iberico Cabezas','ciberico','1234',1);


/*Listado de de productos con categoria*/
select P.id, P.nombre,P.descripcion , P.precio , P.stock , P.marca , C.tipo 
from producto P 
inner join categoria_producto C on C.id = P.idcategoria;
/*Listado de Entradas*/
select E.id , P.nombre , E.descripcion , E.stockE , U.username
from entrada_producto E
inner join producto P on P.id = E.idproducto 
inner join usuario U on U.id = E.id_usuario ;
/*Listado de salidas*/
select S.id , P.nombre , S.descripcion , S.stockS , U.username
from salida_producto S
inner join producto P on P.id = S.idproducto 
inner join usuario U on U.id = S.id_usuario ;


CREATE TRIGGER trg_AumentarStock
ON entrada_producto
AFTER INSERT
AS
BEGIN
    -- Actualizar el stock del producto correspondiente en función de la entrada insertada
    UPDATE producto
    SET stock = stock + i.stockE
    FROM producto p
    INNER JOIN inserted i ON p.id = i.idproducto;
END;

CREATE TRIGGER trg_RestarStock
ON salida_producto
AFTER INSERT
AS
BEGIN
    -- Actualizar el stock del producto correspondiente en función de la salida insertada
    UPDATE producto
    SET stock = stock - i.stockS
    FROM producto p
    INNER JOIN inserted i ON p.id = i.idproducto;
END;