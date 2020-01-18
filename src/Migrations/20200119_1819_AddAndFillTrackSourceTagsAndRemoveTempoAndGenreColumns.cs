using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Migrations {
    [Migration(202001191819)]
    public class _20200119_1819_AddAndFillTrackSourceTagsAndRemoveTempoAndGenreColumns : Migration {
        public override void Up() {
            var sql = @"
                ALTER TABLE app.tracks_source ADD COLUMN IF NOT EXISTS tags text[] DEFAULT NULL;

                DO $$
                DECLARE
                    src record;
                    new_tags text[];
                    tempo text;
                    genre text;
                    need_execute bool;
                BEGIN
                    SELECT (COUNT(*) > 0) INTO need_execute 
                    FROM information_schema.COLUMNS
                    WHERE table_schema = 'app' AND table_name = 'tracks_source' AND column_name = 'tempo';

                    IF need_execute THEN                
                        FOR src IN SELECT * FROM app.tracks_source
                        LOOP
                            CASE src.tempo
                                WHEN 1 THEN tempo := 'soft';
                                WHEN 2 THEN tempo := 'rhythmic';
                                ELSE tempo := '';
                            END CASE;
     
                            CASE src.genre
                                WHEN 1 THEN genre := 'classical';
                                WHEN 2 THEN genre := 'electronic';
                                WHEN 3 THEN genre := 'jazz';
                                WHEN 4 THEN genre := 'rock';
                                WHEN 5 THEN genre := 'metal';
                                ELSE genre := '';
                            END CASE;
            
                            new_tags := ARRAY[]::varchar(40)[];
                            IF (tempo != '') THEN
                                new_tags := array_append(new_tags, tempo);
                            END IF;
                            IF (genre != '') THEN
                                new_tags := array_append(new_tags, genre);
                            END IF;
        
                            UPDATE app.tracks_source SET tags = new_tags WHERE id = src.id;
                        END LOOP;
                    END IF;
                END $$;

                CREATE INDEX IF NOT EXISTS tracks_source_tags_idx ON app.tracks_source USING GIN (tags);

                ALTER TABLE app.tracks_source DROP COLUMN IF EXISTS tempo;
                ALTER TABLE app.tracks_source DROP COLUMN IF EXISTS genre;
            ";

            Execute.Sql(sql);
        }

        public override void Down() { }
    }
}
