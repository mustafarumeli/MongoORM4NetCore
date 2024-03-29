﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using MongoORM4NetCore.Interfaces;

namespace MongoORM4NetCore.Structs
{
    public struct MongoConnectionStringReplicas
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public override string ToString()
        {
            return $"{nameof(Host)}: {Host}, {nameof(Port)}: {Port}";
        }
    }
}
