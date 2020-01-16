using System;
using FluentMigrator;

namespace Musili.ApiApp.Migrations {
    [Migration(201909172310)]
    public class FirstTestMigration : Migration {
        public override void Up() {
            var sql = @"
                CREATE TABLE app.test_table (
                    id serial primary key,
                    some_data text not null
                );
                INSERT INTO app.test_table (id, some_data) VALUES (1, 'hello!');
            ";
            Execute.Sql(sql);
        }

        public override void Down() {
            var sql = @"DROP TABLE app.test_table;";
            Execute.Sql(sql);
        }
    }
}
