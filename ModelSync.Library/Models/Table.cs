﻿using ModelSync.Library.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelSync.Library.Models
{
    public class Table : DbObject
    {
        public override ObjectType ObjectType => ObjectType.Table;
        public string Schema { get; set; }
        public string IdentityColumn { get; set; }

        public IEnumerable<Column> Columns { get; set; }
        public IEnumerable<Index> Indexes { get; set; }

        public override string CreateStatement(DbObject parentObject)
        {
            bool isIdentity(string columnName) { return columnName.Equals(IdentityColumn); }

            List<string> members = new List<string>();
            members.AddRange(Columns.Select(col => col.GetDefinition(isIdentity(col.Name))));
            members.AddRange(Indexes.Select(ndx => ndx.GetDefinition()));

            string createMembers = string.Join(",\r\n", members.Select(member => "\t" + member));

            return $"CREATE TABLE <{this}> (\r\n{createMembers}\r\n)";
        }

        public override string DropStatement(DbObject parentObject)
        {
            return $"DROP TABLE <{this}>";
        }

        public override IEnumerable<DbObject> GetDropDependencies(DataModel dataModel)
        {
            // return foreign keys referencing this table
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return (!string.IsNullOrEmpty(Schema)) ? $"{Schema}.{Name}" : Name;
        }
    }
}
