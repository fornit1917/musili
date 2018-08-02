create table app.tracks_source (
	id serial primary key,
	tempo smallint not null,
	genre smallint not null,
	service smallint not null,
	source_type smallint not null,
	value varchar(2048) not null,
	params_json text default null
);
create index tracks_source_tempo_idx on app.tracks_source(tempo);
create index tracks_source_genre_idx on app.tracks_source(genre);

create table app.track (
	id serial primary key,
	tempo smallint not null,
	genre smallint not null,	
	tracks_source_id int not null references app.tracks_source(id) on delete cascade on update cascade,
	artist varchar(255) not null,
	title varchar(255) not null,
	url varchar(2048) not null,
	duration int not null default 0,
	expiration_datetime timestamp not null
);
create index track_tracks_source_idx on app.track(tracks_source_id);
create index track_expiration_datetime_idx on app.track(expiration_datetime);
create index track_tempo_idx on app.track(tempo);
create index track_genre_idx on app.track(genre);