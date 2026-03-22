create table logins (
    id serial primary key,
    login varchar(255) not null,
    password varchar(255) not null
);

create table specialties (
    id serial primary key,
    name varchar(255) not null,
    description varchar(255)
);

create table groups (
    id serial primary key,
    number varchar(255) not null,
    specialty_id int not null references specialties(id),
    description varchar(255)
);

create table users (
    id serial primary key,
    first_name varchar(255) not null,
    last_name varchar(255) not null,
    patronymic varchar(255),
    dob date,
	login_id int references logins(id),
    group_id int not null references groups(id)
);
