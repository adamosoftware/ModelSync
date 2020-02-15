﻿using ModelSync.Library.Models;
using System;
using System.Collections.Generic;

namespace ModelSync.Library.Abstract
{
    public enum ObjectType
    {
        Table,
        Column,
        Index,
        ForeignKey
    }

    public abstract class DbObject
    {        
        public string Name { get; set; }
        
        public abstract ObjectType ObjectType { get; }
        public abstract IEnumerable<string> CreateStatements(DbObject parentObject);
        public abstract string DropStatement(DbObject parentObject);
        public abstract IEnumerable<DbObject> GetDependencies(DataModel dataModel);
    }
}
