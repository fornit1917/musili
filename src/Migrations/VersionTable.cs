using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musili.ApiApp.Migrations {
    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData {
        public string ColumnName => "version";
        public string SchemaName => "app";
        public string TableName => "_migrations";
        public string UniqueIndexName => "_migrations_version_idx";
        public string AppliedOnColumnName => "applied_on";
        public string DescriptionColumnName => "description";
        public object ApplicationContext { get; set; }
        public bool OwnsSchema => true;
    }
}
