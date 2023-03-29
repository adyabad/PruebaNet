
create table Encuestas(
	EncuestaId int Primary Key Identity,	
	Fecha Datetime not null	
)

create table Respuestas(
	RespuestaId int Primary Key Identity,
	Pregunta varchar(max) not null,
	Respuesta varchar(max) not null,
	EncuestaId int Foreign key References Encuestas(EncuestaId)
)
