﻿namespace MongoORM4NetCore.Interfaces
{
    public abstract class DbObjectSD : DbObject
    {
        public byte IsDeleted { get; set; }

        protected DbObjectSD()
        {
            IsDeleted = 0;
        }
    }
}