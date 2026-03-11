create table specialties (
	id serial primary key,
    name varchar(50)
);

create table groups (
	id serial primary key,
	number varchar(50),
	specialty_id int references specialties(id)
);

create table logins (
	id serial primary key,
    login varchar(50),
    password varchar(50)
);

create table users (
	id serial primary key,
    first_name varchar(50),
    last_name varchar(50),
    patronymic varchar(50),
	dob date,
	login_id int references logins(id),
    group_id int references groups(id)
);
