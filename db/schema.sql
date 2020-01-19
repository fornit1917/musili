CREATE SCHEMA IF NOT EXISTS app;

CREATE TABLE app.tracks_source (
	id serial NOT NULL,
	is_priority bool NOT NULL DEFAULT false,
	service int2 NOT NULL,
	source_type int2 NOT NULL,
	value varchar(2048) NOT NULL,
	params_json text NULL,
	tags text[] NULL,
	CONSTRAINT tracks_source_pkey PRIMARY KEY (id)
);
CREATE INDEX tracks_source_is_priority_idx ON app.tracks_source USING btree (is_priority);
CREATE INDEX tracks_source_tags_idx ON app.tracks_source USING gin (tags);

CREATE TABLE app.track (
	id serial NOT NULL,
	tracks_source_id int4 NOT NULL,
	artist varchar(255) NOT NULL,
	title varchar(255) NOT NULL,
	url varchar(2048) NOT NULL,
	duration int4 NOT NULL DEFAULT 0,
	expiration_datetime timestamp NOT NULL,
	CONSTRAINT track_pkey PRIMARY KEY (id),
	CONSTRAINT track_tracks_source_id_fkey FOREIGN KEY (tracks_source_id) REFERENCES app.tracks_source(id) ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE INDEX track_expiration_datetime_idx ON app.track USING btree (expiration_datetime);
CREATE INDEX track_tracks_source_idx ON app.track USING btree (tracks_source_id);
