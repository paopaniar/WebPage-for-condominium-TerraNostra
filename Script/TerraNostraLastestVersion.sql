CREATE DATABASE terraNostra

USE terraNostra
CREATE FUNCTION encripta_pass
 (
 @contrasena VARCHAR(50)
 )
 RETURNS VarBinary(500)
 AS
 BEGIN
 DECLARE @pass AS VarBinary(500)
 SET @pass=ENCRYPTBYPASSPHRASE('MiclaveSecreta',@contrasena)
 RETURN @pass
 END;
GO

CREATE FUNCTION desencriptar_pass
  (
  @contrasena VarBinary(500)
  )
  RETURNS VARCHAR(50)
  AS
  BEGIN
  DECLARE @pass AS VARCHAR(50)
  SET @pass=DECRYPTBYPASSPHRASE('MiclaveSecreta',@contrasena)
  RETURN @pass
  END;
GO
CREATE TABLE rol
(
rolId INT PRIMARY KEY  NOT NULL, 
rol VARCHAR(250) NOT NULL
);

CREATE TABLE usuario
(
rolId INT NOT NULL,
identificacion INT NOT NULL
,nombre VARCHAR(255)
,apellido VARCHAR(255)
,apellido2 VARCHAR(255)
,telefono INT
,estado INT DEFAULT 1
,PRIMARY KEY (identificacion)
,FOREIGN KEY (rolId) REFERENCES rol(rolId)
);

CREATE TABLE incidente (
    id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    usuario INT NOT NULL,
    estado INT DEFAULT 1, --?PORQUE UN 1? PORQUE LA INCIDENCIA ENTRA ACTIVA Y LA TRANSFORMAMOS A 0 CUANDO SE CIERRE
    tipo INT NOT NULL,
	detalle VARCHAR(255)
  FOREIGN KEY (usuario) REFERENCES usuario(identificacion)
)

CREATE TABLE informacion (
    id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	usuario INT NOT NULL,
    detalle VARCHAR(500) NOT NULL,
	fechaInformacion DATETIME2,
    tipo INT NOT NULL, -- EJ 1 si es Acta, 2 noticias esto para filtros
	estado INT NOT NULL,
	FOREIGN KEY (usuario) REFERENCES usuario(identificacion)
)
-----------------------------------------------------------------------------------------------
CREATE TABLE areaComun (
    id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    detalle VARCHAR(250) NOT NULL,
	disponibilidad INT NOT NULL,
	fechaDisponible VARCHAR NOT NULL,
	horaDisponible INT NOT NULL,
	estado INT NOT NULL
 )


 CREATE TABLE reservacion (
    id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	areaComunId INT NOT NULL, 
    usuario INT NOT NULL,
    detalle VARCHAR(255) NOT NULL,
    estado INT NOT NULL,
    dateFrom DATETIME2,
    dateTo DATETIME2,
    FOREIGN KEY (usuario) REFERENCES usuario(identificacion),
	FOREIGN KEY (areaComunId) REFERENCES areaComun(id)

)


CREATE TABLE residencia (
    id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	usuario INT,
    individualsNumber INT NOT NULL,
    annio VARCHAR(4) NOT NULL,
    estado INT NOT NULL,
    otherInfoDetails VARCHAR(150) NOT NULL,
	numeroCasa INT,
    FOREIGN KEY (usuario) REFERENCES usuario(identificacion)
)

CREATE TABLE rubro_cobro (
    id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    detalle VARCHAR(150) NOT NULL,
    monto DECIMAL NOT NULL,
    estado INT NOT NULL,
)

CREATE TABLE plan_cobro (
    id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    detail VARCHAR(150) NOT NULL,
	mes VARCHAR(30),
    datePlan DATETIME2 NOT NULL, 
    estado INT NOT NULL,
    monto DECIMAL,  
)


CREATE TABLE plan_residencia (
    id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
	planCobroId INT NOT NULL,
	residenciaId INT NOT NULL,
	detalle VARCHAR(150)NOT NULL,
    estado INT NOT NULL,
	total DECIMAL,
    FOREIGN KEY (residenciaId) REFERENCES residencia(id),
	FOREIGN KEY (planCobroId) REFERENCES plan_cobro(id)
)


CREATE TABLE plan_rubro (
	planCobroId INT NOT NULL,
	rubroId INT NOT NULL,	
    FOREIGN KEY (rubroId) REFERENCES rubro_cobro(id),
	FOREIGN KEY (planCobroId) REFERENCES plan_cobro(id)
)


INSERT INTO rol (
rolId
,rol
) 
values (
1
,'Admin'
);
GO
INSERT INTO rol (
rolId
,rol
) 
values (
2
,'Residente'
);
GO


INSERT INTO usuario (rolId,identificacion,nombre ,apellido,apellido2,telefono) 
values (1,891426789, 'Admin','Administrador','Administra',62666090);
----residente user
INSERT INTO usuario (rolId,identificacion,nombre ,apellido,apellido2,telefono) 
VALUES (2,207940152, 'Paola','Paniagua','Arroyo',9999999);

INSERT INTO usuario (rolId,identificacion,nombre ,apellido,apellido2,telefono) 
VALUES (2,207940153, 'Marianela','Residenta','Residente',88888888);

INSERT INTO usuario (rolId,identificacion,nombre ,apellido,apellido2,telefono) 
VALUES (2,207940154, 'Residente','Residenta','Residente',99900900);

INSERT INTO usuario (rolId,identificacion,nombre ,apellido,apellido2,telefono) 
VALUES (1,207940155, 'Johanna','Administrador','Araya',00000000);



INSERT INTO residencia(usuario, individualsNumber,annio,estado,otherInfoDetails,numeroCasa) 
VALUES (207940153, 4, 2023, 1, 'Descripción de prueba', 01);
INSERT INTO residencia(usuario, individualsNumber,annio,estado,otherInfoDetails,numeroCasa) 
VALUES (207940154, 6, 2022, 1, 'Descripción de prueba', 02);
INSERT INTO residencia(usuario, individualsNumber,annio,estado,otherInfoDetails,numeroCasa) 
VALUES (207940152, 6, 2022, 1, 'Descripción de prueba', 03);
INSERT INTO residencia( individualsNumber,annio,estado,otherInfoDetails,numeroCasa) 
VALUES ( 0, 2022, 0, 'Nueva', 04);


INSERT INTO rubro_cobro(detalle, monto,estado) 
VALUES ('MZV', 10000.00, 1);
INSERT INTO rubro_cobro(detalle,monto,estado) 
VALUES ('Mantenimiento zona verde',10000.00,1);
INSERT INTO rubro_cobro(detalle,monto,estado) 
VALUES ('Mensualidad de Residencial',50000.00,1);
INSERT INTO rubro_cobro(detalle,monto,estado) 
VALUES ('Mantenimiento de tejado',10000.00,1);
INSERT INTO rubro_cobro(detalle,monto,estado) 
VALUES ('Recoleccion de basura',10000.00,1);
INSERT INTO rubro_cobro(detalle,monto,estado) 
VALUES ('Limpieza de estructura',20000.00,1);


Declare @Total decimal = (select SUM(monto) from rubro_cobro r
inner join plan_rubro pr on pr.rubroId= r.id
inner join plan_residencia prr on prr.planCobroId = pr.planCobroId
WHERE prr.residenciaId = 1)



INSERT INTO plan_cobro(detail, mes, datePlan,estado) 
VALUES ('Mantenimientos','Marzo','01-03-2023',1 );

INSERT INTO plan_cobro(detail,mes, datePlan,estado) 
VALUES ('Limpieza','Marzo','01-03-2023',1);

INSERT INTO plan_cobro(detail, mes,datePlan,estado) 
VALUES ('Recoleccion','Marzo','01-03-2023',1);

INSERT INTO plan_cobro(detail, mes, datePlan,estado) 
VALUES ('Mensualidad','Marzo','01-03-2023',1);


INSERT INTO plan_residencia(planCobroId,residenciaId,detalle,estado) 
VALUES (1, 1,'Detalle de prueba',1);

INSERT INTO plan_residencia(planCobroId,residenciaId,detalle,estado) 
VALUES (2, 2,'Detalle de prueba',0);

INSERT INTO plan_residencia(planCobroId,residenciaId,detalle,estado) 
VALUES (3, 1,'Detalle de prueba',0);

INSERT INTO incidente(usuario, estado, tipo, detalle)
VALUES(207940152, 1, 3, 'Apartar piscina')
INSERT INTO incidente(usuario, estado, tipo, detalle)
VALUES(207940153, 1, 2, 'Problemas electricos')
INSERT INTO incidente(usuario, estado, tipo, detalle)
VALUES(207940154, 1, 3, 'Apartar salón de sauna')

INSERT INTO informacion(usuario, detalle, fechaInformacion, tipo, estado)
VALUES(891426789,'Se comunica que la seguridad ha sido mejorada...',GETDATE(),1,1)
INSERT INTO informacion(usuario, detalle, fechaInformacion, tipo, estado)
VALUES(891426789,'Nueva reglamentaciones del uso de piscina',GETDATE(),2,1)
INSERT INTO informacion(usuario, detalle, fechaInformacion, tipo, estado)
VALUES(891426789,'Nuevos artículos implementados en contratos I y II',GETDATE(),3,1)


INSERT INTO plan_rubro VALUES(1,2)
INSERT INTO plan_rubro VALUES(1,3)
INSERT INTO plan_rubro VALUES(2,6)
INSERT INTO plan_rubro VALUES(3,5)
